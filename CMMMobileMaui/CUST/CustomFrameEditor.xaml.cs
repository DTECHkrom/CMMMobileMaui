using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFrameEditor : Grid
    {
        #region FIELDS

        public COMMON.ViewModelBase? parentVM;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomFrameEditor), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEditor custom)
            {
                if(newValue is double fontSize)
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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomFrameEditor), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEditor custom)
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

        #region BINDABLEPROPERTY TextProperty

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomFrameEditor), propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEditor custom)
            {
                string text = string.Empty;

                if (newValue != null)
                {
                    text = newValue.ToString() ?? string.Empty;
                }

                custom.tboxText.Text = text;

                custom.SetBorderColor();
            }

            //string text = string.Empty;

            //if (newValue != null)
            //{
            //    text = newValue.ToString();
            //}

            //if (!string.IsNullOrWhiteSpace(text))
            //{
            //    custom.btnClear.IsVisible = true;

            //    custom.border.BorderColor = Color.Black;
            //}
            //else
            //{
            //    if (custom.IsRequired)
            //    {
            //        custom.border.BorderColor = Color.Red;
            //    }

            //    custom.btnClear.IsVisible = false;
            //}

            //custom.tboxText.Text = text;
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

        #region BINDABLEPROPERTY IsRequiredPorperty

        public static readonly BindableProperty IsRequiredPorperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomFrameEditor), propertyChanged: IsRequiredChanged);

        private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEditor custom)
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

        #region COMMAND ClearSelectedItemCommand

        public ICommand ClearSelectedItemCommand
        {
            get;
        }

        #endregion

        #region PROPERTY IsControlEnabledProperty

        public static readonly BindableProperty IsControlEnabledProperty = BindableProperty.Create(nameof(IsControlEnabled)
            , typeof(bool)
            , typeof(CustomFrameEditor)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {
                if (control is CustomFrameEditor con)
                {
                    bool isEnabled = (bool)newValue;
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

        #region Cstr

        public CustomFrameEditor()
        {
            InitializeComponent();
            btnClear.IsVisible = false;
            // btnImage.Source = "edit.png";
            tboxText.FontSize = 14;
            tboxText.TextChanged += TboxText_TextChanged;

            ClearSelectedItemCommand = new Command((obj) =>
            {
                Text = string.Empty;
            });

            btnClear.Behaviors.Add(new AnimationBehavior
            {
                Command = ClearSelectedItemCommand
                , AnimationType = new FadeAnimation()
            });
        }

        private void TboxText_TextChanged(object? sender, TextChangedEventArgs e)
        {
            Text = tboxText.Text;
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