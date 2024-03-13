using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class DeviceWOListRequestHandler : GetWOsRequestBaseHandler
    {
        public DeviceWOListRequestHandler(GetWOsRequest getWOsRequest) : base(getWOsRequest)
        {
        }

        public override List<string> GetFiltersToRemove() =>
            new List<string>
            {
                "IsPlan"
                , "DeviceName"
                , "IsObserved"
                , "IsGroup"
            };

        public override void SetFilterList()
        {

        }
    }
}
