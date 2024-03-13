using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class WOUserTakenConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is List<int>)
            {
                var list = value as List<int>;

                if(API.MainObjects.Instance.CurrentUser != null)
                {
                    if(list.Contains(API.MainObjects.Instance.CurrentUser.PersonID))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
