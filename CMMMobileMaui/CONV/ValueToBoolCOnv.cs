using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class ValueToBoolCOnv : IMarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null &&!string.IsNullOrEmpty(value.ToString()))
            {
                if (parameter == null)
                {
                    if (value.ToString() == "1")
                    {
                        return TranslateExtension.GetResourceText("yes");
                    }
                    else
                    {
                        return TranslateExtension.GetResourceText("no");
                    }
                }
                else if(parameter.ToString() == "bool")
                {
                    if(value.ToString() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public static ValueToBoolCOnv _conv;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new ValueToBoolCOnv();
            }

            return _conv;
        }
    }
}
