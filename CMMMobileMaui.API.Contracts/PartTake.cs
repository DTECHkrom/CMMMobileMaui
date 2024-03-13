namespace CMMMobileMaui.API.Contracts
{
    public enum PartTakeState
    {
        Valid,
        Empty,
        Amount
    }
    public class PartTake
    {
        #region PROPERTY PartID

        public int PartID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WorkOrderID

        public int? WorkOrderID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Amount

        public decimal Amount
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PersonID

        public int PersonID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceID

        public int? DeviceID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY State

        public PartTakeState State
        {
            get;
            set;
        } = PartTakeState.Empty;

        #endregion

        #region PROPERTY OtherPersonID

        public int? OtherPersonID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY StockLevelChangeID

        public int StockLevelChangeID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY OldAmount

        public decimal OldAmount
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY _PartNo

        public string _PartNo
        {
            get;
            set;
        }

        #endregion

        #region PROPRTY IsExchangeable

        public bool IsExchangeable
        {
            get;
            set;
        }

        #endregion
    }
}
