using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.VM
{
    public class VMObservedDeviceCycles : COMMON.ViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceController;

        #endregion

        #region PROPERTY ItemsList

        private List<GetDeviceObservedCyclesResponse> itemsList;
        public List<GetDeviceObservedCyclesResponse> ItemsList
        {
            get => itemsList;
            set => SetProperty(ref itemsList, value);
        }

        #endregion

        #region PROPERTY CurrentMachineID

        public int CurrentMachineID
        {
            get;
            set;
        }

        #endregion

        #region Cstr

        public VMObservedDeviceCycles(IDeviceController deviceController)
        {
            this.deviceController = deviceController;
        }

        #endregion

        #region METHOD InitData

        public async void InitData()
        {
            if (CurrentMachineID == 0)
                return;

            var result = await deviceController.GetObservedCycles(new API.Contracts.v1.Requests.Device.GetByDeviceIDRequest
            {
                DeviceID = CurrentMachineID
            });

            if (result.IsValid)
            {
                ItemsList = result.Data.ToList();
            }
        }

        #endregion
    }
}
