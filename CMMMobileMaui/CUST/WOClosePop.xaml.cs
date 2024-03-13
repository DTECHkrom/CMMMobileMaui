using Microsoft.Maui.Controls.Xaml;
using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WOClosePop : Mopups.Pages.PopupPage
    {
        private IWOCloser _vmMain;

		public WOClosePop()
		{
			InitializeComponent ();         
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _vmMain = this.BindingContext as IWOCloser;
        }  
    }
}