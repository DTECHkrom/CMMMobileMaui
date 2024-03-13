namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopCust : Mopups.Pages.PopupPage
    {
        public PopCust()
        {
            InitializeComponent();
            tboxAmount.Focus();
        }
    }
}