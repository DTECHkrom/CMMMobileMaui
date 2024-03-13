using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderSingleEditView : ContentView
    {
        private VM.VMWorkOrderSingle? vmMain;
        public WorkOrderSingleEditView()
        {
            InitializeComponent();

            //if (!App.IsZebraScanIconVisible)
            //{
            //    var parent = zebraIcon.Parent as Grid;

            //    if (parent != null)
            //    {
            //        parent.Children.Remove(zebraIcon);
            //    }
            //}
            //else
            //{
            //    zebraIcon.StartScanAnimation();
            //}
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext != null)
            {
                vmMain = this.BindingContext as VM.VMWorkOrderSingle;
                // vmMain.SetMainData();

                //  if(App.IsZebraScanIconVisible)
                // {
                if (vmMain is IScanItems scanItems)
                {
                    vmMain.InitScannerOnPage(zebraIcon);
                }
                //  }
            }
        }
    }
}