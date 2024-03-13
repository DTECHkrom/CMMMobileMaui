namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class UpdateStocktakingRequest
    {
        public int StocktakingID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }
    }
}
