using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PartChangeView : PageBase
	{
		private COMMON.ViewModelBase vmMain;

		public PartChangeView ()
		{
			InitializeComponent ();
		}

        protected override void OnBindingContextChanged()
        {
            if(this.BindingContext != null)
            {
                vmMain = this.BindingContext as COMMON.ViewModelBase;
            }

            base.OnBindingContextChanged();
        }


        protected override bool OnBackButtonPressed()
        {
			if(vmMain != null)
            {
                if(!vmMain.CanMoveBack)
                {
                    return true;
                }
            }

            return base.OnBackButtonPressed();
        }
    }
}