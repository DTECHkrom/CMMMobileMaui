using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    internal class FilterBoolNullItem : FilterItem
    {
        public override string FilterValue =>
            Value;

        public override string DisplayValue =>
            GetDisplayTextValue();

        public override void ClearValue()
        {
            Value = string.Empty;
        }

        //TODO get display text from app language resources
        protected string GetDisplayTextValue()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return string.Empty;
            }

            return bool.Parse(Value) ? Container.TextDictionaryResource.GetText("yes") : Container.TextDictionaryResource.GetText("no");
        }
    }
}