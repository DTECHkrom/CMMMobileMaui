using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class PersonWOListRequestHandler : GetWOsRequestBaseHandler
    {
        public PersonWOListRequestHandler(GetWOsRequest getWOsRequest) : base(getWOsRequest)
        {
        }

        public override List<string> GetFiltersToRemove() =>
            new List<string>
            {
               "IsWithPerson"
            };

        public override void SetFilterList()
        {
            currentGetWOsRequest.IsWithPerson = true;
        }

        protected override void ClearFilters()
        {
            base.ClearFilters();
            currentGetWOsRequest.IsWithPerson = true;
        }

        public override GetWOsRequest GetWOsRequest()
        {
            SetFilterList();
            return currentGetWOsRequest;
        }
    }
}
