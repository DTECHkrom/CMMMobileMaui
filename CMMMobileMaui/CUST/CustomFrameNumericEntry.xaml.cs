using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFrameNumericEntry : Grid
    {
        #region FIELDS

        public COMMON.ViewModelBase? parentVM;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomFrameNumericEntry), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameNumericEntry custom)
            {
                if (newValue is double fontSize)
                {
                    custom.tboxText.FontSize = fontSize;
                }
                else
                {
                    throw new ArgumentException("FontSize must be a double");
                }
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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomFrameNumericEntry), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameNumericEntry custom)
            {
                custom.lblTitle.Text = newValue.ToString()?.ToUpperInvariant() ?? string.Empty;
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

        #region BINDABLEPROPERTY IsRequiredPorperty

        public static readonly BindableProperty IsRequiredPorperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomFrameNumericEntry), propertyChanged: IsRequiredChanged);

        private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameNumericEntry custom)
            {
                custom.SetBorderColor();
            }
        }

        public bool IsRequired
        {
            get
            {
                return (bool)GetValue(IsRequiredPorperty);
            }

            set
            {
                SetValue(IsRequiredPorperty, value);
            }
        }

        #endregion

        #region PROPERTY NumericValueProperty

        public static readonly BindableProperty NumericValueProperty = BindableProperty.Create(
            nameof(NumericValue),
            typeof(decimal?),
            typeof(CustomFrameNumericEntry),
            null,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if(bindable is CustomFrameNumericEntry custom)
                {
                    if (newValue is decimal aa)
                    {
                        custom.tboxText.NumericValue = aa;
                    }
                    else
                    {
                        custom.tboxText.NumericValue = null;
                    }

                    custom.SetBorderColor();
                }
            }
        );
        public decimal? NumericValue
        {
            get { return (decimal?)GetValue(NumericValueProperty); }
            set { SetValue(NumericValueProperty, value); }
        }

        #endregion

        #region PROPERTY ClearValueProperty

        public static readonly BindableProperty NumericClearValueProperty = BindableProperty.Create(
            nameof(NumericClearValue),
            typeof(decimal?),
            typeof(CustomFrameNumericEntry),
            null,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var custom = bindable as CustomFrameNumericEntry;
            }
        );
        public decimal? NumericClearValue
        {
            get { return (decimal?)GetValue(NumericClearValueProperty); }
            set { SetValue(NumericClearValueProperty, value); }
        }

        #endregion

        #region PROPERTY NumericValueFormat
        public string NumericValueFormat
        {
            get { return (string)GetValue(NumericValueFormatProperty) ?? "N0"; }
            set
            {
                var _value = string.IsNullOrWhiteSpace(value) ? "N0" : value;
                SetValue(NumericValueFormatProperty, value);
            }
        }

        public static readonly BindableProperty NumericValueFormatProperty = BindableProperty.Create(
            nameof(NumericValueFormat),
            typeof(string),
            typeof(CustomFrameNumericEntry),
            "N0",
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if(bindable is CustomFrameNumericEntry custom)
                {
                    custom.tboxText.NumericValueFormat = newValue.ToString() ?? "N0";
                }
            }
        );

        #endregion

        #region PROPERTY IsControlEnabledProperty

        public static readonly BindableProperty IsControlEnabledProperty = BindableProperty.Create(nameof(IsControlEnabled)
            , typeof(bool)
            , typeof(CustomFrameNumericEntry)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {
                if (control is CustomFrameNumericEntry con)
                {
                    con.SetBorderColor();
                }
            });

        public bool IsControlEnabled
        {
            get
            {
                return (bool)GetValue(IsControlEnabledProperty);
            }

            set
            {
                SetValue(IsControlEnabledProperty, value);
            }
        }

        #endregion

        #region COMMAND ClearSelectedItemCommand

        public ICommand ClearSelectedItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND FocusEditorCommand

        public ICommand FocusEditorCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public CustomFrameNumericEntry()
        {
            InitializeComponent();
            btnClear.IsVisible = false;
            tboxText.TextChanged += TboxText_TextChanged;
            tboxText.Unfocused += OnUnfocused;

            ClearSelectedItemCommand = new Command((obj) =>
            {
                if (NumericClearValue.HasValue)
                {
                    NumericValue = NumericClearValue;
                }
                else
                {
                    tboxText.Text = string.Empty;
                    NumericValue = null;
                    tboxText.Focus();
                }
            });

            FocusEditorCommand = new Command((obj) =>
            {
                tboxText.Focus();
            });


            btnClear.Command = ClearSelectedItemCommand;

            lblTitle.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = FocusEditorCommand
            });
        }

        private void OnUnfocused(object? sender, FocusEventArgs e)
        {
            tboxText.SetDisplayFormat();
           // NumericValue = tboxText.NumericValue;
        }

        private void TboxText_TextChanged(object? sender, TextChangedEventArgs e)
        {
            SetBorderColor();

            tboxText.SetDisplayFormat();
           // NumericValue = tboxText.NumericValue;
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

        #region METHOD SetBorderColor

        internal void SetBorderColor()
        {
            tboxText.IsEnabled = true;

            if (!IsControlEnabled)
            {
                tboxText.IsEnabled = false;
                btnClear.IsVisible = false;
                border.BorderColor = COMMON.CustomControlColors.DisableBorderColor;
                border.BackgroundColor = COMMON.CustomControlColors.DisableBackgroundColor;
            }
            else
            {
                border.BorderColor = COMMON.CustomControlColors.NormalBorderColor;
                border.BackgroundColor = COMMON.CustomControlColors.NormalBackgroundColor;

                if (!string.IsNullOrEmpty(tboxText.Text))
                {
                    btnClear.IsVisible = true;
                }
                else
                {
                    if (IsRequired)
                    {
                        border.BorderColor = COMMON.CustomControlColors.RequiredBorderColor;
                    }

                    btnClear.IsVisible = false;
                }
            }
        }

        #endregion

    }
}