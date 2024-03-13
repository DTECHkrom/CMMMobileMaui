namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : PageBase<VM.VMMainView>
    {
        #region Cstr

        public MainView()
        {
            InitializeComponent();

            this.Title = "Urządzenia";

            //COMMON.SConsts.SetGlobalAction(COMMON.SConsts.DEV_ZEBRA, () =>
            //{
            //   // App.CurrentPage = _vmMain;
            //});

            if (!App.IsZebraScanIconVisible)
            {
                var parent = zebraIcon.Parent as StackLayout;
                parent.Children.Remove(zebraIcon);
            }
            else
            {
                ViewModel.InitScannerOnPage(zebraIcon);
            }
        }

        #endregion

        #region EVENT OnAppearing
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //  COMMON.SConsts.RunMainFrameAnimationOnPage(this, new List<Frame>() { frameDevice, frameDocument }, new List<ListView>() { lvDeviceHist, lvFile });

            //   if (!ViewModel.IsBusy)
            //  {
            ViewModel.SetMainHistoryList();
            //  }
        }

        #endregion
    }
}