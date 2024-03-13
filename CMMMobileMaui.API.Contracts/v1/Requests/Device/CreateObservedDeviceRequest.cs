using System.Collections.Generic;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Device
{
    public class CreateObservedDeviceRequest
    {
        public List<CreateObservedDeviceData> MachineIDs { get; set; }
        public short PersonID { get; set; }
    }

    public class CreateObservedDeviceData
    {
        public int DeviceID { get; set; }
        public long? Cycles { get; set; }
    }
}
