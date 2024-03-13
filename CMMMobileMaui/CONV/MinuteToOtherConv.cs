using System;
using System.Globalization;
using CMMMobileMaui.COMMON;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class MinuteToOtherConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is long)
            {
                long minutes = long.Parse(value.ToString());

                return SConsts.GetTimeTextFromMinutes(minutes);
            }

            return " [0]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
