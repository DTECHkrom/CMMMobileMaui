using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class IsEqualMultiConv : IMultiValueConverter, IMarkupExtension
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values != null
                && values.Length == 2)
            {
                if (values[0] != null
                && values[1] != null)
                {
                    return values[0].ToString().ToLowerInvariant() == values[1].ToString().ToLowerInvariant();
                }
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static IsEqualMultiConv _conv = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new IsEqualMultiConv();
            }

            return _conv;
        }
    }
}
