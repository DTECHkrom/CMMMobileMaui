using System.Collections.Generic;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Device
{
    public class GetDeviceListRequest
    {
        public string Name
        {
            get;
            set;
        }

        public List<int> MachineIDs
        {
            get;
            set;
        }

        public int? CategoryID
        {
            get;
            set;
        }

        public string Lang
        {
            get;
            set;
        }

        public bool? IsSet
        {
            get;
            set;
        }

        public bool? IsCatWithCycle
        {
            get;
            set;
        }
    }
}
