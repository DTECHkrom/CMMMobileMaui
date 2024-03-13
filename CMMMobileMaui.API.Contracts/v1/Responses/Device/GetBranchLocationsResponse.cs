namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetBranchLocationsResponse
    {
        public int BranchID
        {
            get;
            set;
        }

        public string Branch_Name
        {
            get;
            set;
        }

        public int MainLocationID
        {
            get;
            set;
        }

        public string Location_Name
        {
            get;
            set;
        }
    }
}
