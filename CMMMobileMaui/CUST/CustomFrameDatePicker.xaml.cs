using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFrameDatePicker : Grid
    {
        #region FIELDS

        public COMMON.ViewModelBase? parentVM;

        #endregion

        #region BINDABLEPROPERTY Title

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomFrameDatePicker), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameDatePicker custom)
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

        public static readonly BindableProperty StringValueProperty = BindableProperty.Create(nameof(StringValue), typeof(string), typeof(CustomFrameDatePicker), propertyChanged: OnStringValueChanged);

        private static void OnStringValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameDatePicker custom)
            {
                custom.tboxText.Date = string.IsNullOrEmpty(newValue?.ToString()) ? null : DateTime.Parse(newValue.ToString());
                custom.SetBorderColor();
            }
        }

        public string StringValue
        {
            get => (string)GetValue(StringValueProperty);
            set => SetValue(StringValueProperty, value);
        }

        #endregion

        #region BINDABLEPROPERTY 

        public static readonly BindableProperty DateValueProperty = BindableProperty.Create(nameof(DateValue), typeof(DateTime?), typeof(CustomFrameDatePicker), propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomFrameDatePicker custom)
            {
                custom.tboxText.Date = newValue as DateTime?;
                custom.SetBorderColor();
            }
        }

        public DateTime? DateValue
        {
            get
            {
                return (DateTime?)GetValue(DateValueProperty);
            }

            set
            {
                SetValue(DateValueProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY IsRequiredPorperty

        public static readonly BindableProperty IsRequiredPorperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomFrameDatePicker), propertyChanged: IsRequiredChanged);

        private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomFrameDatePicker custom)
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

        #region PROPERTY IsControlEnabledProperty

        public static readonly BindableProperty IsControlEnabledProperty = BindableProperty.Create(nameof(IsControlEnabled)
            , typeof(bool)
            , typeof(CustomFrameDatePicker)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {
                if(control is CustomFrameDatePicker con)
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

        public CustomFrameDatePicker()
        {
            InitializeComponent();
            btnClear.IsVisible = false;
            tboxText.Placeholder = string.Empty;
            tboxText.DefaultHighlightedDate = DateTime.Now.Date;

            tboxText.SelectionChanged += TboxText_SelectionChanged;

            ClearSelectedItemCommand = new Command((obj) =>
            {
                tboxText.ClearSelection();
                StringValue = string.Empty;
                tboxText.Focus();
            });

            FocusEditorCommand = new Command((obj) =>
            {
                tboxText.Focus();
            });


            AnimationBehavior clearAnimation = new();
            clearAnimation.Command = ClearSelectedItemCommand;
            clearAnimation.AnimationType = new FadeAnimation();

            btnClear.Behaviors.Add(clearAnimation);

            //  TouchEffect.SetCommand(btnClear, ClearSelectedItemCommand);
            // TouchEffect.SetCommand(lblTitle, FocusEditorCommand);
            lblTitle.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = FocusEditorCommand
            });
        }

        #endregion

        #region METHOD SetBorderColor

        internal void SetBorderColor()
        {
            if (!IsControlEnabled)
            {
                border.BorderColor = COMMON.CustomControlColors.DisableBorderColor;
                border.BackgroundColor = COMMON.CustomControlColors.DisableBackgroundColor;
            }
            else
            {
                border.BorderColor = COMMON.CustomControlColors.NormalBorderColor;
                border.BackgroundColor = COMMON.CustomControlColors.NormalBackgroundColor;

                if (tboxText.Date.HasValue)
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

        #region EVENT TboxText_SelectionChanged

        private void TboxText_SelectionChanged(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                DateValue = tboxText.Date;
                StringValue = tboxText.Date.HasValue ? tboxText.Date.Value.ToString() : string.Empty;
            }
        }

        #endregion

        #region EVENT OnBindingContextChanged

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