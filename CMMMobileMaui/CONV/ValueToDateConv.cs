using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class ValueToDateConv : IMarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        return DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd");
                    }
                }
                catch (Exception) { }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static ValueToDateConv _conv = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_conv == null)
            {
                _conv = new ValueToDateConv();
            }

            return _conv;
        }
    }
}
