namespace CMMMobileMaui.API.Contracts.v1.Responses.Set
{
    public class GetSetsResponse
    {
        private int? display_Index;

        public string Branch_Name { get; set;}
        public string Location_Name { get; set; }
        public string Set_Name { get; set; }
        public int SetID { get; set; }
        public int? Display_Index 
        { 
            get => display_Index ?? 0; 
            set => display_Index = value; 
        }
    }
}
