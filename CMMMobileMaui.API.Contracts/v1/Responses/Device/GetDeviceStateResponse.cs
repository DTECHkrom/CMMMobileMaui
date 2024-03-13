using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetDeviceStateResponse
    {
        public string State_Name
        {
            get;
            set;
        }

        public DateTime Change_Time
        {
            get;
            set;
        }

        public long State_Time
        {
            get;
            set;
        }

        public int StateID
        {
            get;
            set;
        }
    }
}
