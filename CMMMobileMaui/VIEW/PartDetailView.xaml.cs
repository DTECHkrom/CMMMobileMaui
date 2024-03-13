using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PartDetailView : PageBase<VM.VMPartDetail>
	{
        #region Cstr
        public PartDetailView (int partID)
		{
			InitializeComponent ();

            ViewModel.SetBasePartID(partID);
            
            ViewModel.CanModPartQuantity = API.MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Add 
                | API.MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Edit_State;
		}

        public PartDetailView(int partID, bool isLockButtons):this(partID)
        {
            ViewModel.IsLockButtons = isLockButtons;
        }

        #endregion

        #region EVENT OnAppearing

        protected async override void OnAppearing()
        {
            await ViewModel.SetMainData();
            base.OnAppearing();
        }

        #endregion

        #region EVENT OnBackButtonPressed

        protected override bool OnBackButtonPressed()
        {
            if(!ViewModel.CanMoveBack)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }

        #endregion
    }
}