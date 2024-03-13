using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class ActUserTakenConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is List<short>)
            {
                var list = value as List<short>;

                if (API.MainObjects.Instance.CurrentUser != null)
                {
                    if (list.Contains(API.MainObjects.Instance.CurrentUser.PersonID))
                    {
                        return Colors.Green;
                    }
                }
            }

            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
