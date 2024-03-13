namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartPutBack : Mopups.Pages.PopupPage
    {
        public PartPutBack()
        {
            InitializeComponent();
            tboxAmount.Focus();
        }
    }
}