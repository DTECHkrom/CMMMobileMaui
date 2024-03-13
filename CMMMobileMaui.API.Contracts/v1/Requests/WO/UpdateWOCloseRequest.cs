namespace CMMMobileMaui.API.Contracts.v1.Requests.WO
{
    public class UpdateWOCloseRequest
    {
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

        public int ReasonID
        {
            get;
            set;
        }
    }
}
