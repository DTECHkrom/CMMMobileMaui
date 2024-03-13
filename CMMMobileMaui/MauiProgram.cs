using Camera.MAUI;
using CMMMobileMaui.Handlers;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Telerik.Maui.Controls.Compatibility;
using ZXing.Net.Maui.Controls;

namespace CMMMobileMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCameraView()
                .UseSkiaSharp()
                .UseTelerik()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
                })
                .ConfigureEffects(configuration =>
                {
#if ANDROID
                    configuration.Add(typeof(COMMON.Effects.TouchEffect), typeof(Effects.TouchEffect));
#endif
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    //     handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryHandler));
                    //     handlers.AddHandler(typeof(NumericTextBox), typeof(CustomEntryRenderer));

#endif
                });


#if ANDROID
            DependencyService.Register<SerialNumberService>();
            DependencyService.Register<ZebraBrodcastReceiver>();
            DependencyService.Register<DownloadVersion>();
#endif


            FormHandler.RemoveBorders();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
