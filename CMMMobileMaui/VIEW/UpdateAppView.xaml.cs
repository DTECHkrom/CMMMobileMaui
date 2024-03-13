using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAppView : PageBase<VM.VMUpadeApp>
    {
        #region Cstr
        public UpdateAppView()
        {
            InitializeComponent();

            ViewModel.MainParent = this;
            ViewModel.CurrentVersion = App.Version;
            ViewModel.NewVersion = VM.VMStartPage.LastAppVersion;

        }

        #endregion

        #region EVENT OnBackButtonPressed

        protected override bool OnBackButtonPressed()
        {
            if(ViewModel.ProgressValue != 0)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

        #endregion
    }
}