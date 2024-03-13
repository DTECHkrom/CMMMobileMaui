namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderFileMainView : PageBase<VM.VMWorkOrderFileMain>
    {
        #region Cstr
        public WorkOrderFileMainView(int workOrderID)
        {
            InitializeComponent();

            // ViewModel.WorkOrderID = workOrderID;
        }

        #endregion

        #region EVENT OnAppearing

        protected override void OnAppearing()
        {
            // ViewModel.SetMainData();

            base.OnAppearing();
        }

        #endregion

        #region METHOD OnBackButtonPressed

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.IsBusy)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

        #endregion

    }
}