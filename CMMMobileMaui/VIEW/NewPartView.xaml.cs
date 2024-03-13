using System;
using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPartView : PageBase<VM.VMNewPart>
	{

        #region Cstr

        public NewPartView ()
		{
			InitializeComponent ();
            ViewModel.OnClearNewDevice += _vmMain_OnClearNewDevice;
		}

        private void _vmMain_OnClearNewDevice(object sender, EventArgs e)
        {
            
        }

        #endregion

    }
}