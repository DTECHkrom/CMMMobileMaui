namespace CMMMobileMaui.API.Contracts.v1.Responses.Plan
{
    public class GetWOPlanActsResponse
    {
        public int WorkOrderID
        {
            get;
            set;
        }

        public int? GroupIndexID
        {
            get;
            set;
        }

        public int ActivityIndex
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public string ActivityName
        {
            get;
            set;
        }

        public int? WorkOrderActivityID
        {
            get;
            set;
        }

        public int PlanTaskActivityID
        {
            get;
            set;
        }

        public int WOACategoryID
        {
            get;
            set;
        }

        public decimal? _WorkLoad
        {
            get;
            set;
        }

        public string _Description
        {
            get;
            set;
        }
    }
}
