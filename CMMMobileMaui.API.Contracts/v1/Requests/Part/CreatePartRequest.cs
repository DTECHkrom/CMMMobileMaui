namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class CreatePartRequest
    {
        #region PROPERTY WarehouseID

        public int WarehouseID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PartNo

        public string PartNo
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY StockLevel

        public decimal StockLevel
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY StockLevelTarget

        public decimal? StockLevelTarget
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Description

        public string Description
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY UnitPrice

        public decimal? UnitPrice
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrencyID

        public int CurrencyID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsExchangeable

        public bool IsExchangeable
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Image

        public byte[] Image
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Producent_Code

        public string ProducentCode
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY GeneralCategoryID

        public int GeneralCategoryID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CategoryID

        public int CategoryID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ProducentID

        public int? ProducentID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY UnitID

        public int UnitID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PersonID

        public short PersonID
        {
            get;
            set;
        }

        #endregion
    }
}
