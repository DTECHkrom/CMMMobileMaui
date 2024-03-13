#if IOS || MACCATALYST
using PlatformView = Microsoft.Maui.Platform.MauiTextField;
#elif ANDROID
using PlatformView = AndroidX.AppCompat.Widget.AppCompatEditText;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.TextBox;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif
using Microsoft.Maui.Handlers;
using CMMMobileMaui.CUST;


namespace CMMMobileMaui.Handlers
{
    public partial class CustomEntryHandler
    {
        public static PropertyMapper mapper = new PropertyMapper<CustomEntry, CustomEntryHandler>(ViewHandler.ViewMapper)
        {
            [nameof(CustomEntry.Text)] = MapTextProperty
        };

        public CustomEntryHandler() : base(mapper)
        {
        }
    }
}
