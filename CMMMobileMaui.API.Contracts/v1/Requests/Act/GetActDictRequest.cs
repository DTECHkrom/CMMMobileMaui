namespace CMMMobileMaui.API.Contracts.v1.Requests.Act
{
    public class GetActDictRequest
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
    }
}
