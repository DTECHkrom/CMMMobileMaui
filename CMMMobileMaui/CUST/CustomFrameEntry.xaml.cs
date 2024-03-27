using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFrameEntry : Grid
    {
        #region FIELDS

        public COMMON.ViewModelBase? parentVM;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomFrameEntry), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEntry custom)
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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomFrameEntry), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEntry custom)
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

        #region BINDABLEPROPERTY 

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomFrameEntry), propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEntry custom)
            {
                string text = string.Empty;

                if (newValue != null)
                {
                    text = newValue.ToString() ?? string.Empty;
                }

                if (!string.IsNullOrWhiteSpace(text))
                {
                    custom.btnClear.IsVisible = true;

                    custom.border.BorderColor = Colors.Black;
                }
                else
                {
                    if (custom.IsRequired)
                    {
                        custom.border.BorderColor = Colors.Red;
                    }

                    custom.btnClear.IsVisible = false;
                }

                custom.tboxText.Text = text;
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

        #region BINDABLEPROPERTY IsRequiredPorperty

        public static readonly BindableProperty IsRequiredPorperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomFrameEntry), propertyChanged: IsRequiredChanged);

        private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEntry custom)
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

        #region BINDABLEPROPERTY IsPasswordPorperty

        public static readonly BindableProperty IsPasswordPorperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomFrameEntry), propertyChanged: IsPasswordChanged);

        private static void IsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEntry custom)
            {
                custom.tboxText.IsPassword = (bool)newValue;
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordPorperty);
            }

            set
            {
                SetValue(IsPasswordPorperty, value);
            }
        }

        #endregion

        #region PROPERTY IsControlEnabledProperty

        public static readonly BindableProperty IsControlEnabledProperty = BindableProperty.Create(nameof(IsControlEnabled)
            , typeof(bool)
            , typeof(CustomFrameEntry)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {
                if (control is CustomFrameEntry con)
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

        #region PROPERTY ReturnCommandProperty

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(CustomFrameEntry), propertyChanged: OnReturnCommandChanged);

        private static void OnReturnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameEntry custom)
            {
                if(newValue != null)
                {
                    custom.tboxText.ReturnCommand = (ICommand)newValue;
                }
            }
        }

        public ICommand ReturnCommand
        {
            get
            {
                return (ICommand)GetValue(ReturnCommandProperty);
            }
            set
            {
                SetValue(ReturnCommandProperty, value);
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

        public CustomFrameEntry()
        {
            InitializeComponent();
            btnClear.IsVisible = false;
            // btnImage.Source = "edit.png";
            tboxText.FontSize = 14;
            tboxText.TextChanged += TboxText_TextChanged;

            ClearSelectedItemCommand = new Command((obj) =>
            {
                Text = string.Empty;

                if (ReturnCommand != null)
                {
                    ReturnCommand.Execute(obj);
                }

                tboxText.Focus();
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

        private void TboxText_TextChanged(object? sender, TextChangedEventArgs e)
        {
            Text = tboxText.Text;
            Console.WriteLine(Text);
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