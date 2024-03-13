using System;
using System.Globalization;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class PartStockColorConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                var part = value as GetPartDetailResponse;

                if(part.StockLevelTarget.HasValue && part.StockLevel.HasValue)
                {
                    if(part.StockLevel > part.StockLevelTarget)
                    {
                        return COMMON.SConsts.COLOR_GREEN;
                    }
                    else if(part.StockLevelTarget == part.StockLevel)
                    {
                        return COMMON.SConsts.COLOR_YELLOW;
                    }
                    else if(part.StockLevel < part.StockLevelTarget)
                    {
                        return COMMON.SConsts.COLOR_RED;
                    }
                }
            }

            return Colors.Gainsboro;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public static PartStockColorConv _con;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_con == null)
            {
                _con = new PartStockColorConv();
            }

            return _con;
        }
    }
}
