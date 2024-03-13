using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    internal class IsDemandTypeConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null
                && parameter != null)
            {
                if (value is Type type)
                {
                    var demadType = parameter.ToString();

                    if (demadType == "Date")
                    {
                        //if (type == typeof(DateTime))
                        //{
                        //    return true;
                        //}

                        var nullableType = Nullable.GetUnderlyingType(type);

                        if (nullableType != null)
                        {
                            if (nullableType == typeof(DateTime))
                            {
                                return true;
                            }
                        }
                    }
                    else if (demadType == "bool")
                    {
                        //if (type == typeof(bool))
                        //{
                        //    return true;
                        //}

                        var nullableType = Nullable.GetUnderlyingType(type);

                        if (nullableType != null)
                        {
                            if (nullableType == typeof(bool))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static IsDemandTypeConv _conv;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_conv == null)
            {
                _conv = new IsDemandTypeConv();
            }

            return _conv;
        }
    }
}
