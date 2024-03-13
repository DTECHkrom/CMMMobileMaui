namespace CMMMobileMaui.CONV
{
    //public class SVGImageSourceToDataConv : IValueConverter, IMarkupExtension
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if(parameter != null)
    //        {
    //            double width = 32;
    //            double height = 32;

    //            if (value != null)
    //            {
    //                if(value is ImageButton)
    //                {
    //                    width = ((ImageButton)value).WidthRequest != -1? ((ImageButton)value).WidthRequest :width;
    //                    height = ((ImageButton)value).HeightRequest != -1? ((ImageButton)value).HeightRequest : height;
    //                }
    //                else if(value is Image)
    //                {
    //                    width = ((Image)value).WidthRequest != -1 ? ((Image)value).WidthRequest : width;
    //                    height = ((Image)value).HeightRequest != -1 ? ((Image)value).HeightRequest : height;
    //                }
    //            }

    //            var source = SvgImageSource.FromFile(parameter.ToString(), (int)width, (int)height);

    //            return source;
    //        }

    //        return string.Empty;
    //    }

    //    public bool IsColorChange
    //    {
    //        get;
    //        set;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return null;
    //    }

    //    private static SVGImageSourceToDataConv _conv;

    //    public object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        if(_conv == null)
    //        {
    //            _conv = new SVGImageSourceToDataConv();
    //        }

    //        return _conv;
    //    }
    //}
}
