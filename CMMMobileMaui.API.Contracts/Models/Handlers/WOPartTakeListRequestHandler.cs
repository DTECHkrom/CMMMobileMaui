using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class WOPartTakeListRequestHandler : GetWOsRequestBaseHandler
    {
        private readonly int deviceId;
        public WOPartTakeListRequestHandler(GetWOsRequest getWOsRequest, int deviceId) : base(getWOsRequest)
        {
            this.deviceId = deviceId;
        }

        public override List<string> GetFiltersToRemove() =>
            new List<string>
            {
                "DeviceName"
            };

        public override void SetFilterList()
        {
            currentGetWOsRequest.DeviceID= deviceId;
        }
        protected override void ClearFilters()
        {
            base.ClearFilters();
            SetFilterList();
        }

        public override GetWOsRequest GetWOsRequest()
        {
            SetFilterList();
            return currentGetWOsRequest;
        }

    }
}
