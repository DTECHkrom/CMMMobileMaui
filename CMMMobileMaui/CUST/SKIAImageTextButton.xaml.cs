using CMMMobileMaui.COMMON;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Graphics.Converters;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.ComponentModel;
using System.Windows.Input;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SKIAImageTextButton : Grid, INotifyPropertyChanged
    {
        #region FIELDS

        protected Color orgColor = Colors.Black;
        private bool isBackwardAnimRunning;
        private bool isForwardAnimRunning;

        #endregion

        #region PROPERTY TextProperty

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SKIAImageTextButton), propertyChanged: TextPropertyChanged);

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is SKIAImageTextButton btn)
            {
                btn.Text = newValue.ToString() ?? string.Empty;
                btn.skiaText.InvalidateSurface();
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

        #region PROPERTY ImageHeightWidthProperty

        public static readonly BindableProperty ImageHeightWidthProperty = BindableProperty.Create(nameof(ImageHeightWidth), typeof(double), typeof(SKIAImageTextButton), propertyChanged: OnImageHeightWidthChanged);

        private static void OnImageHeightWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SKIAImageTextButton btn)
            {
                if (double.TryParse(newValue.ToString(), out double result) && result > 0)
                {
                    btn.img.WidthRequest = btn.img.HeightRequest = result;
                }
                else
                {
                    throw new ArgumentException("Invalid value for ImageHeightWidth");
                }
            }
        }

        public double ImageHeightWidth
        {
            get
            {
                return (double)GetValue(ImageHeightWidthProperty);
            }

            set
            {
                SetValue(ImageHeightWidthProperty, value);
            }
        }

        #endregion

        #region PROPERTY ImageHeightWidthProperty

        public static readonly BindableProperty TextHeightProperty = BindableProperty.Create(nameof(TextHeight), typeof(double)
            , typeof(SKIAImageTextButton), propertyChanged: OnTextHeightChanged, defaultValue: 15d);

        private static void OnTextHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is SKIAImageTextButton btn)
            {
                if(double.TryParse(newValue.ToString(), out double result) && result > 0)
                {
                    btn.skiaText.HeightRequest = result;
                    btn.skiaText.InvalidateSurface();
                }
                else
                {
                    throw new ArgumentException("Invalid value for TextHeight");
                }
            }
        }
        public double TextHeight
        {
            get
            {
                return (double)GetValue(TextHeightProperty);
            }

            set
            {
                SetValue(TextHeightProperty, value);
            }
        }

        #endregion

        #region PROPERTY ImageSourceProperty

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(SKIAImageTextButton), propertyChanged: OnImageSourcePropertyChanged);

        private static void OnImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is SKIAImageTextButton btn)
            {
                if(newValue is string image)
                { 
                    btn.img.Text = image;
                }
            }
        }

        public string ImageSource
        {
            get
            {
                return (string)GetValue(ImageSourceProperty);
            }

            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        #endregion

        #region PROPERY ButtonCommandProperty

        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(SKIAImageTextButton), propertyChanged: OnButtonCommandChanged);

        private static void OnButtonCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SKIAImageTextButton btn)
            {
                btn.ButtonCommand = newValue as ICommand ?? null;
            }
        }

        public ICommand? ButtonCommand
        {
            get
            {
                return (ICommand)GetValue(ButtonCommandProperty);
            }

            set
            {
                SetValue(ButtonCommandProperty, value);
            }
        }

        #endregion

        #region PROPERTY ButtonColorProperty

        public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Color)
            , typeof(SKIAImageTextButton), propertyChanged: OnButtonColorChanged, defaultValue: Colors.Black);

        private static void OnButtonColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SKIAImageTextButton btn)
            {
                if (newValue is Color result)
                {
                    btn.SetTextColor(result);
                }
                else
                {
                    throw new ArgumentException("Invalid value for ButtonColor");
                }
            }
        }

        public Color ButtonColor
        {
            get
            {
                return (Color)GetValue(ButtonColorProperty);
            }

            set
            {
                SetValue(ButtonColorProperty, value);
            }
        }

        #endregion

        #region PROPERTY IsButtonEnableProperty

        public static readonly BindableProperty IsButtonEnableProperty = BindableProperty.Create(nameof(IsButtonEnable), typeof(bool)
            , typeof(SKIAImageTextButton), propertyChanged: OnIsEnableButtonChanged
            , defaultValue: true);
        private static void OnIsEnableButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is SKIAImageTextButton btn)
            {
                if (newValue is bool ok)
                {
                    if (!ok)
                    {
                        btn.Disable();
                    }
                    else
                    {
                        btn.Enable();
                    }
                }
            }        
        }

        public bool IsButtonEnable
        {
            get
            {
                return (bool)GetValue(IsButtonEnableProperty);
            }

            set
            {
                SetValue(IsButtonEnableProperty, value);
            }
        }


        #endregion

        #region PROPERTY IsButtonSelecedProperty

        public static BindableProperty IsButtonSelectedProperty = BindableProperty.Create(nameof(IsButtonSelected), typeof(bool), typeof(SKIAImageTextButton), propertyChanged: OnIsButtonSelectedChanged);

        private static void OnIsButtonSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SKIAImageTextButton btn)
            {
                if (newValue is bool ok)
                {
                    if (ok)
                    {
                        btn.Select();
                    }
                    else
                    {
                        btn.Deselect();
                    }
                }
            }
        }

        public bool IsButtonSelected
        {
            get
            {
                return (bool)GetValue(IsButtonSelectedProperty);
            }

            set
            {
                SetValue(IsButtonSelectedProperty, value);
            }
        }

        #endregion

        #region PROPERTY ButtonBackgroundColorProperty

        public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(SKIAImageTextButton), defaultValue: Colors.Transparent, propertyChanged: OnButtonBackgroundColorChanged);

        private static void OnButtonBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SKIAImageTextButton btn)
            {
                if(newValue is Color result)
                { 
                    btn.orgColor = result;
                    btn.SetBackgroundColor(result);
                }
                else
                {
                    throw new ArgumentException("Invalid value for ButtonBackgroundColor");
                }
            }
        }

        public Color? ButtonBackgroundColor
        {
            get
            {
                return (Color?)GetValue(ButtonBackgroundColorProperty);
            }

            set
            {
                SetValue(ButtonBackgroundColorProperty, value);
            }
        }

        #endregion

        #region PROPERY IsSimpleClickProperty

        public static readonly BindableProperty IsSimpleClickProperty = BindableProperty.Create(nameof(IsSimpleClick), typeof(bool), typeof(SKIAImageTextButton), defaultValue: false);

        public bool IsSimpleClick
        {
            get
            {
                return (bool)GetValue(IsSimpleClickProperty);
            }

            set
            {
                SetValue(IsSimpleClickProperty, value);
            }
        }

        #endregion

        #region PROPERTY IsForwardAnimRunning

        public bool IsForwardAnimRunning
        {
            get => isForwardAnimRunning;
            set
            {
                if (value)
                {
                    IsBackwardAnimRunning = false;
                }

                isForwardAnimRunning = value;
                OnPropertyChanged(nameof(IsForwardAnimRunning));
            }
        }

        #endregion

        #region PROPERTY IsBackwardAnimRunning

        public bool IsBackwardAnimRunning
        {
            get => isBackwardAnimRunning;
            set
            {
                if (value)
                {
                    IsForwardAnimRunning = false;
                }

                isBackwardAnimRunning = value;
                OnPropertyChanged(nameof(IsBackwardAnimRunning));
            }
        }

        #endregion

        #region Cstr

        public SKIAImageTextButton()
        {
            InitializeComponent();
            skiaText.HeightRequest = 15;
            ColorTypeConverter c = new ColorTypeConverter();
            ButtonBackgroundColor = (Color?)c.ConvertFromInvariantString(COMMON.SConsts.COLOR_1);
        }

        #endregion

        #region METHOD SetTextColor
        private void SetTextColor(Color txtColor)
        {
            //var beh = img.Behaviors.OfType<IconTintColorBehavior>().FirstOrDefault();

            //if (beh != null)
            //{
            //    beh.TintColor = txtColor;
            //}
            img.TextColor = txtColor;
            frame.BorderColor = txtColor;
            skiaText.InvalidateSurface();
        }

        #endregion

        #region METHOD Deselect

        private void Deselect()
        {
            IsButtonSelected = false;
            frame.BorderThickness = 0;
        }

        #endregion

        #region METHOD Select

        private void Select()
        {
            IsButtonSelected = true;
            frame.BorderThickness = 3;
        }

        #endregion

        #region METHOD SetBackgroundColor

        private void SetBackgroundColor(Color buttonBackgroundColor)
        {
            frame.BackgroundColor = buttonBackgroundColor;
        }

        #endregion

        #region METHOD Disable
        private void Disable()
        {
            IsEnabled = false;
            gContainer.Opacity = 0.2;
            SetTextColor(ButtonBackgroundColor ?? orgColor);
            SetBackgroundColor(ButtonColor);
        }

        #endregion

        #region METHOD Enable

        private void Enable()
        {
            IsEnabled = true;
            gContainer.Opacity = 1;
            SetTextColor(ButtonColor);
            SetBackgroundColor(ButtonBackgroundColor ?? orgColor);
        }

        #endregion

        #region EVENT SKCanvasView_PaintSurface

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (frame.BorderColor is null)
                return;

            if (string.IsNullOrWhiteSpace(Text))
                return;

            var surface = e.Surface;
            var info = e.Info;
            var canvas = surface.Canvas;

            canvas.Clear();

            var skColor = frame.BorderColor.ToSKColor();

            SKRect textBounds = new SKRect();

            SKPaint paintText = new SKPaint();
            paintText.Style = SKPaintStyle.Fill;
            paintText.FakeBoldText = true;
            paintText.TextEncoding = SKTextEncoding.Utf32;
            paintText.TextAlign = SKTextAlign.Center;
            paintText.TextSize = 44;
            paintText.Color = skColor;

            float xText = info.Width / 2;// - textBounds.MidX;
            float yText = info.Height - 6;// - textBounds.MidY;

            var width = paintText.MeasureText(Text, ref textBounds);

            if (width > info.Width)
            {
                paintText.TextSize = 0.8f * (info.Width) * paintText.TextSize / width;
                paintText.MeasureText(Text, ref textBounds);
            }

            if ((info.Height) < (textBounds.Height))
            {
                paintText.TextSize = 0.8f * (info.Height) * paintText.TextSize / textBounds.Height;
                paintText.MeasureText(Text, ref textBounds);
                yText = info.Height - 6;//- textBounds.Height;// + textBounds.Height /2;// - textBounds.MidY;
            }

            canvas.DrawText(Text, xText, yText, paintText);
        }

        #endregion

        private async void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
        {
            if (ButtonCommand != null)
            {
                img.RotationY = 0;

                await img.RotateYTo(360, 200, easing: Easing.SinInOut);
                COMMON.ViewModelBase.ClickedButton = this;
                IsForwardAnimRunning = true;
                ButtonCommand.Execute(null);

                if (IsSimpleClick)
                {
                    await Task.Delay(200);
                    IsBackwardAnimRunning = true;
                }
            }
        }
    }
}