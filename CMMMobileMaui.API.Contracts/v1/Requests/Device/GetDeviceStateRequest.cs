using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Device
{
    public class GetDeviceStateRequest
    {
        public int MachineID
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
