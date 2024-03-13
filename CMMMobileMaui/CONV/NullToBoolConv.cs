using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class NullToBoolConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (value != null)
                {
                    return false;
                }

                return true;
            }
            else
            {
                if (value != null)
                {
                    if (value is int?)
                    {
                        return ((int?)value).HasValue;
                    }
                    else if (value is decimal?)
                    {
                        return ((decimal?)value).HasValue;
                    }
                    else if (value is DateTime?)
                    {
                        return ((DateTime?)value).HasValue;

                    }
                    else if (value is string)
                    {
                        return !string.IsNullOrEmpty(value.ToString());
                    }
                }

                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static NullToBoolConv _conv;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new NullToBoolConv();
            }

            return _conv;
        }
    }
}
