
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkOrderPartMainView : PageBase<VM.VMWorkOrderPartMain>
	{
        #region Cstr

        public WorkOrderPartMainView (int woID)
		{
			InitializeComponent ();



            ViewModel.CurrentSearchMode = VM.VMWorkOrderPartMain.PartSearchMode.WO;
            ViewModel.WorkOrderID = woID;

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

        #endregion

        #region METHOD

        protected override void OnAppearing()
        {
            this.ViewModel.SetMainData();
            
            base.OnAppearing();
        }

        #endregion
    }
}