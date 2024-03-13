namespace CMMMobileMaui.CUST
{
    //public class TintedCachedImage : CachedImage
    //{
    //    public static BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(TintedCachedImage), Colors.Transparent, propertyChanged: UpdateColor);

    //    public Color TintColor
    //    {
    //        get { return (Color)GetValue(TintColorProperty); }
    //        set { SetValue(TintColorProperty, value); }
    //    }

    //    private static void UpdateColor(BindableObject bindable, object oldColor, object newColor)
    //    {
    //        var oldcolor = (Color)oldColor;
    //        var newcolor = (Color)newColor;

    //        if (!oldcolor.Equals(newcolor))
    //        {
    //            var view = (TintedCachedImage)bindable;
    //            TintTransformation tint = new TintTransformation(newcolor.ToHex());
    //            tint.EnableSolidColor = true;
    //            var transformations = new List<ITransformation>() {
    //                tint
    //            };
    //            view.Transformations = transformations;
    //        }
    //    }
    //}
}
