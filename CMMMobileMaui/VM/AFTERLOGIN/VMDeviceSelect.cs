using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.Models.Handlers;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using System.Windows.Input;

namespace CMMMobileMaui.VM.AFTERLOGIN
{
    public class VMDeviceSelect : COMMON.ViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceController;
        private GetDeviceListResponseHandler? currentDeviceHandler;

        #endregion

        #region PROEPRTY CurrentObservedDeviceList

        private List<GetObservedResponse>? currentObservedDeviceList;

        public List<GetObservedResponse>? CurrentObservedDeviceList
        {
            get => currentObservedDeviceList;
            set => SetProperty(ref currentObservedDeviceList, value);
        }

        #endregion

        #region PROPERTY ObservedDeviceHistoryList

        private List<GetObservedResponse>? observedDeviceHistoryList;

        public List<GetObservedResponse>? ObservedDeviceHistoryList
        {
            get => observedDeviceHistoryList;
            set => SetProperty(ref observedDeviceHistoryList, value);
        }

        #endregion

        #region PROPERY CurrentDevice

        private GetDeviceListResponse? currentDevice;

        public GetDeviceListResponse? CurrentDevice
        {
            get => currentDevice;
            set
            {
                SetProperty(ref currentDevice, value);
                IsDeviceSelected = value != null;
                currentDeviceHandler = currentDevice;
            }
        }

        #endregion

        #region PROPERTY IsDeviceSelected

        private bool isDeviceSelected;
        private long? cycle;

        public bool IsDeviceSelected
        {
            get => isDeviceSelected;
            set => SetProperty(ref isDeviceSelected, value);
        }

        #endregion

        #region PROPERTY Cycle

        public long? Cycle
        {
            get => cycle;
            set => SetProperty(ref cycle, value);
        }

        #endregion

        #region COMMAND ShowDeviceWOCommand

        public ICommand ShowDeviceWOCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectDeviceCommand

        public ICommand SelectDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCommand

        public ICommand SaveCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelCommand

        public ICommand CancelCommand
        {
            get;
        }

        #endregion

        #region COMMAND ReselectDeviceCommand

        public ICommand ReselectDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenCycleHistoryCommand

        public ICommand OpenCycleHistoryCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDeviceSelect(IDeviceController deviceController)
        {
            this.deviceController = deviceController;

            SelectDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.DeviceSearchView vm = new VIEW.DeviceSearchView();
                    vm.ViewModel.IsCatWithCycles = true;

                    vm.OnDeviceSelected += Vm_OnDeviceSelected;
                    await OpenModalPage(vm);
                }
            });

            SaveCommand = new Command(() =>
            {
                if (CanClick())
                {
                    SaveMethod();
                }
            });

            ReselectDeviceCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetObservedResponse observedDeviceHistory)
                    {
                        ReselectDeviceMethod(observedDeviceHistory);
                    }
                }
            });

            OpenCycleHistoryCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetObservedResponse observedResponse)
                    {
                        VIEW.ObservedDeviceCyclesPage observedDeviceCyclesPage = new VIEW.ObservedDeviceCyclesPage();
                        observedDeviceCyclesPage.ViewModel.CurrentMachineID = observedResponse.MachineID;
                        await OpenModalPage(observedDeviceCyclesPage);
                    }
                }
            });

            CancelCommand = new Command(() =>
            {
                if (CanClick())
                {
                    ClearAfterSave();
                }
            });

            ShowDeviceWOCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetObservedResponse observedResponse)
                    {
                        var device = await deviceController.GetById(new API.Contracts.v1.Requests.Device.GetByDeviceIDRequest
                        {
                            DeviceID = observedResponse.MachineID
                        });

                        if (device.IsResponseWithData(this))
                        {
                            MainObjects.Instance.CurrentDevice = device.Data;

                            var deviceWOListView = new VIEW.WorkOrderListAllView(false, canAddNewWO: false, isActive: true);
                            deviceWOListView.OnPageUnload += DeviceWOListView_OnPageUnload;
                            await OpenModalPage(deviceWOListView);
                        }
                    }
                }
            });
        }

        private void DeviceWOListView_OnPageUnload(object? sender, EventArgs e)
        {
            MainObjects.Instance.CurrentDevice = null;
        }

        #endregion

        #region METHOD ReselectDeviceMethod

        private async void ReselectDeviceMethod(GetObservedResponse observedDeviceHistory)
        {
            var deviceResponse = await deviceController.GetList(new API.Contracts.v1.Requests.Device.GetDeviceListRequest
            {
                MachineIDs = new List<int> { observedDeviceHistory.MachineID }
                ,
                Lang = MainObjects.Instance.Lang
            });

            if (deviceResponse.IsResponseWithData(this))
            {
                CurrentDevice = deviceResponse.Data!.FirstOrDefault();
            }
        }

        #endregion

        #region METHOD SaveMethod

        private async void SaveMethod()
        {
            if (!CanSave())
                return;

            var result = await deviceController.CreateObserved(new API.Contracts.v1.Requests.Device.CreateObservedDeviceRequest
            {
                MachineIDs = new List<API.Contracts.v1.Requests.Device.CreateObservedDeviceData>
                {
                    new API.Contracts.v1.Requests.Device.CreateObservedDeviceData
                    {
                        DeviceID = CurrentDevice!.MachineID
                        , Cycles = Cycle
                    }
                }
                ,
                PersonID = MainObjects.Instance.CurrentUser!.PersonID
            });

            if (result.IsResponseWithData(this))
            {
                ClearAfterSave();
                InitData();
            }
        }

        #endregion

        #region METHOD ClearAfterSave

        private void ClearAfterSave()
        {
            CurrentDevice = null;
            Cycle = null;
        }

        #endregion

        #region METHOD CanSave

        private bool CanSave() =>
            currentDeviceHandler != null
            && currentDeviceHandler.IsNewCycleValid(Cycle);

        #endregion

        #region EVENT Vm_OnDeviceSelected

        private void Vm_OnDeviceSelected(object? sender, GetDeviceListResponse e)
        {
            if (e != null)
            {
                CurrentDevice = e;
            }
        }

        #endregion

        #region METHOD InitData

        public async void InitData()
        {
            var observedDeviceTask = deviceController.GetObserved(new API.Contracts.v1.Requests.Device.GetObservedRequest
            {
                Lang = MainObjects.Instance.Lang
                ,
                PersonID = MainObjects.Instance.CurrentUser!.PersonID
            });

            await Task.WhenAll(observedDeviceTask);

            CurrentObservedDeviceList = new List<GetObservedResponse>();
            ObservedDeviceHistoryList = new List<GetObservedResponse>();

            AnalyzeGetObservedResult(observedDeviceTask.Result);
        }

        #endregion

        #region METHOD AnalyzeGetObservedResult

        private void AnalyzeGetObservedResult(InvokeReturn<IEnumerable<GetObservedResponse>> observedDeviceResult)
        {
            if (observedDeviceResult.IsResponseWithData(this))
            {
                CurrentObservedDeviceList = observedDeviceResult.Data!
                    .Where(tt => tt.ListType == 1
                    && !GetObservedResponseHandler.IsAfterTime(tt))
                    .OrderByDescending(tt => tt.Select_Date)
                    .ToList();

                ObservedDeviceHistoryList = observedDeviceResult.Data!
                    .Where(tt => tt.ListType == 2
                    || GetObservedResponseHandler.IsAfterTime(tt))
                    .OrderByDescending(tt => tt.Select_Date)
                    .ToList();
            }          
        }

        #endregion
    }
}
