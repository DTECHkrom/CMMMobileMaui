using Mopups.Pages;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartCorrection : PopupPage
    {
        public PartCorrection()
        {
            InitializeComponent();
            tboxAmount.Focus();
        }
    }
}