using System;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class UpdateStocktakingEndRequest
    {
        public int StocktakingID
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
