using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkOrderHistoryListView : PageBase<VM.VMWorkOrderHistory>
	{

        #region Cstr

        public WorkOrderHistoryListView (GetWOsResponse wo)
		{
			InitializeComponent ();
            ViewModel.CurrentWO = wo;
		}

        #endregion

        #region EVENT OnAppearing

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.SetMainData();        
        }

        #endregion
    }
}