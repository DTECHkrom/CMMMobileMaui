using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceDocumentationView : PageBase<VM.VMDeviceDocumentation>
	{
		public DeviceDocumentationView (string path)
		{
			InitializeComponent ();

            ViewModel.SetBasePath(path);
            ViewModel.SetStartData(path);
		}
    }
}