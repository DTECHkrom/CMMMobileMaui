using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using System.Globalization;

namespace CMMMobileMaui.CONV
{
    public class FileImageConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is byte[])
                {
                    var item = value as byte[];

                    return ImageSource.FromStream(() => new MemoryStream(item));
                }
                else if (value is WOFile)
                {
                    var item = value as WOFile;

                    return ImageSource.FromStream(() => new MemoryStream(item.Data));
                }
                else if (value is GetDeviceImageResposne)
                {
                    var item = value as GetDeviceImageResposne;

                    return ImageSource.FromStream(() => new MemoryStream(item.Image));
                }
                else if (value is GetPartDetailResponse)
                {
                    var item = value as GetPartDetailResponse;

                    if (item.Picture != null)
                    {
                        return ImageSource.FromStream(() => new MemoryStream(item.Picture));
                    }
                }
                //else if (value is vDeviceCMM)
                //{
                //    var item = value as vDeviceCMM;

                //    if (item._Image != null)
                //    {
                //        return ImageSource.FromStream(() => new MemoryStream(item._Image));
                //    }
                //}
                //else if (value is vPart)
                //{
                //    var item = value as vPart;

                //    if (item._Image != null)
                //    {
                //        return ImageSource.FromStream(() => new MemoryStream(item._Image));
                //    }
                //}
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
