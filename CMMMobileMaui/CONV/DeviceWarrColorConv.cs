using System;
using System.Globalization;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class DeviceWarrColorConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var warr = value as GetDeviceWarrantyResponse;
                long toEnd = 0;

                if (warr.WarrantyID == 1)
                {
                    if (warr.Current_Cycles.HasValue)
                    {
                        if (long.TryParse(warr.Param1, out long warrVal))
                        {
                            toEnd = (warrVal - warr.Current_Cycles.Value);                  
                        }
                    }
                }
                else if (warr.WarrantyID == 2)
                {
                    if (warr.Buy_Date.HasValue)
                    {
                        if (int.TryParse(warr.Param1, out int months))
                        {
                            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                            TimeSpan ts2 = new TimeSpan(warr.Buy_Date.Value.Date.AddMonths(months).Ticks);

                            toEnd = (long)ts2.Subtract(ts1).TotalDays;
                        }
                    }
                }
                else if(warr.WarrantyID == 3)
                {
                    if (!string.IsNullOrEmpty(warr.Param2))
                    {
                        if (DateTime.TryParse(warr.Param2, out DateTime modDate)
                            && int.TryParse(warr.Param1, out int months))
                        {
                            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                            TimeSpan ts2 = new TimeSpan(modDate.Date.AddMonths(months).Ticks);

                            toEnd = (long)ts2.Subtract(ts1).TotalDays;
                        }
                    }
                }
                else if(warr.WarrantyID == 4)
                {
                    if (warr.Current_Cycles.HasValue)
                    {
                        //param2 has a war start cycle quantity
                        if (!string.IsNullOrEmpty(warr.Param2))
                        {
                            if (long.TryParse(warr.Param2, out long modCycles)
                               && long.TryParse(warr.Param1, out long val))
                            {
                                toEnd = (val - (warr.Current_Cycles.Value - modCycles));
                            }
                        }
                    }
                }

                if (toEnd >= 0)
                {
                    return Colors.GreenYellow;
                }
                else
                {
                    return Colors.Red;
                }
            }

            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static DeviceWarrColorConv _conv = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new DeviceWarrColorConv();
            }

            return _conv;
        }
    }
}
