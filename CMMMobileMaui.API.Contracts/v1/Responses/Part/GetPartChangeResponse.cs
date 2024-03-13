using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetPartChangeResponse
    {
        public int PartID { get; set; }
        public decimal AmountStateChange { get; set; }
        public DateTime AddDate { get; set; }
        public string DeviceWO { get; set; }
        public string AddPerson { get; set; }
        public string TakePerson { get; set; }
        public string DeviceTake { get; set; }
        public string ChangeType { get; set; }
        public int StockLevelChangeID { get; set; }
        public int StockChangeTypeID { get; set; }
    }
}
