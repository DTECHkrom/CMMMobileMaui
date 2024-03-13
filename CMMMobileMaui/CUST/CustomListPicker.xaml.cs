using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;
using Mopups.Services;
using System.Windows.Input;


namespace CMMMobileMaui.CUST
{
 //   [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomListPicker : Grid
    {
        #region FIELDS

        public COMMON.ViewModelBase? parentVM;
        public IEnumerable<object>? orgList;
        private bool isWindownOpened = false;
        private string filterText;
        private IEnumerable<object>? listItemsSource;

        #endregion

        #region BINDABLEPROPERTY FontSizeProperty

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomListPicker), defaultValue: 14d, propertyChanged: OnFontSizeChangd);

        private static void OnFontSizeChangd(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomListPicker custom)
            {
                if(double.TryParse(newValue.ToString(), out double fontSize))
                {
                    custom.lblValue.FontSize = fontSize;
                }
                else
                {
                    throw new ArgumentException("FontSize must be a number");
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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomListPicker), propertyChanged: OnTitleChanged);

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomListPicker custom)
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

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomListPicker), propertyChanged: OnTextChanged);

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomListPicker custom)
            {
                string text = newValue.ToString() ?? string.Empty;

                custom.lblValue.Text = text;
                custom.SetBorderColor();
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

        #region BINDABLEPROPERTY ItemsSourceProperty

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource)
            , typeof(IEnumerable<object>)
            , typeof(CustomListPicker)
            , defaultBindingMode : BindingMode.OneWay
            , propertyChanged: OnItemsSourceChanged);

        private static bool wasInit = false;
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomListPicker;

            if (custom == null)
                return;

            var items = newValue as IEnumerable<object>;

            if (items != null)
            {
                if (!wasInit)
                {
                    custom.orgList = items;
                    custom.ListItemsSource = items;
                    wasInit = true;
                }
            }
            else
            {
                custom.orgList = Enumerable.Empty<object>();
            }
        }

        public IEnumerable<object>? ItemsSource
        {
            get
            {
                return (IEnumerable<object>?)GetValue(ItemsSourceProperty);
            }

            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY IsRequiredPorperty

        public static readonly BindableProperty IsRequiredPorperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomListPicker), propertyChanged: IsRequiredChanged);

        private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomListPicker custom)
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
            , typeof(CustomListPicker)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {
                if (control is CustomListPicker con)
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

        #region BINDABLEPROPERTY SelectedItemProperty

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem)
            , typeof(object)
            , typeof(CustomListPicker)
            , defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: OnSelectedItemChanged);

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var custom = bindable as CustomListPicker;

            if (custom is null)
                return;

            if (newValue != null)
            {
                if (!string.IsNullOrWhiteSpace(custom.DisplayPath))
                {
                    var property = newValue.GetType().GetProperty(custom.DisplayPath);

                    if (property != null)
                    {
                        custom.Text = property.GetValue(newValue)?.ToString() ?? string.Empty;
                    }
                    else
                    {
                        throw new ArgumentException($"Property {custom.DisplayPath} not found in {newValue.GetType().Name}");
                    }
                }
                else
                {
                    custom.Text = newValue.ToString() ?? string.Empty;
                }
            }
            else
            {
                custom.Text = string.Empty;
            }
        }

        public object? SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }

            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY DisplayPathProperty

        public static readonly BindableProperty DisplayPathProperty = BindableProperty.Create(nameof(DisplayPath), typeof(string), typeof(CustomListPicker), propertyChanged: OnDisplayPathChanged);

        private static void OnDisplayPathChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public string DisplayPath
        {
            get
            {
                return (string)GetValue(DisplayPathProperty);
            }

            set
            {
                SetValue(DisplayPathProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY DisplayPathProperty

        public static readonly BindableProperty EmptyPropertyNameProperty = BindableProperty.Create(nameof(EmptyPropertyName)
            , typeof(string)
            , typeof(CustomListPicker)
            , propertyChanged: OnEmptyPropertyNameChanged);

        private static void OnEmptyPropertyNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public string EmptyPropertyName
        {
            get
            {
                return (string)GetValue(EmptyPropertyNameProperty);
            }

            set
            {
                SetValue(EmptyPropertyNameProperty, value);
            }
        }

        #endregion

        #region PROPERTY ButtonImageSourceProperty

        public static readonly BindableProperty ButtonImageSourceProperty = BindableProperty.Create(nameof(ButtonImageSource), typeof(ImageSource), typeof(CustomListPicker)
            , propertyChanged: OnButtonImageSourceChanged);

        private static void OnButtonImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomListPicker custom)
            {
                if (newValue is ImageSource image)
                {
                    custom.btnImage.Source = image;
                    custom.btnImage.IsVisible = true;
                }
                else
                {
                    custom.btnImage.IsVisible = false;
                }
            }
        }

        public ImageSource ButtonImageSource
        {
            get
            {
                return (ImageSource)GetValue(ButtonImageSourceProperty);
            }

            set
            {
                SetValue(ButtonImageSourceProperty, value);
            }
        }

        #endregion

        #region BINDABLEPROPERTY CustomActionProperty

        public static readonly BindableProperty CustomActionProperty = BindableProperty.Create(nameof(CustomAction), typeof(Action<object>), typeof(CustomListPicker), propertyChanged: OnCustomAction);

        private static void OnCustomAction(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomListPicker custom)
            {
                if(newValue is Action<object> customAction)
                {
                    custom.CustomAction = customAction;
                }
            }
        }

        public Action<object> CustomAction
        {
            get
            {
                return (Action<object>)GetValue(CustomActionProperty);
            }

            set
            {
                SetValue(CustomActionProperty, value);
            }
        }

        #endregion

        #region PROPERTY IsControlEnabledProperty

        public static readonly BindableProperty IsClearAllowedProperty = BindableProperty.Create(nameof(IsClearAllowed)
            , typeof(bool)
            , typeof(CustomListPicker)
            , defaultValue: true
            , BindingMode.TwoWay
            , propertyChanged:
            (control, oldValue, newValue) =>
            {

            });

        public bool IsClearAllowed
        {
            get
            {
                return (bool)GetValue(IsClearAllowedProperty);
            }

            set
            {
                SetValue(IsClearAllowedProperty, value);
            }
        }

        #endregion

        #region PROPERTY FilterText

        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                FilterMethod();
            }
        }

        #endregion

        #region PROPERTY

        public IEnumerable<object>? ListItemsSource
        {
            get => listItemsSource;
            set
            {
                listItemsSource = value;
                OnPropertyChanged(nameof(ListItemsSource));
            }
        }

        #endregion

        #region COMMAND OpenWindowCommand

        private ICommand OpenWindowCommand
        {
            get;
        }

        #endregion

        #region COMMAND CloseListCommand

        public ICommand CloseListCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectItemCommand

        public ICommand SelectItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND ClearFilterCommand

        public ICommand ClearFilterCommand
        {
            get;
        }

        #endregion

        #region COMMAND ClearSelectedItemCommand

        public ICommand ClearSelectedItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenCustomActionCommand

        public ICommand OpenCustomActionCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public CustomListPicker()
        {
            InitializeComponent();
            btnClear.IsVisible = false;
            btnImage.Source = "expand.png";
            ListItemsSource = Enumerable.Empty<object>();
            ClearFilterCommand = new Command((obj) =>
            {
                FilterText = string.Empty;
            });

            ClearSelectedItemCommand = new Command((obj) =>
            {
                SelectedItem = null;
                OnPropertyChanged(nameof(SelectedItem));
            });

            CloseListCommand = new Command(async (obj) =>
            {
                isWindownOpened = false;
                await MopupService.Instance.PopAsync();
            });

            OpenWindowCommand = new Command(async (obj) =>
            {
                if (!IsControlEnabled)
                    return;

                if (!isWindownOpened)
                {
                    isWindownOpened = true;

                    ListCustomListPicker pc = new ListCustomListPicker();
                    pc.BindingContext = this;

                    if (this.parentVM != null)
                    {
                        pc.Disappearing += (s, e) =>
                        {
                            isWindownOpened = false;
                        };
                    }

                    await MopupService.Instance.PushAsync(pc);
                }
            });

            SelectItemCommand = new Command((obj) =>
            {
                SelectedItem = obj;
                CloseListCommand.Execute(obj);
            });

            OpenCustomActionCommand = new Command(() =>
            {
                if (SelectedItem != null)
                {
                    CustomAction?.Invoke(SelectedItem);
                }
            });

           // Maui.TouchEffect.TouchEffect.SetCommand(gMain, OpenWindowCommand);
          //  Maui.TouchEffect.TouchEffect.SetNativeAnimation(gMain, true);
           // Maui.TouchEffect.TouchEffect.SetLongPressCommand(gMain, OpenCustomActionCommand);
          //  Maui.TouchEffect.TouchEffect.SetCommand(btnClear, ClearSelectedItemCommand);

            btnClear.Behaviors.Add(new AnimationBehavior
            {
                Command = ClearSelectedItemCommand
                , AnimationType = new FadeAnimation()
            });

            gMain.Behaviors.Add(new AnimationBehavior
            {
                Command = OpenWindowCommand
                ,
                AnimationType = new FadeAnimation()
            });

            
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
            btnClear.IsVisible = false;
            btnImage.IsVisible = false;

            if (!IsControlEnabled)
            {
                //TouchEffect.SetNativeAnimation(gMain, false);
                border.BorderColor = COMMON.CustomControlColors.DisableBorderColor;
                border.BackgroundColor = COMMON.CustomControlColors.DisableBackgroundColor;
            }
            else
            {
                //  TouchEffect.SetNativeAnimation(gMain, true);
                btnImage.IsVisible = true && IsClearAllowed;
                border.BorderColor = COMMON.CustomControlColors.NormalBorderColor;
                border.BackgroundColor = COMMON.CustomControlColors.NormalBackgroundColor;

                if (!string.IsNullOrEmpty(lblValue.Text))
                {
                    btnClear.IsVisible = true && IsClearAllowed;
                }
                else
                {
                    if (IsRequired)
                    {
                        border.BorderColor = COMMON.CustomControlColors.RequiredBorderColor;
                    }
                }
            }
        }

        #endregion

        #region METHOD FilterMethod

        private void FilterMethod()
        {
            if (orgList != null)
            {
                if (string.IsNullOrWhiteSpace(FilterText))
                {
                    ListItemsSource = orgList;
                    //ItemsSource = orgList;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(DisplayPath))
                    {
                        var stringItems = orgList.Cast<string>().Where(tt => tt.Contains(FilterText.ToLower()));
                        ItemsSource = stringItems;
                        return;
                    }

                    var items = orgList.Where(tt => tt.GetType()
                    .GetProperty(DisplayPath)
                    .GetValue(tt)
                    .ToString()
                    .ToLower()
                    .Contains(FilterText.ToLower())).ToList();

                    ListItemsSource = items;
                }

                OnPropertyChanged(nameof(ListItemsSource));
            }
        }

        #endregion
    }
}