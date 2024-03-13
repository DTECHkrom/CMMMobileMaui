namespace CMMMobileMaui.API.Contracts.v1.Requests.Device
{
    public class UpdateDeviceLocationRequest
    {
        public int MachineID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

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

        public string Place
        {
            get;
            set;
        }
    }
}
