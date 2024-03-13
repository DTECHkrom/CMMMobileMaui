using System.Collections.ObjectModel;
using System.Globalization;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;

namespace CMMMobileMaui.CONV
{
    public class DeviceStatePerConv : IValueConverter, IMarkupExtension
    {
        public static decimal TotalCount = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<GetDeviceStateResponse> list && parameter != null)
            {
                if (TotalCount == 0)
                {
                    TotalCount = list.Where(tt => tt.State_Time > 0).Sum(tt => tt.State_Time);
                }

                if (TotalCount > 0)
                {
                    decimal stateTotal = list.Where(tt => tt.State_Time > 0
                    && tt.StateID == int.Parse(parameter.ToString())).Sum(tt => tt.State_Time);
                    return (stateTotal / TotalCount);
                }              
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static DeviceStatePerConv _conv = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_conv == null)
            {
                _conv = new DeviceStatePerConv();
            }

            return _conv;
        }
    }
}
