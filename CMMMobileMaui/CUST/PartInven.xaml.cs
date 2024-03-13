using Mopups.Pages;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartInven : PopupPage
    {
        public PartInven()
        {
            InitializeComponent();
            tboxAmount.Focus();
        }
    }
}