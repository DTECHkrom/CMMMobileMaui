namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class GetPartDetailShortRequest
    {
        public string Filter
        {
            get;
            set;
        }

        public int? WarID
        {
            get;
            set;
        }

        public int? PartID
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
