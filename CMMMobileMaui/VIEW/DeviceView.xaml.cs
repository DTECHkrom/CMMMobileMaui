using SkiaSharp;
using SkiaSharp.Views.Maui;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceView : PageBase<VM.VMDevice>
    {
        #region FIELDS

        private SKPaint paintBackground;
        private SKPaint paintDarkBackground;
        private SKPaint paintExtraDarkBackground;
        private SKColor colorBackground;
        private SKColor colorDarkBackground;
        private SKColor colorExtraDarkBackground;
        private SKColor colorPanelBackground;
        private Color orgColor;

        #endregion

        #region Cstr

        public DeviceView(GetDeviceListResponse device, bool isWOPanel = true)
        {
            InitializeComponent();

            ViewModel.IsWOPanel = isWOPanel;
            ViewModel.CurrentDevice = device;

            COMMON.SConsts.SetGlobalAction(COMMON.SConsts.DEV_REF_BACK, () =>
            {
                SetColorForCurrentDevice();
            });

            SetColorForCurrentDevice();
        }

        #endregion

        #region METHOD SetColorForCurrentDevice

        private void SetColorForCurrentDevice()
        {
            orgColor = Color.FromArgb(COMMON.SConsts.GetStateColor(ViewModel.CurrentDevice.StateID));
            colorBackground = SKColor.Parse(orgColor.WithLuminosity(orgColor.GetLuminosity() + 0.2f).ToHex());
            colorPanelBackground = SKColor.Parse(orgColor.WithLuminosity(orgColor.GetLuminosity() + 0.4f).ToHex());
            colorDarkBackground = SKColor.Parse(orgColor.WithLuminosity(orgColor.GetLuminosity() + 0.1f).ToHex());
            colorExtraDarkBackground = SKColor.Parse(orgColor.ToHex());
            paintBackground = new SKPaint() { Color = colorBackground };
            paintDarkBackground = new SKPaint() { Color = colorDarkBackground };
            paintExtraDarkBackground = new SKPaint() { Color = colorExtraDarkBackground };

          //  canvasBackground.InvalidateSurface();

        }

        #endregion

        #region EVENT SKCanvasView_PaintSurface

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (paintBackground == null)
                return;

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            canvas.DrawRect(info.Rect, paintBackground);

            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(info.Width * .7f, 0f);
                path.LineTo(info.Width * .3f, info.Height);
                path.LineTo(0f, info.Height);
                path.LineTo(0f, 0f);

                path.Close();
                canvas.DrawPath(path, paintDarkBackground);
            }

            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(info.Width * .4f, 0f);
                path.LineTo(0f, info.Height * 0.7f);
                path.LineTo(0f, 0f);

                path.Close();
                canvas.DrawPath(path, paintExtraDarkBackground);
            }
        }

        #endregion
    }
}