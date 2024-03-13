using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class CurrentOldStackConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                //if (value is APIRES.OBJECTS.vStackPart)
                //{
                //    var item = value as APIRES.OBJECTS.vStackPart;

                //    if (item != null)
                //    {
                //        if (item.Old_Value != item._Org_Value)
                //        {
                //            return Color.Red;
                //        }
                //    }
                //}
            }

            return Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static CurrentOldStackConv _conv;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new CurrentOldStackConv();
            }

            return _conv;
        }
    }
}
