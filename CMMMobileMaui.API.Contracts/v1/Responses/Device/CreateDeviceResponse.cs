using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class CreateDeviceResponse
    {
        public int MachineID
        {
            get;
            set;
        }

        public int CategoryID
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

        public int? MainLocationID
        {
            get;
            set;
        }

        public int? ManufacturerID
        {
            get;
            set;
        }

        public short AddPersonID
        {
            get;
            set;
        }

        public string SerialNo
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public DateTime? BuyDate
        {
            get;
            set;
        }
    }
}
