using System.Collections.ObjectModel;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using CommunityToolkit.Mvvm.Input;
using WeakEventManager = Microsoft.Maui.WeakEventManager;

namespace CMMMobileMaui.VM
{
    public class VMDeviceSearch : ScannerViewModelBase
    {
        #region FIELDS

        private readonly WeakEventManager deviceSelectedWeakManager = new WeakEventManager();
        //private COMMON.IScannerService _zebraScaner;

        public event EventHandler<GetDeviceListResponse> OnDeviceSelected
        {
            add => deviceSelectedWeakManager.AddEventHandler(value);
            remove => deviceSelectedWeakManager.RemoveEventHandler(value);
        }

        private IDeviceController deviceCMMBLL;

        #endregion

        #region PROPERTY IsPartGive

        public bool IsPartGive
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY MainDeviceCategoryID

        public int? MainDeviceCategoryID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsCatWithCycles

        public bool? IsCatWithCycles
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY FilterText

        public string? FilterText
        {
            get => filterText;
            set => SetProperty(ref filterText, value);
        }

        #endregion

        #region PROPERTY DeviceList

        public ObservableCollection<MODEL.DeviceModel> DeviceList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentDevice

        public MODEL.DeviceModel? CurrentDevice
        {
            get;
            set;
        }

        #endregion

        #region COMMAND FilterDeviceCommand

        public ICommand FilterDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND ScanSerialNoCommand

        public ICommand ScanSerialNoCommand
        {
            get;
        }

        #endregion

        #region COMMAND ScanCodeCommand

        public ICommand ScanCodeCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddNewDeviceCommand

        public ICommand AddNewDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND RefrehListCommand

        public ICommand RefrehListCommand
        {
            get;
        }

        #endregion

        #region COMMAND TakeDeviceCommand

        public ICommand TakeDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND LoadNextItemsCommand

        public IAsyncRelayCommand LoadNextItemsCommand
        {
            get;
        }

        #endregion

        #region Cstr
        public VMDeviceSearch(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;

            DeviceList = new ObservableCollection<MODEL.DeviceModel>();

            LoadNextItemsCommand = new AsyncRelayCommand(LoadNextItems);

            SConsts.SetGlobalAction<string>(SConsts.DEV_SCAN_TEXT, async (item) =>
            {
                await Shell.Current.Navigation.PopModalAsync();
                FilterText = item;
                FilterDeviceCommand?.Execute(null);
            });


            SConsts.SetGlobalAction<GetDeviceListResponse>(SConsts.DEV_SCAN_CODE, (item) =>
             {
                 GoToDevice(new MODEL.DeviceModel(item, TakeDeviceCommand!));
             });

            ScanCodeCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.ScanView(ScanType.Device));
                }
            });

            ScanSerialNoCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.ScanView(ScanType.Text));
                }
            });

            FilterDeviceCommand = new Command(() =>
            {
                RefreshList();
            });

            AddNewDeviceCommand = new Command(async (obj) =>
            {
                await OpenModalPage(SConsts.GetBaseNavigationPage(new VIEW.NewDeviceView()));
            });

            RefrehListCommand = new Command((obj) =>
            {
                RefreshList();
            });

            TakeDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is MODEL.DeviceModel device)
                    {
                        device.IsBusy = true;
                        await Task.Delay(200);

                        if (IsInTakeDeviceMode())
                        {
                            API.MainObjects.Instance.CurrentDevice = device.BaseItem;

                            CurrentDevice = device;

                            await Shell.Current.Navigation.PopModalAsync();

                            deviceSelectedWeakManager.HandleEvent(this, device.BaseItem, nameof(OnDeviceSelected));
                        }
                        else
                        {
                            API.MainObjects.Instance.CurrentDevice = device.BaseItem;

                            GoToDevice(device);
                        }

                        device.IsBusy = false;
                    }
                }
            });
        }

        #endregion

        #region METHOD IsInTakeDeviceMode

        private bool IsInTakeDeviceMode() =>
            IsPartGive || (IsCatWithCycles.HasValue && IsCatWithCycles.Value);

        #endregion

        #region METHOD RefreshList

        private List<GetDeviceListResponse> tempDeviceList;
        private string? filterText;

        public async void RefreshList()
        {
            if (CanClick())
            {
                IsBusy = true;
                DeviceList.Clear();

                if (string.IsNullOrEmpty(FilterText))
                {
                    IsBusy = false;
                    return;
                }

                var deviceItemsResponse = await deviceCMMBLL.GetList(new API.Contracts.v1.Requests.Device.GetDeviceListRequest
                {
                    Name = FilterText,
                    CategoryID = MainDeviceCategoryID,
                    Lang = API.MainObjects.Instance.Lang,
                    IsCatWithCycle = IsCatWithCycles
                });

                if (deviceItemsResponse.IsResponseWithData(this))
                {
                    tempDeviceList = deviceItemsResponse.Data!.ToList();
                    //DeviceList = new MvvmHelpers.ObservableRangeCollection<MODEL.DeviceModel>();

                    //deviceItemsResponse.Data!.ToList().ForEach(tt =>
                    //{
                    //    DeviceList.Add(new MODEL.DeviceModel(tt, TakeDeviceCommand));
                    //});

                   await LoadNextItems();
                }

                IsBusy = false;

                OnPropertyChanged("DeviceList");
            }       
        }

        #endregion

        private async Task LoadNextItems()
        {
            if(tempDeviceList == null || tempDeviceList.Count == 0)
            {
                return;
            }

            if(DeviceList.Count != tempDeviceList.Count)
            {
                var tempList = tempDeviceList.Skip(DeviceList.Count).Take(10).ToList();

                tempList.ForEach(tt =>
                {
                    DeviceList.Add(new MODEL.DeviceModel(tt, TakeDeviceCommand));
                });
            }
        }

        #region METHOD GoToDevice

        private async void GoToDevice(MODEL.DeviceModel item)
        {
            if (item != null
                && !IsPartGive)
            {
                DBMain.Engine en = new DBMain.Engine();

                var histList = await en.GetAllHistory(API.MainObjects.Instance.CurrentUser!.PersonID);

                var hist = histList.FirstOrDefault(tt => tt.ID == Convert.ToInt32(item.BaseItem.MachineID) && tt.Type == "d");

                if (hist != null)
                {
                    hist.Mod_Date = DateTime.Now;
                    int a = await en.UpdateHist(hist);
                }
                else
                {
                    hist = new DBMain.Model.History();
                    hist.ID = item.BaseItem.MachineID;
                    hist.Type = "d";
                    hist.Mod_Date = DateTime.Now;
                    hist.Name = item.BaseItem.AssetNo;
                    hist.PersonID = API.MainObjects.Instance.CurrentUser.PersonID;
                    int b = await en.InsertHist(hist);
                }

                en.CloseConnection();

                ShowItem(item);
            }
        }

        #endregion

        #region METHOD ShowItem

        private async void ShowItem(MODEL.DeviceModel obj)
        {
            if (obj != null && obj is MODEL.DeviceModel devMod)
            {
                await OpenModalPage(new VIEW.DeviceView(devMod.BaseItem));
            }
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            var deviceScan = new DeviceScan();
            var deviceMESScan = new DeviceMESScan();

            deviceScan.UIMethod = async (obj) =>
            {
                if (obj is GetDeviceListResponse device)
                {
                    if (MainDeviceCategoryID.HasValue)
                    {
                        if (device.CategoryID == MainDeviceCategoryID.Value)
                        {
                            if(Shell.Current.Navigation.ModalStack.Count > 0)
                                await Shell.Current.Navigation.PopModalAsync();

                            deviceSelectedWeakManager.HandleEvent(this, device, nameof(OnDeviceSelected));
                        }
                    }
                    else if (IsPartGive)
                    {
                        deviceSelectedWeakManager.HandleEvent(this, device, nameof(OnDeviceSelected));

                        if (Shell.Current.Navigation.ModalStack.Count > 0)
                            await Shell.Current.Navigation.PopModalAsync();
                    }
                    else
                    {
                        GoToDevice(new MODEL.DeviceModel(device, TakeDeviceCommand));
                    }
                }
            };

            deviceMESScan.UIMethod = async (obj) =>
            {
                if (obj is GetDeviceListResponse device)
                {
                    if (MainDeviceCategoryID.HasValue)
                    {
                        if (device.CategoryID == MainDeviceCategoryID.Value)
                        {
                            await Shell.Current.Navigation.PopModalAsync();

                            deviceSelectedWeakManager.HandleEvent(this, device, nameof(OnDeviceSelected));
                        }
                    }
                    else if (IsPartGive)
                    {
                        await Shell.Current.Navigation.PopModalAsync();
                        deviceSelectedWeakManager.HandleEvent(this, device, nameof(OnDeviceSelected));
                    }
                    else
                    {
                        GoToDevice(new MODEL.DeviceModel(device, TakeDeviceCommand));
                    }
                }
            };

            scanTypes.Add(deviceScan);
            scanTypes.Add(deviceMESScan);

            return scanTypes;
        }

        public override string GetVisualScanPresentation() => "precision_manufacturing";

        #endregion
    }
}
