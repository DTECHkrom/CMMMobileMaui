using CMMMobileMaui.COMMON.PictureOperation;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListView : PageBase<VM.VMPictureList>
    {
        #region FIELDS

        private bool wasFirstLoad = false;

        #endregion

        #region Cstr

        public PictureListView(IPictureOperation operation)
        {
            InitializeComponent();
            ViewModel.Operation = operation;
        }

        #endregion

        protected override void OnAppearing()
        {
            //  pageHeader.RunAnimation();

            base.OnAppearing();

            if (!wasFirstLoad)
            {
                ViewModel.Init();
                wasFirstLoad = true;
            }
        }

        #region EVENT OnBackButtonPressed

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