using System;
using System.Globalization;
using CMMMobileMaui.API.Contracts.v1.Responses.Plan;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class WOPlanActConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null
                && value is GetWOPlanActsResponse item)
            {
                if(!item.WorkOrderActivityID.HasValue)
                {
                    return true;
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
