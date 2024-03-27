using CMMMobileMaui.CUST;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;

namespace CMMMobileMaui.Handlers
{
    //public partial class CustomEntryHandler : ViewHandler<CustomEntry, TextBox>
    //{
    //    protected override TextBox CreatePlatformView() =>
    //        new TextBox();

    //    protected override void DisconnectHandler(TextBox nativeView)
    //    {
    //        base.DisconnectHandler(nativeView);
    //        nativeView.Tapped -= NativeView_Tapped;
    //    }

    //    protected override void ConnectHandler(TextBox nativeView)
    //    {
    //        base.ConnectHandler(nativeView);

    //        nativeView.Background = null;
    //        nativeView.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
    //        nativeView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
    //        nativeView.Tapped += NativeView_Tapped;
    //    }

    //    private void NativeView_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    //    {
    //        Console.WriteLine("tabbped");
    //    }

    //    public static void MapTextProperty(CustomEntryHandler handler, CustomEntry view)
    //    {
    //        handler.PlatformView.Text = view.Text;
    //    }
    //}
}
