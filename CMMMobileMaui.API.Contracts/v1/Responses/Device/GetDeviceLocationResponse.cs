namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetDeviceLocationResponse
    {
        public string LocationName
        {
            get;
            set;
        }

        public int? LocationID
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
        
        public string DefaultLocationName
        {
            get;
            set;
        }

        public int? DefaultLocationID
        {
            get;
            set;
        }
        
        public int? DefaultSubLocationID
        {
            get;
            set;
        }

        public string DefaultPlace
        {
            get;
            set;
        }
    }
}
