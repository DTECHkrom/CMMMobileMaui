using System;
using System.Globalization;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CONV
{
    public class FileNameConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                string name = value.ToString();

                return name.Substring(name.LastIndexOf(@"\") + 1, name.Length - name.LastIndexOf(@"\") - 1);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static FileNameConv _conv = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_conv == null)
            {
                _conv = new FileNameConv();
            }

            return _conv;
        }
    }
}
