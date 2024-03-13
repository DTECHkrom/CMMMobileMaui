using System.Globalization;

namespace CMMMobileMaui.CONV
{
    internal class IsGreatherThenConv : IValueConverter, IMarkupExtension
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (parameter is int paramValue)
                {
                    return (intValue > paramValue);
                }

                return intValue > 0;
            }

            return true;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static IsGreatherThenConv? _instance;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_instance == null)
            {
                _instance = new IsGreatherThenConv();
            }

            return _instance;
        }
    }
}
