namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetPartDetailResponse
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

        public string UnitName
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public byte[]? Picture
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

        public decimal? StockMaxTarget
        {
            get;
            set;
        }

        public decimal? StockMinTarget
        {
            get;
            set;
        }

        public string GeneralCategory
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string Warehouse
        {
            get;
            set;
        }

        public decimal? CurrentNeeds
        {
            get;
            set;
        }

        public decimal? OrderedQuantity
        {
            get;
            set;
        }

        public string DefaultSupplier
        {
            get;
            set;
        }

        public bool LockedOrders
        {
            get;
            set;
        }

        public bool Obsolete
        {
            get;
            set;
        }

        public string ManufacturerCode
        {
            get;
            set;
        }

        public string Producent
        {
            get;
            set;
        }

        public string Locations
        {
            get;
            set;
        }

        public bool IsExchangeable
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
