using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class ValueToDecConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(!string.IsNullOrEmpty(value.ToString()))
                {
                    decimal dec = 0;

                    if(decimal.TryParse(value.ToString(), out dec))
                    {
                        return dec.ToString("0.##");
                    }

                    return value.ToString();
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public static ValueToDecConv _con;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_con == null)
            {
                _con = new ValueToDecConv();
            }

            return _con;
        }
    }
}
