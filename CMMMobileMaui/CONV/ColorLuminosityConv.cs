using Microsoft.Maui.Graphics.Converters;
using System.Globalization;

namespace CMMMobileMaui.CONV
{
    public class ColorLuminosityConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string currentColor = string.Empty;
                Color defColor = Colors.Gainsboro;

                if (value is string color)
                {
                    if (!string.IsNullOrEmpty(color))
                    {
                        currentColor = color;
                    }
                }
                else if (value is Color col)
                {
                    defColor = col;
                }

                if (!string.IsNullOrEmpty(currentColor))
                {
                    try
                    {
                        if (currentColor.StartsWith("#"))
                        {
                            defColor = Color.FromArgb(currentColor);
                        }
                        else
                        {
                            ColorTypeConverter c = new ColorTypeConverter();
                            defColor = (Color)c.ConvertFromInvariantString(currentColor);
                        }
                    }
                    catch (Exception)
                    { 
                        defColor = Colors.Gainsboro;
                    }
                }

                float Luminosity = 0;

                if (parameter != null
                    && !string.IsNullOrEmpty(parameter.ToString()))
                {
                    if(float.TryParse(parameter.ToString(), out float result))
                    {                         
                        Luminosity = result;
                    }
                }

                return defColor.AddLuminosity(Luminosity);
            }

            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static ColorLuminosityConv? _conv = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_conv == null)
            {
                _conv = new ColorLuminosityConv();
            }

            return _conv;
        }
    }
}
