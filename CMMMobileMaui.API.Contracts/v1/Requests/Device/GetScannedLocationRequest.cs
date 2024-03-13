namespace CMMMobileMaui.API.Contracts.v1.Requests.Device
{
    public class GetScannedLocationRequest
    {
        public int LocationID
        {
            get;
            set;
        }

        public int? SubLocationID
        {
            get;
            set;
        }
    }
}
