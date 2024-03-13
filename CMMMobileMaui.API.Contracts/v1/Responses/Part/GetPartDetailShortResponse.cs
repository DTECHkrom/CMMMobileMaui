namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetPartDetailShortResponse
    {
        public int PartID
        {
            get;
            set;
        }

        public string PartNo
        {
            get;
            set;
        }

        public decimal? StockLevel
        {
            get;
            set;
        }

        public decimal? StockLevelTarget
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public decimal? UnitPrice
        {
            get;
            set;
        }

        public int? CurrencyValueID
        {
            get;
            set;
        }

        public int WarehouseID
        {
            get;
            set;
        }

        public string UnitName
        {
            get;
            set;
        }

        public string CurrencyName
        {
            get;
            set;
        }

        public bool IsExchangeable
        {
            get;
            set;
        }
    }
}
