namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetSparePartResponse
    {
        public int PartID { get; set; }
        public string PartNo { get; set; }
        public int PartGeneralCategoryID { get; set; }
        public int PartCategoryID { get; set; }
        public int? ManufacturerID { get; set; }
        public decimal? StockLevel { get; set; }
        public short AddPersonID { get; set; }
        public int UnitID { get; set; }
        public int WarehouseID { get; set; }
    }
}
