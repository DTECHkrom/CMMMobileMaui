namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseListView : PageBase<VM.VMBaseList>
    {
        public BaseListView()
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

        protected override bool OnBackButtonPressed()
        {
            if (!ViewModel.CanMoveBack)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

    }
}