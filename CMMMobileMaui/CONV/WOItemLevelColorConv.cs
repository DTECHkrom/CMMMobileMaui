using System;
using System.Globalization;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class WOItemLevelColorConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(value is int)
                {
                    var levelID = (int)value;

                    if(levelID == 1)
                    {
                        return Colors.Red;
                    }
                    else if(levelID == 2)
                    {
                        return Colors.Yellow;
                    }
                    else if(levelID == 3)
                    {
                        return Colors.Green;
                    }
                }
            }

            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
