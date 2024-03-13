namespace CMMMobileMaui.API.Contracts.v1.Responses.WO
{
    public class GetWOPartsResponse
    {
        public int PartID { get; set; }
        public string Part_No { get; set; }
        public string Description { get; set; }
        public decimal Amount_State_Change { get; set; }
        public string Person { get; set; }
        public string Stock_Change { get; set; }
        public string Warehouse { get; set; }
    }
}
