using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW.START
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartHostPage : PageBase<VM.VMStartPage>
    {
        public StartHostPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}