using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    internal class FilterBoolItem : FilterItem
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
                return GetDisplayTextValue();
            }
        }


        //TODO: get display text from app language resources
        protected string GetDisplayTextValue()
        {
            if (string.IsNullOrEmpty(Value))
            {
              //  Container.TextDictionaryResource.GetText("no");
                return Container.TextDictionaryResource.GetText("no");
            }

            return bool.Parse(Value) ? Container.TextDictionaryResource.GetText("yes"): Container.TextDictionaryResource.GetText("no"); 
        }

        public override void ClearValue()
        {
            Value = "false";
        }
    }
}