namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomGridLabel : Grid
    {
        #region FIELDS

        public COMMON.ViewModelBase parentVM;
        public IEnumerable<object> orgList;
        private bool isWindownOpened = false;
        private string filterText;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomGridLabel), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGridLabel;
            double fontSize = (double)newValue;
            custom.lblValue.FontSize = fontSize;
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

        #region BINDABLEPROPERTY Title

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomGridLabel), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGridLabel;
            custom.lblTitle.Text = newValue.ToString().ToUpperInvariant();
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

        #region BINDABLEPROPERTY TextProperty

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomGridLabel), propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomGridLabel;

            string text = string.Empty;

            if (newValue != null)
            {
                text = newValue.ToString();
            }

            custom.lblValue.Text = text;
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        #endregion

        #region PROEPRTY TextMaxLinesProperty

        public static readonly BindableProperty TextMaxLinesProperty = BindableProperty.Create(nameof(TextMaxLines), typeof(int), typeof(CustomGridLabel), defaultValue: 2, propertyChanged: OnTextMaxLinesChanged);
        private static void OnTextMaxLinesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomGridLabel;
            int value = (int)newValue;

            if (value <= 0)
            {
                value = 1000;
                //  control.lblValue2.IsVisible = false;
                // control.mainScroll.IsVisible = true;
            }

            control.lblValue.MaxLines = value;
        }

        public int TextMaxLines
        {
            get
            {
                return (int)GetValue(TextMaxLinesProperty);
            }
            set
            {
                SetValue(TextMaxLinesProperty, value);
            }
        }

        #endregion

        #region PROPERTY ScrollMaxHeightProperty

        public static readonly BindableProperty ScrollMaxHeightProperty = BindableProperty.Create(nameof(ScrollMaxHeight), typeof(double), typeof(CustomGridLabel), propertyChanged: OnScrollHeightChanged);
        private static void OnScrollHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomGridLabel;

            if (newValue != null)
            {
                control.mainScroll.HeightRequest = (double)newValue;
            }
        }

        public double ScrollMaxHeight
        {
            get
            {
                return (double)GetValue(ScrollMaxHeightProperty);
            }
            set
            {
                SetValue(ScrollMaxHeightProperty, value);
            }
        }

        #endregion

        #region Cstr

        public CustomGridLabel()
        {
            InitializeComponent();
        }

        #endregion

        #region EVENT 

        protected override void OnBindingContextChanged()
        {
            if (this.BindingContext != null && this.BindingContext is COMMON.ViewModelBase vm)
            {
                this.parentVM = vm;
            }

            base.OnBindingContextChanged();
        }

        #endregion

    }
}