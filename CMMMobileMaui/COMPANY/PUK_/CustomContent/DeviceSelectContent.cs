using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.Models.Handlers;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMPANY.ExtraContent;
using CMMMobileMaui.VIEW.AFTERLOGIN;
using static CMMMobileMaui.API.Contracts.Models.COMMON.Enums;

namespace CMMMobileMaui.COMPANY.PUK_.CustomContent
{
    public class DeviceSelectContent : AfterLoginExtraContent
    {
        private readonly IDeviceController deviceController;


        public DeviceSelectContent() : base("sh_dc", "truck", typeof(DeviceSelectPage))
        {
            deviceController = (IDeviceController)API.MainObjects.Instance.ServiceProvider.GetService(typeof(IDeviceController));
        }

        public override async ValueTask<bool> ShouldOpenOnStart()
        {
            if (API.MainObjects.Instance.CurrentUser == null)
                return false;

            if (!API.MainObjects.Instance.CurrentUser.GetUserRightResponse.MD_Add_ForceCycle)
                return false;

            var isObserved = await IsUserObservedDevicesExists();          

            return isObserved; 
        }

        private async Task<bool> IsUserObservedDevicesExists()
        {
            var observedDevices = await deviceController.GetObserved(new API.Contracts.v1.Requests.Device.GetObservedRequest
            {
                PersonID = API.MainObjects.Instance.CurrentUser.PersonID
                ,
            });

            if(observedDevices.IsValid)
            {
                if (IsNoActiveObservedDevice(observedDevices.Data))
                    return true;

                foreach(var observedDevice in observedDevices.Data.Where(tt=> tt.Select_Date.HasValue))
                {
                    if(GetObservedResponseHandler.IsAfterTime(observedDevice))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsNoActiveObservedDevice(IEnumerable<GetObservedResponse> observedDevices) =>
            observedDevices == null
                    || observedDevices.Where(tt => tt.ListType == (int)ObservedDeviceListType.Active
                    && !GetObservedResponseHandler.IsAfterTime(tt))
                    .Count() == 0;
    }
}
