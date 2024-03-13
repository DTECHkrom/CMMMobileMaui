using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class DisplayValueConv : IMultiValueConverter, IMarkupExtension
    {
        private static DisplayValueConv _conv;
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new DisplayValueConv();
            }

            return _conv;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                if (values.Length == 2)
                {
                    var item = values[0];

                    if (item != null)
                    {
                        var path = values[1].ToString();

                        //if displayPathIsSet
                        if (!string.IsNullOrEmpty(path))
                        {
                            return item.GetType()
                                .GetProperty(path)
                                .GetValue(item)
                                .ToString();
                        }

                        return item.ToString();
                    }
                }
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
