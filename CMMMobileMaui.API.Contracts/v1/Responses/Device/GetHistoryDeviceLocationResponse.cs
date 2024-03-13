using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetHistoryDeviceLocationResponse
    {
        public DateTime ChangeTime
        {
            get;
            set;
        }

        public string AddPerson
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }
    }
}
