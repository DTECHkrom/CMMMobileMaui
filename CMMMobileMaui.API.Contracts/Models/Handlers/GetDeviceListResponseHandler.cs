using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class GetDeviceListResponseHandler
    {
        private GetDeviceListResponse getDeviceListResponse;

        public GetDeviceListResponseHandler()
        {            
        }

        public GetDeviceListResponseHandler(GetDeviceListResponse getDeviceListResponse)
        {
            this.getDeviceListResponse = getDeviceListResponse;
        }

        public void SetDeviceListResponse(GetDeviceListResponse getDeviceListResponse) =>
            this.getDeviceListResponse = getDeviceListResponse;

        public GetDeviceListResponse GetDeviceListResponse() =>
            getDeviceListResponse;

        public bool IsNewCycleValid(long? cycle) =>
            cycle.HasValue == false? false
            : getDeviceListResponse.Cycle.HasValue == false ? true
            : getDeviceListResponse.Cycle.Value <= cycle;


        public static implicit operator GetDeviceListResponseHandler(GetDeviceListResponse getDeviceListResponse) => 
            new GetDeviceListResponseHandler(getDeviceListResponse);

        public static implicit operator GetDeviceListResponse(GetDeviceListResponseHandler getDeviceListResponseHandler) =>
            getDeviceListResponseHandler.GetDeviceListResponse();
             
    }
}
