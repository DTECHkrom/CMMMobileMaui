using Newtonsoft.Json;
using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetObservedResponse
    {
        public int ListType { get; set; }
        public int MachineID { get; set; }
        public string Asset_No { get; set; }
        public string Asset_No_Short { get; set; }
        public string Category_Name { get; set; }
        public DateTime? Select_Date { get; set; }
        public long? Cycles { get; set; }
        public string Select_Cycles { get; set; }

        [JsonIgnore]
        public bool _IsOverTime { get; set; }
    }
}
