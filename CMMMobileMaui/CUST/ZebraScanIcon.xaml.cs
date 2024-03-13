using SkiaSharp;
using SkiaSharp.Views.Maui;
using CMMMobileMaui.COMMON;
using System.Windows.Input;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Animations;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZebraScanIcon : Grid, SCAN.IScanIcon
    {
        #region FIELDS

        private Color orgColor;
        private Color defaultColor = Colors.Blue;
      //  private SKPaint paintBackground;
       // private SKColor colorBackground;

      //  private bool isPageActive = false;
        private bool isDown = true;
        private float heightScale = 0f;
        //  private Task animationTask;

        #endregion

        #region PROPERTY TapCommandProperty

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
                       propertyName: nameof(TapCommand),
                                  returnType: typeof(ICommand),
                                             declaringType: typeof(ZebraScanIcon),
                                                        defaultValue: null,
                                                                   defaultBindingMode: BindingMode.TwoWay
            , propertyChanged: OnTapCommandPropertyChanged);

        private static void OnTapCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is ZebraScanIcon zebraScanIcon)
            {
                var animationBehavior = zebraScanIcon.Behaviors.FirstOrDefault(x => x is AnimationBehavior) as AnimationBehavior;

                if(animationBehavior is not null)
                {
                    animationBehavior.Command = (ICommand)newValue;
                }
                //zebraScanIcon.TapCommand = (ICommand)newValue;
            }
        }

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        #endregion


        #region Cstr

        public ZebraScanIcon()
        {
            InitializeComponent();

            AnimationBehavior animationBehavior = new();
            FadeAnimation fadeAnimation = new();
            fadeAnimation.Opacity = 0.5;
            animationBehavior.AnimationType = fadeAnimation;

            this.Behaviors.Add(animationBehavior);

          //  gvMain.BindingContext = this;

            //SetItemBackground();
        }

        #endregion

        //#region METHOD SetItemBackground
        //public void SetItemBackground()
        //{
        //    #region SKIA SETT

        //    orgColor = defaultColor;

        //    colorBackground = SKColor.Parse(orgColor.WithLuminosity(orgColor.GetLuminosity() + 0.2f).ToHex());
        //    paintBackground = new SKPaint() { Color = colorBackground };

        //    #endregion
        //}

        //#endregion

        //#region EVENT vcSearchSKIA_PaintSurface
        //private void vcSearchSKIA_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        //{
        //    if (paintBackground == null)
        //        return;

        //    SKImageInfo info = e.Info;
        //    SKSurface surface = e.Surface;
        //    SKCanvas canvas = surface.Canvas;

        //    canvas.Clear();

        //    SKRect rect = new SKRect(0, info.Height * (1 - heightScale), info.Width, info.Height * (1 - heightScale) + (info.Height * 0.33f));

        //    if (isDown)
        //    {
        //        paintBackground.Shader = SKShader.CreateLinearGradient(
        //                           new SKPoint(rect.Left, rect.Top),
        //                           new SKPoint(rect.Left, rect.Bottom),
        //                           new SKColor[] { SKColors.LightBlue, SKColors.White },
        //                           new float[] { 0, 1 },
        //                           SKShaderTileMode.Repeat);
        //    }
        //    else
        //    {
        //        paintBackground.Shader = SKShader.CreateLinearGradient(
        //                           new SKPoint(rect.Left, rect.Top),
        //                           new SKPoint(rect.Left, rect.Bottom),
        //                           new SKColor[] { SKColors.White, SKColors.LightBlue },
        //                           new float[] { 0, 1 },
        //                           SKShaderTileMode.Repeat);
        //    }

        //    canvas.DrawRect(rect, paintBackground);
        //}

        //#endregion

        #region METHOD SetBackgroudAfterScan

        public async void SetScanColorForMode(ScanMode mode)
        {
            if (mode == ScanMode.Start)
            {
             //   gvMain.IsVisible = false;
                border.BackgroundColor = Colors.Blue;
            }
            else
            {
                if (mode == ScanMode.Good)
                {
                    border.BackgroundColor = Colors.Green;
                }
                else if (mode == ScanMode.Fail)
                {
                    border.BackgroundColor = Colors.Red;
                }
                else if (mode == ScanMode.Empty)
                {
                    //CurrentScanStep = ScanStep.None;
                    //BoxCode = string.Empty;
                    //BoxAmount = string.Empty;
                    //UIScanAction?.Invoke();
                }

                await Task.Delay(200);
                border.BackgroundColor = Colors.Transparent;
             //   gvMain.IsVisible = true;
            }
        }

        #endregion

        //public void StartScanAnimation()
        //{
        //    IsRunning = true;

        //    //if (animationTask == null)
        //    //{
        //    //    animationTask = Task.Run(async () =>
        //    //    {
        //    //        while (isPageActive)
        //    //        {
        //    //            if (heightScale >= 1.3)
        //    //            {
        //    //                isDown = false;
        //    //            }
        //    //            else if (heightScale <= 0)
        //    //            {
        //    //                isDown = true;
        //    //            }

        //    //            if (isDown)
        //    //            {
        //    //                heightScale += 0.1f;
        //    //            }
        //    //            else
        //    //            {
        //    //                heightScale -= 0.1f;
        //    //            }

        //    //            vcSearchSKIA.InvalidateSurface();


        //    //            await Task.Delay(100);
        //    //        }
        //    //    });
        //    //}


        //}

        //public void StopScanAnimation()
        //{
        //    IsRunning = false;
        //    isPageActive = false;

        //    if (animationTask != null)
        //    {
        //        Task.WaitAny(animationTask);
        //        animationTask.Dispose();
        //        animationTask = null;
        //    }
        //}
    }
}