namespace CMMMobileMaui.API.Contracts.v1.Requests.Device
{
    public class UpdateDeviceImageRequest
    {
        public int DeviceID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public byte[]? Image
        {
            get;
            set;
        }
    }
}
