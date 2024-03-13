namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetScannedLocationResponse
    {
        public int LocationID
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public int? SubLocationID
        {
            get;
            set;
        }

        public string Place
        {
            get;
            set;
        }
    }
}
