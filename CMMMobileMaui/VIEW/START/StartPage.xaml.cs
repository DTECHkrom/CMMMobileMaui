
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW.START
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(IsBackToStart), "back")]
    public partial class StartPage : PageBase<VM.VMStartPage>
    {

        public bool IsBackToStart
        {
            set
            {
                BackToStart();
            }
        }

        public StartPage()
        {
            InitializeComponent();
        }

        private void BackToStart()
        {
            ViewModel.InitData();
        }

        protected override void OnAppearing()
        {
            ViewModel.InitData();
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}