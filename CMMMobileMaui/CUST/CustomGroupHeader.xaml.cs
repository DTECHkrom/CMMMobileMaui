
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomGroupHeader : Grid
    {

        #region BINDABLEPROPERTY Title

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomGroupHeader), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGroupHeader;
            custom.lblTitle.Text = newValue.ToString();
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }

            set
            {
                SetValue(TitleProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomGroupHeader), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGroupHeader;
            double fontSize = (double)newValue;
            //  custom.lblTitle.FontSize = fontSize;
            custom.lblTitle.FontSize = fontSize;
        }

        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }

            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY BorderColorProperty

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomGroupHeader), defaultValue: Colors.Black, propertyChanged: OnBorderColorChangd);

        private static void OnBorderColorChangd(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGroupHeader;
            custom.bv1.BackgroundColor = (Color)newValue;
            custom.bv2.BackgroundColor = (Color)newValue;
        }

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }

            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY BorderColorProperty

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomGroupHeader), defaultValue: Colors.Black, propertyChanged: OnTextColorChangd);

        private static void OnTextColorChangd(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGroupHeader;
            custom.lblTitle.TextColor = (Color)newValue;
        }

        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        #endregion

        public CustomGroupHeader()
        {
            InitializeComponent();
        }
    }
}