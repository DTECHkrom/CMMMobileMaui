namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomGridLabel : Grid
    {
        #region FIELDS

      //  public COMMON.ViewModelBase parentVM;
      //  public IEnumerable<object> orgList;
       // private bool isWindownOpened = false;
//private string filterText;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomGridLabel), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomGridLabel custom)
            {
                double fontSize = (double)newValue;

                if(fontSize > 0)
                    custom.lblValue.FontSize = fontSize;
            }
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
            if(bindable is CustomGridLabel custom)
            {
                custom.lblTitle.Text = newValue.ToString()?.ToUpperInvariant();
            }
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
            if (bindable is CustomGridLabel custom)
            {
                string text = string.Empty;

                if (newValue is string str)
                {
                    text = str;
                }

                custom.lblValue.Text = text;
            }
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
            if (bindable is CustomGridLabel control)
            {
                if (int.TryParse(newValue.ToString(), out int value))
                {
                    if (value <= 0)
                    {
                        value = 10;
                    }

                    control.lblValue.MaxLines = value;
                }
            }       
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
            if(bindable is CustomGridLabel custom)
            {
                double height = (double)newValue;

                if(height > 0)
                {
                    custom.mainScroll.HeightRequest = height;
                }
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
            //if (this.BindingContext != null && this.BindingContext is COMMON.ViewModelBase vm)
            //{
            //    this.parentVM = vm;
            //}

            base.OnBindingContextChanged();
        }

        #endregion

    }
}