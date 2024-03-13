namespace CMMMobileMaui.API.Contracts.v1.Responses.Act
{
    public class CreateWOActivityResponse
    {
        public int ActivityID { get;set;}
        public int WorkOrderID { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public decimal Work_Load { get; set; }
        public decimal? Cost { get; set; }
        public byte Workers { get; set; }
    }
}
