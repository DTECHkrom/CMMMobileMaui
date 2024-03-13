using Microsoft.Maui.Controls.Xaml;
using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WOActivityPop : Mopups.Pages.PopupPage
    {

		public WOActivityPop()
		{
			InitializeComponent ();         
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

        }  
    }
}