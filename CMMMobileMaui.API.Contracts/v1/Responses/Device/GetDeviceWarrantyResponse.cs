using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetDeviceWarrantyResponse
    {
        public int WarrantyID
        {
            get;
            set;
        }
        public string Name 
        {
            get;
            set;
        } 
        
        public string Description
        {
            get;
            set;
        }
        
        public string Param1
        {
            get;
            set;
        }
        
        public string Param2
        {
            get; 
            set;
        }
        
        public DateTime? Buy_Date
        {
            get;
            set;
        }

        public long? Current_Cycles
        {
            get;
            set;
        }
    }
}
