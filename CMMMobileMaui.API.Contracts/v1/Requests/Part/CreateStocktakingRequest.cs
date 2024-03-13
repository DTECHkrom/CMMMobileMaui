namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class CreateStocktakingRequest
    {
        public short PersonID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int WarehouseID
        {
            get;
            set;
        }
    }
}
