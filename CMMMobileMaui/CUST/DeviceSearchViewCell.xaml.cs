using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSearchViewCell : ViewCell
    {
        #region FIELDS

        //      public static int DisplayIndex;

        //   private VM.VMDeviceSearchViewCell _vmMain;
        private Color orgColor;
        //private SKPaint paintBackground;
        //private SKPaint paintDarkBackground;
        //private SKPaint paintExtraDarkBackground;
        //private SKColor colorBackground;
        //private SKColor colorDarkBackground;
        //private SKColor colorExtraDarkBackground;

        //   double scrollValue;
        // IViewLocationFetcher viewLocationFetcher;

        #endregion
        public DeviceSearchViewCell()
        {
            InitializeComponent();

            //MessagingCenter.Subscribe<SendMsgObject, double>(this, SConsts.SC_DEV_SEARCH,
            //    (arg, scrollInfo) =>
            //    {
            //        // store away the scroll value
            //        scrollValue = scrollInfo;

            //        // tell the cell to redraw
            //        if (vcSearchSKIA != null)
            //            vcSearchSKIA.InvalidateSurface();
            //    });

            //viewLocationFetcher = DependencyService.Get<IViewLocationFetcher>();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext is MODEL.DeviceModel device)
            {
                //   lblIndexDeviceSearch.Text = $"{++DisplayIndex}";
                SetItemBackground();
            }
        }

        public void SetItemBackground()
        {

            #region SKIA SETT

            orgColor = Color.FromArgb(SConsts.GetStateColor(((MODEL.DeviceModel)this.BindingContext).BaseItem.StateID));

            gMain.BackgroundColor = orgColor;
            //colorBackground = SKColor.Parse(orgColor.WithLuminosity(orgColor.GetLuminosity() + 0.2f).ToHex());
            //colorDarkBackground = SKColor.Parse(orgColor.WithLuminosity(orgColor.GetLuminosity() + 0.1f).ToHex());
            //colorExtraDarkBackground = SKColor.Parse(orgColor.ToHex());
            //paintBackground = new SKPaint() { Color = colorBackground };
            //paintDarkBackground = new SKPaint() { Color = colorDarkBackground };
            //paintExtraDarkBackground = new SKPaint() { Color = colorExtraDarkBackground };
            //lblIndexDeviceSearch.BackgroundColor = orgColor.WithLuminosity(orgColor.GetLuminosity() - 0.1f);
            ////  deviceMainPanel.BackgroundColor = Color.FromRgba(colorPanelBackground.Red, colorPanelBackground.Green, colorPanelBackground.Blue, colorPanelBackground.Alpha);

            //if (vcSearchSKIA != null)
            //{
            //    vcSearchSKIA.InvalidateSurface();
            //}

            #endregion
        }

        //private void vcSearchSKIA_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        //{
        //    if (paintBackground == null)
        //        return;

        //    SKImageInfo info = e.Info;
        //    SKSurface surface = e.Surface;
        //    SKCanvas canvas = surface.Canvas;

        //    canvas.Clear();

        ////    var thisCellPosition = viewLocationFetcher.GetCoordinates(this.View);

        //    canvas.DrawRect(info.Rect, paintBackground);

        //    using (SKPath path = new SKPath())
        //    {
        //        path.MoveTo(0, 0);
        //        path.LineTo((info.Width) - thisCellPosition.Y, 0);
        //        path.LineTo(0, info.Height * .8f);
        //        path.Close();
        //        canvas.DrawPath(path, paintDarkBackground);
        //    }

        //    using (SKPath path = new SKPath())
        //    {
        //        path.MoveTo(0, 0);
        //        path.LineTo(info.Width - (thisCellPosition.Y * 2f), 0);
        //        path.LineTo(0, info.Height * .6f);

        //        path.Close();
        //        canvas.DrawPath(path, paintExtraDarkBackground);
        //    }
        //}
    }
}