using static CMMMobileMaui.VM.VMWorkOrderPartMain;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartSearchView : PageBase<VM.VMWorkOrderPartMain>
    {

        #region Cstr

        public PartSearchView()
        {
            InitializeComponent();

            if (!App.IsZebraScanIconVisible)
            {
                var parent = zebraIcon.Parent as Grid;

                if (parent != null)
                {
                    parent.Children.Remove(zebraIcon);
                }
            }
            else
            {
                ViewModel.InitScannerOnPage(zebraIcon);
            }
        }

        #endregion

        #region EVENT

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.IsZebraScanIconVisible)
            {
                ViewModel.InitScannerOnPage(zebraIcon);
            }

            this.ViewModel.SetMainPartData();
        }

        #endregion

        #region METHOD SetSearchMode

        public void SetSearchMode(PartSearchMode mode)
        {
            this.ViewModel.CurrentSearchMode = mode;
        }

        #endregion

    }
}