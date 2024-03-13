using Telerik.Maui.Controls;
using CMMMobileMaui.COMMON;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Animations;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFrameCheckBox : Grid
    {
        #region FIELDS

        // public COMMON.BaseViewModel parentVM;
        // public IEnumerable<object> orgList;
        // private bool isWindownOpened = false;
        //private string filterText;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomFrameCheckBox), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomFrameCheckBox;
            double fontSize = (double)newValue;
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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomFrameCheckBox), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomFrameCheckBox;
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

        #region BINDABLEPROPERTY IsCheckedStringProperty

        public static readonly BindableProperty IsCheckedStringProperty = BindableProperty.Create(nameof(IsCheckedString), typeof(string), typeof(CustomFrameCheckBox), propertyChanged: OnIsCheckedStringChanged);

        private static void OnIsCheckedStringChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomFrameCheckBox;

            if (newValue == null
                || string.IsNullOrEmpty(newValue.ToString()))
            {
                custom.cb.IsChecked = null;
                return;
            }

            custom.cb.IsChecked = bool.Parse(newValue.ToString());
        }

        public string IsCheckedString
        {
            get
            {
                return (string)GetValue(IsCheckedStringProperty);
            }

            set
            {
                SetValue(IsCheckedStringProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY IsCheckedProperty

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool?), typeof(CustomFrameCheckBox), propertyChanged: OnIsCheckedChanged);

        private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomFrameCheckBox;

            if (newValue == null
                || string.IsNullOrEmpty(newValue.ToString()))
            {
                if (custom.IsThreeState)
                {
                    custom.cb.IsChecked = null;
                }
                else
                {
                    custom.cb.IsChecked = false;
                }

                return;
            }

            custom.cb.IsChecked = bool.Parse(newValue.ToString());
        }

        public bool? IsChecked
        {
            get
            {
                return (bool?)GetValue(IsCheckedProperty);
            }

            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY IsThreeStateProperty

        public static readonly BindableProperty IsThreeStateProperty = BindableProperty.Create(nameof(IsThreeState)
            , typeof(bool), typeof(CustomFrameCheckBox)
            , defaultValue: false
            , propertyChanged: OnIsThreeStateChanged);

        private static void OnIsThreeStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomFrameCheckBox;

            custom.cb.IsThreeState = bool.Parse(newValue.ToString());
        }

        public bool IsThreeState
        {
            get
            {
                return (bool)GetValue(IsThreeStateProperty);
            }

            set
            {
                SetValue(IsThreeStateProperty, value);
            }
        }

        #endregion

        #region PROPERTY IsControlEnabledProperty

        public static readonly BindableProperty IsControlEnabledProperty = BindableProperty.Create(nameof(IsControlEnabled)
            , typeof(bool)
            , typeof(CustomFrameCheckBox)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {
                if (control != null)
                {
                    bool isEnabled = (bool)newValue;
                    CustomFrameCheckBox con = control as CustomFrameCheckBox;
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

        public CustomFrameCheckBox()
        {
            InitializeComponent();
            border.BorderColor = CustomControlsFrameColors.NormalBorderColor;
            cb.IsChecked = null;
            //cb.IsThreeState = true;
            //IsThreeState = true;
            cb.IsCheckedChanged += Cb_IsCheckedChanged;

            gMain.Behaviors.Add(new AnimationBehavior
            {
                Command = new Command(() =>
                {
                    cb.IsChecked = GetNextValue();
                })
                , AnimationType = new FadeAnimation()
            });

            //Maui.TouchEffect.TouchEffect.SetCommand(gMain, new Command(() =>
            //{
            //    //if (IsThreeState)
            //    //    return;

            //}));
        }

        private bool? GetNextValue()
        {
            if (IsThreeState)
            {
                if (!cb.IsChecked.HasValue)
                {
                    return true;
                }

                if (cb.IsChecked == true)
                {
                    return false;
                }

                return null;
            }
            else
            {
                return !cb.IsChecked;
            }
        }


        private void Cb_IsCheckedChanged(object? sender, IsCheckedChangedEventArgs e)
        {
            if (e.NewValue.HasValue)
            {
                IsCheckedString = e.NewValue.Value.ToString();
                IsChecked = e.NewValue.Value;
                return;
            }

            IsCheckedString = string.Empty;
        }

        private void Cb_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            IsCheckedString = e.Value.ToString();
        }

        #endregion

        #region METHOD SetBorderColor

        internal void SetBorderColor()
        {
            cb.IsEnabled = true;

            if (!IsControlEnabled)
            {
                cb.IsEnabled = false;
                border.BorderColor = CustomControlsFrameColors.DisableBorderColor;
            }
            else
            {
                border.BorderColor = CustomControlsFrameColors.NormalBorderColor;
            }
        }

        #endregion
    }
}