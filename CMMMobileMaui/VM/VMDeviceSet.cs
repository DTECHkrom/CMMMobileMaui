using System.Collections.ObjectModel;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Set;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.VM
{
    public class VMDeviceSet : ViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceCMMBLL;
        private ISetController setController;
        private readonly WeakEventManager eventWeakManager = new WeakEventManager();

        private List<GetSetsResponse>? setList;
        private List<GetDeviceListResponse>? deviceList;
        private List<GetSubSetsResponse>? subSetList;
        private List<DeviceSetModel> itemsToCollapse = new List<DeviceSetModel>();

        public event EventHandler<List<DeviceSetModel>> OnItemsSet
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        public event EventHandler<bool> OnCollapseExpandAll
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        #endregion

        #region PROPERTY Source

        private ObservableCollection<DeviceSetModel> source = new ObservableCollection<DeviceSetModel>();

        public ObservableCollection<DeviceSetModel> Source
        {
            get => source;
            set => SetProperty(ref source, value);
        }

        #endregion

        #region PROPERTY FilterText

        private string? filterText;

        public string? FilterText
        {
            get => filterText;
            set
            {
                if(SetProperty(ref filterText, value))
                {
                    FilterList(false);
                }
            }
        }

        #endregion

        #region COMMAND OpenDeviceCommand

        public ICommand OpenDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND LoadItemsCommand

        public ICommand LoadItemsCommand
        {
            get;
        }

        #endregion

        #region COMMAND ExpandAllCommand

        public ICommand ExpandAllCommand
        {
            get;
        }

        #endregion

        #region COMMAND CollapseAllCommand

        public ICommand CollapseAllCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDeviceSet(IDeviceController deviceCMMBLL, ISetController setController)
        {
            this.deviceCMMBLL = deviceCMMBLL;
            this.setController = setController;

            OpenDeviceCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if(obj is DeviceSetModel temp 
                    && temp.LineType == LinePartType.Device)
                    {
                        OpenDevice(temp);
                    }                  
                }
            });

            LoadItemsCommand = new Command(() =>
            {
                if (CanClick())
                {
                    InitData(true);
                }
            });

            ExpandAllCommand = new Command(() =>
            {
                if (CanClick())
                {
                    eventWeakManager.HandleEvent(this, true, nameof(OnCollapseExpandAll));
                }
            });

            CollapseAllCommand = new Command(() =>
            {
                if (CanClick())
                {
                    eventWeakManager.HandleEvent(this, false, nameof(OnCollapseExpandAll));
                }
            });

            SConsts.SetGlobalAction(SConsts.SET_DEVICE, () =>
            {
                InitData(false);
            });
        }

        #endregion

        #region METHOD InitData

        public async void InitData(bool isFirstLoad)
        {
            IsBusy = true;

            var setTask = setController.GetSets();
            var subSetTask = setController.GetSubSets();
            var deviceTask = deviceCMMBLL.GetWithSet(new API.Contracts.v1.Requests.Device.GetDeviceWithSetRequest
            {
                IsSet = true,
                Lang = API.MainObjects.Instance.Lang
            });

            await Task.WhenAll(setTask, subSetTask, deviceTask);

            var setResult = setTask.Result;
            var subSetResult = subSetTask.Result;
            var deviceResult = deviceTask.Result;

            if(!setResult.IsResponseWithData(this)
                || !subSetResult.IsResponseWithData(this)
                || !deviceResult.IsResponseWithData(this))
            {
                return;
            }

            setList = setResult.Data!.ToList();
            subSetList = subSetResult.Data!.ToList();
            deviceList = deviceResult.Data!.ToList();
            
            IsBusy = false;

            FilterList(isFirstLoad);
        }

        internal async void OpenDevice(DeviceSetModel temp)
        {
            var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
            {
                MachineID = temp.ID
                        ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (deviceResponse.IsResponseWithData(this))
            {
                await OpenModalPage(new VIEW.DeviceView(deviceResponse.Data!));
            }
        }

        #endregion

        #region METHOD FilterList

        public void FilterList(bool isFirstLoad)
        {
            IsBusy = true;

            if (setList != null && deviceList != null)
            {
                Source = new ObservableCollection<DeviceSetModel>();

                foreach (var branchName in setList.Select(tt => tt.Branch_Name).Distinct())
                {
                    var branch = DeviceSetModel.FromBranchName(branchName);

                    foreach (var locationName in setList.Where(tt => tt.Branch_Name == branchName)
                        .Select(tt => tt.Location_Name)
                        .OrderBy(tt => tt)
                        .Distinct())
                    {
                        var location = DeviceSetModel.FromLocationName(locationName);

                        foreach (var setData in setList.Where(tt => tt.Branch_Name == branchName && tt.Location_Name == locationName)
                            .Distinct()
                            .OrderBy(tt => tt.Display_Index)
                            .Select(tt => new { tt.SetID, tt.Set_Name, tt.Display_Index }))
                        {
                            var set = DeviceSetModel.FromSet(setData.Set_Name, setData.SetID);

                            foreach (var deviceName in deviceList.Where(tt => tt.SetID == setData.SetID)
                                .OrderBy(tt => tt.AssetNo))
                            {
                                var device = DeviceSetModel.FromDevice(deviceName);
                                set.AddChild(device);
                            }

                            if (set.HasChildren()
                                && set.IsFilerItem(FilterText ?? string.Empty))
                            {
                                if (isFirstLoad)
                                {
                                    itemsToCollapse.Add(set);
                                }

                                set.SetNodeIcon();

                                location.Children.Add(set);
                            }
                        }

                        if (location.HasChildren())
                        {
                            branch.Children.Add(location);
                        }
                    }

                    if (branch.HasChildren())
                    {
                        try
                        {
                            Source.Add(branch);
                        }
                        catch (Exception) { }
                    }
                }
            }

            if (isFirstLoad
                && itemsToCollapse.Count > 0)
            {
                //  OnItemsSet?.Invoke(this, itemsToCollapse);
                eventWeakManager.HandleEvent(this, itemsToCollapse, nameof(OnItemsSet));
            }

            IsBusy = false;
        }

        #endregion
    }
}
