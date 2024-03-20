using System.Globalization;

namespace CMMMobileMaui.CONV
{
    public class ActUserTakenConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<short> list)
            {
                if (list.Contains(API.MainObjects.Instance.CurrentUser!.PersonID))
                {
                    return Colors.Green;
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
