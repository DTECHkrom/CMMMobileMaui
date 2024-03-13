using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.SELECTORS
{
    public class DeviceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DeviceTemplate
        {
            get;
            set;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return DeviceTemplate;
        }
    }
}
