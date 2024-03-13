namespace CMMMobileMaui.API.Contracts.v1.Requests.Plan
{
    public class CreatePlanActivityRequest
    {
        public int WorkOrderID
        {
            get;
            set;
        }

        public int PlanTaskActivityID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public int CategoryID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public decimal WorkLoad
        {
            get;
            set;
        }
    }
}
