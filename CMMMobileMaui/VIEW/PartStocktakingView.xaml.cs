using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartStocktakingView : PageBase<VM.VMPartStocktaking>
    {

        #region Cstr

        public PartStocktakingView()
        {
            InitializeComponent();
        }

        #endregion

        #region EVENT OnAppearing

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(!ViewModel.IsBusy)
            {
                ViewModel.IsBusy = true;
            }
        }

        #endregion
    }
}