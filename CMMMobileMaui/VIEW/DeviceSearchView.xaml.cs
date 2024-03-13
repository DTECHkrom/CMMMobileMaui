using CMMMobileMaui.API.Contracts.v1.Responses.Device;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSearchView : PageBase<VM.VMDeviceSearch>
    {
        #region FIELDS

        public event EventHandler<GetDeviceListResponse>? OnDeviceSelected;

        #endregion

        #region Cstr

        public DeviceSearchView()
        {
            InitializeComponent();

            ViewModel.IsPartGive = false;
            ViewModel.OnDeviceSelected += _vmMain_OnDeviceSelected;

            ViewModel.IsPartGive = false;

            
        }

        protected override void OnAppearing()
        {
           // ViewModel.RefrehListCommand.Execute(null);
            base.OnAppearing();
        }


        private void _vmMain_OnDeviceSelected(object? sender, GetDeviceListResponse e)
        {
            OnDeviceSelected?.Invoke(sender, e);
        }

        //private void Eff_ScrollChanged(object arg1, XFUtils.Effects.ScrollEventArgs arg2)
        //{
        //    MessagingCenter.Send(new COMMON.SendMsgObject(), COMMON.SConsts.SC_DEV_SEARCH, arg2.Y);
        //}

        public DeviceSearchView(bool isPartGive = false, int? mainDeviceCategoryID = null) : this()
        {
            ViewModel.IsPartGive = isPartGive;
            ViewModel.MainDeviceCategoryID = mainDeviceCategoryID;
        }

        #endregion
    }
}