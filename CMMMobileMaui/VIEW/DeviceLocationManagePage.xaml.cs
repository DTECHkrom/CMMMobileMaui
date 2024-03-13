using CMMMobileMaui.VM;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceLocationManagePage : PageBase<VMDeviceLocationManager>
    {

        public DeviceLocationManagePage()
        {
            InitializeComponent();

            if (!App.IsZebraScanIconVisible)
            {
                var parent = zebraIcon.Parent as StackLayout;
                parent?.Children.Remove(zebraIcon);
            }
            else
            {
                ViewModel.InitScannerOnPage(zebraIcon);
            }
        }

        #region EVENT OnAppearing
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //  prevPage = App.CurrentPage;
            await ViewModel.InitData();
        }

        #endregion

    }
}