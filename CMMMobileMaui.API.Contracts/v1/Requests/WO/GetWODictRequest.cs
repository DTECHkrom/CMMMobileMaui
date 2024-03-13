namespace CMMMobileMaui.API.Contracts.v1.Requests.WO
{
    public class GetWODictRequest
    {
        public int DeviceCategoryID
        {
            get;
            set;
        }
        public short PersonID
        {
            get;
            set;
        }

        public string Lang
        {
            get;
            set;
        }

        public bool IsEdit
        {
            get;
            set;
        }
    }
}
