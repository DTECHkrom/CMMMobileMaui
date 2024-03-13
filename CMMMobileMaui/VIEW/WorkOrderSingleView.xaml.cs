using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using System.Runtime.CompilerServices;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderSingleView : PageBase<VM.VMWorkOrderSingle>
    {

        #region Cstr

        public WorkOrderSingleView(GetWOsResponse? wo)
        {
            InitializeComponent();

            if (MainThread.IsMainThread)
                SetContent(wo);
            else
                MainThread.BeginInvokeOnMainThread(() => SetContent(wo));

            ViewModel.OnWOViewChange += _vmMain_OnWOViewChange;

        }

        private void SetContent(GetWOsResponse? wo)
        {
            if (wo is null)
            {
                ViewModel.CurrentWO = new GetWOsResponse();
                this.Content = new WorkOrderSingleEditView();
            }
            else
            {
                ViewModel.CurrentWO = wo;
                this.Content = new WorkOrderSingleStaticView();
            }

            if (Shell.Current.Navigation.ModalStack.Count == 0)
            {
                ViewModel.IsDeviceInfo = true;
            }
            else
            {
                ViewModel.IsDeviceInfo = false;
            }

        }

        private void _vmMain_OnWOViewChange(object? sender, bool e)
        {
            if (e)
            {
                if(this.Content.GetType() != typeof(WorkOrderSingleEditView))
                    this.Content = new WorkOrderSingleEditView();
            }
            else
            {
                if (this.Content.GetType() != typeof(WorkOrderSingleStaticView))
                    this.Content = new WorkOrderSingleStaticView();
            }
        }

        #endregion

        #region EVENT OnAppearing

        protected async override void OnAppearing()
        {
            await Task.Delay(100);

            if (!ViewModel.IsEdit)
            {
                ViewModel.SetMainData();
            }

            base.OnAppearing();
        }

        #endregion

        #region METHOD OnBackButtonPressed

        protected override bool OnBackButtonPressed()
        {
            if (!ViewModel.CanMoveBack)
            {
                return true;
            }

            // COMMON.SConsts.GetGlobalAction(COMMON.SConsts.WO_LOGIN)?.Invoke();

            return base.OnBackButtonPressed();
        }

        #endregion
    }
}