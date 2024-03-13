namespace CMMMobileMaui.API.Contracts.v1.Requests.ActPer
{
    public class CreateWOActivityPersonRequest
    {
        public int ActivityID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public decimal Work_Load
        {
            get;
            set;
        }
    }
}
