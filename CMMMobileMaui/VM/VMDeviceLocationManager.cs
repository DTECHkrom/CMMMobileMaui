using System.Text;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Requests.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.VM
{
    public class VMDeviceLocationManager : ScannerViewModelBase//, COMMON.IZebraScanPage
    {
        #region FIELDS

        private List<GetMainLocationResponse> mainLocations = new List<GetMainLocationResponse>();
        private GetMainLocationResponse? selectedLocation;

        #endregion

        #region PROPERTY HistoryList

        private List<GetHistoryDeviceLocationResponse>? historyList;

        public List<GetHistoryDeviceLocationResponse>? HistoryList
        {
            get => historyList;
            set => SetProperty(ref historyList, value);
        }

        #endregion

        #region PROPERTY CurrentDevice

        private GetDeviceListResponse? currentDevice;
        public GetDeviceListResponse? CurrentDevice
        {
            get => currentDevice;
            set => SetProperty(ref currentDevice, value);
        }

        private IDeviceController deviceCMMBLL;

        #endregion

        #region PROPERTY CurrentDeviceLocations

        private ObservableCollection<GetMainLocationResponse> currentDeviceLocations = new ObservableCollection<GetMainLocationResponse>();

        public ObservableCollection<GetMainLocationResponse> CurrentDeviceLocations
        {
            get => currentDeviceLocations;
            set => SetProperty(ref currentDeviceLocations, value);
        }

        #endregion

        #region PROPERTY Location

        private GetDeviceLocationResponse? location;

        public GetDeviceLocationResponse? Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        #endregion

        #region PROPERTY DefaulLocationName

        private string? defaulLocationName;

        public string? DefaulLocationName
        {
            get => defaulLocationName;
            set => SetProperty(ref defaulLocationName, value);
        }

        #endregion

        #region COMMAND ExecuteLocationCommand

        public ICommand ExecuteLocationCommand
        {
            get;
        }

        #endregion

        #region COMMAND DefaultLocationCommand

        public ICommand DefaultLocationCommand
        {
            get;
        }

        #endregion

        #region COMMAND HistoryCommand

        public ICommand HistoryCommand
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

        #region Cstr

        public VMDeviceLocationManager(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;


            SelectDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.DeviceSearchView vm = new VIEW.DeviceSearchView(true);
                    vm.OnDeviceSelected += Vm_OnDeviceSelected;
                    await OpenModalPage(vm);
                }
            });

            ExecuteLocationCommand = new Command(async (obj) =>
            {
                if (!CanClick())
                    return;

                if (obj is GetMainLocationResponse loc)
                {
                    selectedLocation = loc;

                    if (loc.IsWriteText.HasValue && loc.IsWriteText.Value)
                    {

                    }
                    else if (!string.IsNullOrEmpty(loc.TabSource))
                    {
                        VIEW.BaseListView baseView = new VIEW.BaseListView();
                        baseView.OnPageUnload += BaseView_OnClose;
                        var vm = (VMBaseList)baseView.BindingContext;

                        vm.SetScanTypes(new List<IScanType>() { GetScanDeviceLocationType() });
                        vm.OnSelectedItem += Vm_OnSelectedItem;

                        await vm.SetMainData(loc.LocationID, loc.Name);

                        await OpenModalPage(baseView);                    
                    }
                    else
                    {
                        if (CurrentDevice != null)
                            return;

                        bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                        if (dialogResult)
                        {
                            var temp = new UpdateDeviceLocationRequest();
                            temp.MachineID = CurrentDevice!.MachineID;
                            temp.LocationID = loc.LocationID;
                            temp.PersonID = API.MainObjects.Instance.CurrentUser!.PersonID;

                            ChangeDeviceLocation(temp);
                        }                      
                    }
                }
            });

            DefaultLocationCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if(CurrentDevice is null)
                    {
                        return;
                    }

                    bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                    if (dialogResult)
                    {
                        var temp = new UpdateDeviceLocationRequest();
                        temp.MachineID = CurrentDevice.MachineID;
                        temp.LocationID = Location!.DefaultLocationID!.Value;

                        if (Location.DefaultSubLocationID.HasValue)
                        {
                            temp.SubLocationID = Location.DefaultSubLocationID;
                            temp.Place = Location.DefaultPlace;
                        }

                        temp.PersonID = API.MainObjects.Instance.CurrentUser!.PersonID;

                        ChangeDeviceLocation(temp);
                    }
                }
            });

            HistoryCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    var view = new BIANOR.MouldChangeHistoryView();
                    view.BindingContext = this;

                    var historyResponse = await deviceCMMBLL.GetHistLoc(new GetByDeviceIDRequest
                    {
                        DeviceID = CurrentDevice!.MachineID
                    });

                    if (historyResponse.IsResponseWithData(this))
                    {
                        HistoryList = historyResponse.Data!
                        .OrderByDescending(tt => tt.ChangeTime)
                        .ToList();

                        await OpenModalPage(view);
                    }
                }
            });
        }

        private void BaseView_OnClose(object? sender, EventArgs e)
        {
            selectedLocation = null;
        }

        #endregion

        #region EVENT Vm_OnDeviceSelected

        private void Vm_OnDeviceSelected(object? sender, GetDeviceListResponse e)
        {
            CurrentDevice = e;
            SetDeviceInfo();
        }

        #endregion

        #region EVENT Vm_OnSelectedItem

        private async void Vm_OnSelectedItem(object? sender, GetDeviceSubLocationResponse e)
        {
            if (e != null)
            {
                bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure") + $"\n[{selectedLocation?.Name}][{e.Name}]");

                if (dialogResult)
                {
                    var temp = new UpdateDeviceLocationRequest();
                    temp.MachineID = CurrentDevice!.MachineID;
                    temp.LocationID = selectedLocation!.LocationID;
                    temp.SubLocationID = e.ID;
                    temp.Place = e.Name;
                    temp.PersonID = API.MainObjects.Instance.CurrentUser!.PersonID;

                    await Shell.Current.Navigation.PopModalAsync();
                    ChangeDeviceLocation(temp);
                }
            }
        }

        #endregion

        #region METHOD GetScanDeviceLocationType

        public IScanType GetScanDeviceLocationType()
        {
            var deviceScan = new DeviceLocationScan(selectedLocation!, App.CurrentScanManager);

            deviceScan.UIMethod = async (obj) =>
            {
                if (obj is GetScannedLocationResponse scanedLocation)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"{CONV.TranslateExtension.GetResourceText("dev_loc_new")}: ");
                    sb.Append($"[{scanedLocation.LocationName}]");

                    if (!string.IsNullOrEmpty(scanedLocation.Place))
                    {
                        sb.Append($"[{scanedLocation.Place}]");
                    }

                    bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog
                        , CONV.TranslateExtension.GetResourceText("q_sure")
                        + "\n" + sb.ToString());

                    if (dialogResult)
                    {
                        UpdateDeviceLocationRequest updateDeviceLocationRequest = new UpdateDeviceLocationRequest();
                        updateDeviceLocationRequest.LocationID = scanedLocation.LocationID;
                        updateDeviceLocationRequest.SubLocationID = scanedLocation.SubLocationID;
                        updateDeviceLocationRequest.Place = scanedLocation.Place;

                        ChangeDeviceLocation(updateDeviceLocationRequest);

                        if (Shell.Current.Navigation.ModalStack.Count == 1)
                        {
                            await Shell.Current.Navigation.PopModalAsync();
                        }
                    }
                }
            };

            return deviceScan;
        }

        #endregion

        #region METHOD InitData

        public async Task InitData()
        {
            var locationResponse = await deviceCMMBLL.GetMainLocation();

            if (locationResponse.IsResponseWithData(this))
            {
                mainLocations = locationResponse.Data!.ToList();
            }
        }


        #endregion

        #region METHOD SetDeviceInfo

        public async void SetDeviceInfo()
        {
            CurrentDeviceLocations.Clear();
            DefaulLocationName = string.Empty;

            if (CurrentDevice == null)
            {
                Location = null;
            }
            else
            {
                var locationResponse = await deviceCMMBLL.GetLocation(new GetByDeviceIDRequest
                {
                    DeviceID = currentDevice!.MachineID
                });

                if (locationResponse.IsResponseWithData(this))
                {
                    Location = locationResponse.Data!;

                    if (mainLocations != null
                        && mainLocations.Count > 0)
                    {
                        var items = mainLocations.Where(tt => !tt.CategoryID.HasValue || tt.CategoryID.Value == CurrentDevice.CategoryID).ToList();

                        if (items.Count > 0)
                        {
                            items.ForEach(CurrentDeviceLocations.Add);
                        }

                        if (Location != null)
                        {
                            if (!string.IsNullOrEmpty(Location.DefaultLocationName))
                            {
                                DefaulLocationName = $"[{Location.DefaultLocationName}]";

                                if (!string.IsNullOrEmpty(Location.DefaultPlace))
                                {
                                    DefaulLocationName += $"[{Location.DefaultPlace}]";
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region METHOD ChangeDeviceLocation

        private async void ChangeDeviceLocation(UpdateDeviceLocationRequest loc)
        {
            loc.PersonID = API.MainObjects.Instance.CurrentUser!.PersonID;
            loc.MachineID = CurrentDevice!.MachineID;

            var updateResponse = await deviceCMMBLL.UpdateLoc(loc);

            if (updateResponse.IsValid)
            {
                selectedLocation = null;
                SetDeviceInfo();
            }
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            if (Shell.Current.Navigation.ModalStack.Count == 0)
            {
                var device = new DeviceScan();

                device.UIMethod = (obj) =>
                {
                    if (obj is GetDeviceListResponse dev)
                    {
                        CurrentDevice = dev;
                        SetDeviceInfo();
                    }
                };

                scanTypes.Add(device);

                scanTypes.Add(GetScanDeviceLocationType());
            }

            return scanTypes;
        }

        #endregion

    }
}
