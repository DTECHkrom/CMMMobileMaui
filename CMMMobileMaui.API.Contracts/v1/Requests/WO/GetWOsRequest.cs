using System.Linq;
using CMMMobileMaui.API.Contracts.Models.COMMON;
using CMMMobileMaui.API.Contracts.Models.GUI;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;

namespace CMMMobileMaui.API.Contracts.v1.Requests.WO
{
    public class GetWOsRequest: SortableItem
    {
        [FilterIgnore]
        public short? PersonID
        {
            get;
            set;
        }

        public string DeviceName
        {
            get;
            set;
        }
        public bool? Active
        {
            get;
            set;
        }
        public bool? IsDep
        {
            get;
            set;
        }

        public bool? IsAssignedPerson
        {
            get;
            set;
        }

        public bool? IsWithPerson
        {
            get;
            set;
        }

        public bool? IsPlan
        {
            get;
            set;
        }

        [FilterIgnore]
        public int? DeviceID
        {
            get;
            set;
        }

        [FilterIgnore]
        public int? WorkOrderID
        {
            get;
            set;
        }

        [FilterIgnore]
        public string Lang
        {
            get;
            set;
        }        

        public bool? IsGroup
        {
            get;
            set;
        }

        public bool? IsObserved
        {
            get;
            set;
        }

        [FilterIgnore]
        public bool? IsScheduled
        {
            get;
            set;
        }

        public bool? IsTakenPerson
        {
            get;
            set;
        }

        public GetWOsRequest()
        {
            SortingPropertyList = SMethods.GetSortItemList<GetWOsResponse>();
            DefaultSortItem = SortingPropertyList.FirstOrDefault(tt => tt.PropertyName == "Mod_Date");
        }   
    }
}
