using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    [ContentProperty("Text")]
    public class TranslateExtensionConv : IMarkupExtension, IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string text = value.ToString();

                if (parameter != null && !string.IsNullOrEmpty(parameter.ToString()))
                {
                    text = $"{parameter.ToString()}_{text}";
                }

                return TranslateExtension.GetResourceText(text.ToLowerInvariant()) ;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public static TranslateExtensionConv _con;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_con == null)
            {
                _con = new TranslateExtensionConv();
            }

            return _con;

        }
    }
}
