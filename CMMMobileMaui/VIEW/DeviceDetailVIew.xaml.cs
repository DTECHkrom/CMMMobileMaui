using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceDetailVIew : PageBase<VM.VMDeviceDetail>
	{
        public DeviceDetailVIew(int devID, string name)
        {
            InitializeComponent();
            ViewModel.SetDeviceID(devID);
            this.Title = name;
        }

        protected override void OnAppearing()
        {
            Task.Run(ViewModel.SetMainData);

            base.OnAppearing();
        }
    }
}