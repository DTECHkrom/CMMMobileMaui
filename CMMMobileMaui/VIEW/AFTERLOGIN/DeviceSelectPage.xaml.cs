namespace CMMMobileMaui.VIEW.AFTERLOGIN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSelectPage : PageBase<VM.AFTERLOGIN.VMDeviceSelect>
    {
        public DeviceSelectPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.InitData();
        }
    }
}