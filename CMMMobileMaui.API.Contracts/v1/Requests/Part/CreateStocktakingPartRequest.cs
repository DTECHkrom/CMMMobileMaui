using System.Collections.Generic;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class CreateStocktakingPartRequest
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

        public List<PartStocktakingSingle> PartList
        {
            get;
            set;
        }
    }

    public class PartStocktakingSingle
    {
        public int PartID
        {
            get;
            set;
        }

        public decimal OldValue
        {
            get;
            set;
        }

        public decimal NewValue
        {
            get;
            set;
        }

        public int? CurrencyID
        {
            get;
            set;
        }

        public decimal? UnitPrice
        {
            get;
            set;
        }
    }
}
