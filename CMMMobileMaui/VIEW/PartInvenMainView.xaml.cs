using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PartInvenMainView : PageBase<VM.VMPartInvenMain>
	{

        #region Cstr

        public PartInvenMainView(GetStocktakingViewResponse stack)
		{
			InitializeComponent();

            ViewModel.CurrentStack = stack;

            if (!App.IsZebraScanIconVisible)
            {
              //  zebraIcon.DisableAnimation();
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

        #region EVENT OnAppearing

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.SetMainData();

            //COMMON.SConsts.RunMainFrameAnimationOnPage(this,
            //    new List<Frame>() { framePartInv }
            //    , new List<ListView>() { lvPartInv }
            //    , null
            //    , 1000);
        }

        #endregion

    }
}