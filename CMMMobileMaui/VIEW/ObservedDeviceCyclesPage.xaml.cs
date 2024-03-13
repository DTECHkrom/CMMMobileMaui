using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObservedDeviceCyclesPage : PageBase<VM.VMObservedDeviceCycles>
    {
        public ObservedDeviceCyclesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            this.ViewModel.InitData();

            base.OnAppearing();
        }

    }
}