
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.BIANOR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MouldCalendarChange : VIEW.PageBase<VMMouldCalendarChange>
    {
        #region Cstr
        public MouldCalendarChange()
        {
            InitializeComponent();

            if (!App.IsZebraScanIconVisible)
            {
                var parent = zebraIcon.Parent as StackLayout;
                parent.Children.Remove(zebraIcon);
            }
            else
            {
                ViewModel.InitScannerOnPage(zebraIcon);
            }
        }

#endregion
    }
}