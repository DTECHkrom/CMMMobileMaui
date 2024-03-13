using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetDeviceObservedCyclesResponse
    {
        public string PersonName { get; set; }
        public DateTime ChangeTime { get; set; }
        public string Cycle { get; set; }
    }
}
