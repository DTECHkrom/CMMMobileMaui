namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    internal class FilterStringItem : FilterItem
    {
        public override string FilterValue
        {
            get
            {
                return Value;
            }
        }

        public override string DisplayValue
        {
            get
            {
                return Value;
            }
        }

        public override void ClearValue()
        {
            Value = string.Empty;
        }
    }
}