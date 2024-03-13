using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class DevicePlanListRequestHandler : GetWOsRequestBaseHandler
    {
        public DevicePlanListRequestHandler(GetWOsRequest getWOsRequest) : base(getWOsRequest)
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
            currentGetWOsRequest.Active = true;
            currentGetWOsRequest.IsPlan = true;
        }

        protected override void ClearFilters()
        {
            base.ClearFilters();
            currentGetWOsRequest.IsPlan = true;
        }

        public override GetWOsRequest GetWOsRequest()
        {
            currentGetWOsRequest.IsPlan = true;
            return currentGetWOsRequest;
        }

    }
}
