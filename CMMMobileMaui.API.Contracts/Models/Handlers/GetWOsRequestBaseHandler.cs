using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public abstract class GetWOsRequestBaseHandler
    {
        protected GetWOsRequest currentGetWOsRequest;

        public GetWOsRequestBaseHandler(GetWOsRequest getWOsRequest)
        {
            currentGetWOsRequest = getWOsRequest;
        }

        public abstract void SetFilterList();

        public abstract List<string> GetFiltersToRemove();

        protected virtual void ClearFilters()
        {
            currentGetWOsRequest.Active = null;
            currentGetWOsRequest.DeviceName = string.Empty;
            currentGetWOsRequest.IsAssignedPerson = null;
            currentGetWOsRequest.IsDep = null;
            currentGetWOsRequest.IsGroup = null;
            currentGetWOsRequest.IsObserved = null;
            currentGetWOsRequest.IsPlan = null;
            currentGetWOsRequest.IsScheduled = null;
            currentGetWOsRequest.IsWithPerson = null;
            currentGetWOsRequest.IsTakenPerson = null;
        }

        public void AfterFilterChange(GetWOsRequest getWOsRequest)
        {
            if (getWOsRequest == null)
            {
                ClearFilters();
                return;
            }

            currentGetWOsRequest = getWOsRequest;
        }

        public virtual GetWOsRequest GetWOsRequest() =>
            currentGetWOsRequest;

        public void SetBaseUnselectableFilter(short? personID
            , int? deviceID
            , int? workorderID
            , string lang)
        {
            currentGetWOsRequest.PersonID = personID;
            currentGetWOsRequest.DeviceID = deviceID;
            currentGetWOsRequest.WorkOrderID = workorderID;
            currentGetWOsRequest.Lang = lang;
        }
    }
}
