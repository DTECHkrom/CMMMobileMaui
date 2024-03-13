using CommunityToolkit.Mvvm.ComponentModel;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    public abstract class FilterItem : ObservableObject
    {
        #region FIELDS

        private FilterDisplayName filterDisplayName;

        #endregion

        #region PROPERTY Name

        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                filterDisplayName = new FilterDisplayName(value);
            }
        }

        #endregion

        #region PROPERTY Value

        private string valueItem;

        public string Value
        {
            get
            {
                return valueItem;
            }

            set => valueItem = value;
        }

        #endregion

        #region PROPERTY DisplayName

        public string DisplayName
        {
            get
            {
                return filterDisplayName?.GetDisplayName();
            }
        }

        #endregion

        #region PROPERTY FilterValue

        public abstract string FilterValue
        {
            get;
        }

        #endregion

        #region PROPERTY DataType
        public Type DataType { get; set; }

        #endregion

        #region PROPERTY DisplayValue

        public abstract string DisplayValue
        {
            get;
        }

        #endregion

        public FilterItem()
        {
        }

        #region PROPERTY IsInUse

        public bool IsInUse =>
            !string.IsNullOrEmpty(valueItem);

        #endregion

        #region METHOD ClearValue

        public abstract void ClearValue();

        #endregion
    }
}
