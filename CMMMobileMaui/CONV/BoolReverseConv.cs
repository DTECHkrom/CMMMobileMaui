using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class BoolReverseConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(bool.TryParse(value.ToString(), out bool a))
                {
                    return !a;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static BoolReverseConv _conv;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new BoolReverseConv();
            }

            return _conv;
        }
    }
}
