using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : PageBase<VM.VMLogin>
    {
        #region Cstr

        public LoginView()
        {
            InitializeComponent();

            if (!App.IsZebraScanIconVisible)
            {
              //  zebraIcon.DisableAnimation();

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

        public LoginView(bool isFromStart) : this()
        {
            ViewModel.IsFromStart = isFromStart;
        }

        #endregion

        #region EVENT OnAppearing

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (ViewModel != null)
            //{
            //    if (ViewModel.IsFromStart)
            //    {
            //        this.ToolbarItems.RemoveAt(1);
            //    }
            //}
        }

        #endregion

        #region EVENT OnBackButtonPressed

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        #endregion

    }
}