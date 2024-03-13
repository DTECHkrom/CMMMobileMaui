using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using System.Windows.Input;

namespace CMMMobileMaui.BIANOR
{
    public class VMMouldCalendarChange : ScannerViewModelBase
    {

        #region PROEPRTY CurrentDevice
        public GetDeviceListResponse CurrentDevice { get; private set; }

        private IDeviceController deviceCMMBLL;

        #endregion

        #region COMMAND SelectDeviceCommand

        public ICommand SelectDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCurrentDataCommand

        public ICommand SaveCurrentDataCommand
        {
            get;
        }

        #endregion

        #region PROPERTY CurrentData

        public GetRostiMouldCalendarResponse CurrentData
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY NewCalendarYear

        public int? NewCalendarYear
        {
            get;
            set;
        }

        #endregion

        #region Cstr

        public VMMouldCalendarChange(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;

            SelectDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.DeviceSearchView vm = new VIEW.DeviceSearchView(true, 9);
                    vm.OnDeviceSelected += Vm_OnDeviceSelected;
                    await OpenModalPage(vm);
                }
            });

            SaveCurrentDataCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentDevice == null)
                        return;

                    if (NewCalendarYear.HasValue)
                    {
                        //var data = new UpdateMouldCalendarRequest();
                        //data.D = CurrentDevice.MachineID;
                        //data.Calendar_Year = NewCalendarYear;

                        var updateResponse = await deviceCMMBLL.UpdateMouldCal(new API.Contracts.v1.Requests.Device.UpdateMouldCalendarRequest
                        {
                            CalendarYear = NewCalendarYear.Value,
                            MachineID = CurrentDevice.MachineID
                        }); ;

                        if (updateResponse.IsValid
                        && updateResponse.Data)
                        {
                            SetDeviceInfo();
                            NewCalendarYear = null;
                        }

                        OnPropertyChanged("CurrentData");
                        OnPropertyChanged("NewCalendarYear");
                    }
                }
            });
        }


        #endregion

        #region EVENT Vm_OnDeviceSelected
        private void Vm_OnDeviceSelected(object? sender, GetDeviceListResponse e)
        {
            CurrentDevice = e;
            OnPropertyChanged("CurrentDevice");
            SetDeviceInfo();
        }

        #endregion

        #region METHOD SetDeviceInfo

        private async void SetDeviceInfo()
        {
            var mouldResponse = await deviceCMMBLL.GetMouldCal(new API.Contracts.v1.Requests.Device.GetByDeviceIDRequest
            {
                DeviceID = CurrentDevice.MachineID
            });

            if (mouldResponse.IsValid)
            {
                CurrentData = mouldResponse.Data;
            }

            OnPropertyChanged("CurrentData");
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            var deviceScan = new DeviceScan();
            deviceScan.UIMethod = (obj) =>
            {
                if (obj is GetDeviceListResponse dev)
                {
                    if (dev.CategoryID == BIANORConsts.MouldCategoryID)
                    {
                        CurrentDevice = dev;
                        SetDeviceInfo();
                    }
                }
            };

            scanTypes.Add(deviceScan);

            return scanTypes;
        }

        #endregion
    }
}
