using AndroidX.AppCompat.Widget;
using CMMMobileMaui.CUST;
using Microsoft.Maui.Handlers;

namespace CMMMobileMaui.Handlers
{
    public partial class CustomEntryHandler : ViewHandler<CustomEntry, AppCompatEditText> //EntryHandler
    {
        protected override AppCompatEditText CreatePlatformView() =>
            new AppCompatEditText(Context);

        protected override void DisconnectHandler(AppCompatEditText nativeView)
        {
            nativeView.Dispose();
            base.DisconnectHandler(nativeView);
        }

        protected override void ConnectHandler(AppCompatEditText nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.Background = null;
            nativeView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            nativeView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            nativeView.SetPadding(0, 0, 0, 0);
        }

        public static void MapTextProperty(CustomEntryHandler handler, CustomEntry view)
        {
            handler.PlatformView.Text = view.Text;
        }
    }
}
