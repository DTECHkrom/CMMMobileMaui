namespace CMMMobileMaui.API.Contracts.v1.Responses.WO
{
    public class GetWOFilesResponse
    {
        public int WorkOrderDataID { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
        public string File_Name { get; set; }
        public string Person_Name { get; set; }
    }
}
