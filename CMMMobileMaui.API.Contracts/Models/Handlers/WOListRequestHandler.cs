using System.Collections.Generic;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class WOListRequestHandler : GetWOsRequestBaseHandler
    {
        public WOListRequestHandler(GetWOsRequest getWOsRequest) : base(getWOsRequest)
        {
        }

        public override List<string> GetFiltersToRemove() =>
            new List<string>();

        public override void SetFilterList()
        {
            currentGetWOsRequest.Active = true;
            currentGetWOsRequest.IsDep = true;
        }
    }
}
