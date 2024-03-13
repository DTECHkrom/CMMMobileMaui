using Telerik.Maui;

namespace CMMMobileMaui.COMMON
{
    public class CustomTelerikLocalizationManager : TelerikLocalizationManager
    {
        public override string GetString(string key)
        {
            var value = CONV.TranslateExtension.GetResourceText(key);

            if (string.IsNullOrEmpty(value)
                || value == key)
            {
                return base.GetString(key);
            }
            else
            {
                return value;
            }
        }
    }
}
