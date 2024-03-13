using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using System.Globalization;

namespace CMMMobileMaui.CONV
{
    public class IsReturnButtonVisibleConv : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null
                && value is GetPartChangeResponse partChange)
            {
                if (API.MainObjects.Instance.CurrentUser == null)
                    return false;

                return partChange.AmountStateChange > 0
                    && (partChange.StockChangeTypeID == 1 || partChange.StockChangeTypeID == 5)
                    && (API.MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Add
                    || API.MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Edit_State
                    || API.MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Give);
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static IsReturnButtonVisibleConv? _conv;
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_conv == null)
            {
                _conv = new IsReturnButtonVisibleConv();
            }

            return _conv;
        }
    }
}
