using System;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    internal class FilterDateItem : FilterItem
    {
        public override string FilterValue => Value;

        public override string DisplayValue => GetDisplayTextValue();

        public override void ClearValue()
        {
            Value = string.Empty;
        }

        protected string GetDisplayTextValue()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return string.Empty;
            }

            return DateTime.Parse(Value).ToString("yyyy-MM-dd");
        }
    }
}