namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    //TODO split for short mould set detail
    public class GetDeviceListResponse
    {
        public int MachineID
        {
            get;
            set;
        }

        public string AssetNo
        {
            get;
            set;
        }

        public string AssetNoShort
        {
            get;
            set;
        }

        public string DeviceCategory
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string SerialNo
        {
            get;
            set;
        }

        public int StateID
        {
            get;
            set;
        }

        public int CategoryID
        {
            get;
            set;
        }

        public string DocumentationPath
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public bool? LocationRequired
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public string Place
        {
            get;
            set;
        }

        public bool? IsCritical
        {
            get;
            set;
        }

        public string SetName
        {
            get;
            set;
        }

        public int? SetID
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public long? Cycle
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }
    }
}
