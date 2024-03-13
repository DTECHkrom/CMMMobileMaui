namespace CMMMobileMaui.API.Contracts.v1.Requests.Act
{
    public class CreateWOActivityRequest
    {
        private decimal workLoad;

        public int WorkOrderID
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
            get => workLoad;
            set => workLoad = value;
        }

        public int? DepartmentID
        {
            get;
            set;
        }
    }
}
