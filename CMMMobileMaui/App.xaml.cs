using CMMMobileMaui.COMMON;
using CMMMobileMaui.COMMON.Resources;
using DBMain;
using Telerik.Maui;

namespace CMMMobileMaui
{
    public partial class App : Application
    {
        #region FIELDS

        private static App instance;
        public static bool isHost = false;
        private static bool isLogin = false;
        public static string Version = "2.0.0.4"; //była juz 1.0.0.9
      //  private static COMMON.IScannerService Scanner;
        public static COMPANY.Company CompanyData;
        public static event EventHandler<string> OnStartMessage;
        public static SCAN.ScanManager? CurrentScanManager;
        public static bool IsZebraScanIconVisible
        {
            get;
            set;
        }
        #endregion

        #region Cstr

        public App()
        {
            TelerikLocalizationManager.Manager = new COMMON.CustomTelerikLocalizationManager();
            Engine.OnExceptionCatch += Engine_OnExceptionCatch;
            InitializeComponent();
            instance = this;

            ServiceProviderInitializer.Init();
            InitHostLoginData();

            var page = new MainShell();
            MainPage = page;
        }

        private void Engine_OnExceptionCatch(object? sender, string e)
        {
            #if ANDROID

            Android.Widget.Toast.MakeText(Android.App.Application.Context, e, Android.Widget.ToastLength.Long)?.Show();

            #endif
        }

        #endregion

        protected override Window CreateWindow(IActivationState? activationState)
        {
#if WINDOWS
            Window window = new Window();
            
#endif


            return base.CreateWindow(activationState);
        }

        public static App GetInstance() => instance;

        public void InitScanner(IScannerService scannerService)
        {

            if (scannerService is not null)
            {
                App.CurrentScanManager = new SCAN.ScanManager(scannerService);
                IsZebraScanIconVisible = true;
            }
        }

        protected async override void OnStart()
        {
            await CONV.TranslateExtension.InitLanguage(Thread.CurrentThread.CurrentCulture.Name, false);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void InitHostLoginData()
        {
            var apiManage = API.MainObjects.Instance.ServiceProvider?.GetService<API.Interfaces.IAPIManage>();

            if (apiManage != null)
            {
                API.MainObjects.Instance.Lang = Thread.CurrentThread.CurrentCulture.Name;

                var dictionaryManager = new DictionaryResourceManager(new DBSource()
               , new LocalSource());

                CONV.TranslateExtension.InitDictionarySource(dictionaryManager);

                API.Contracts.Models.COMMON.Container.SetDictionaryResource(dictionaryManager);

                COMMON.Settings.Login = "FlexiUser";
                COMMON.Settings.Pass = "Flexi2018";

#if RELEASEPROD
                //COMMON.Settings.WebAPI = "10.16.241.37:4090";
                 COMMON.Settings.WebAPI = "10.1.41.22:1234"; //showroom
             //     COMMON.Settings.WebAPI = "10.1.41.22:4444"; //Lfexi_AC
                // COMMON.Settings.WebAPI = "192.168.1.84:1234"; // PUK
              //  apiManage.IsSSL = true;
              //  COMMON.Settings.WebAPI = "cmms.puk-hajnowka.pl:1443"; // PUK_EXT
#else
                //   COMMON.Settings.WebAPI = "10.1.40.128:5432";

                //  COMMON.Settings.WebAPI = "10.1.40.128:1234"; 

                //  COMMON.Settings.WebAPI = "10.16.155.19:1234";
                //    COMMON.Settings.WebAPI = "10.1.41.22:1234";
                // COMMON.Settings.WebAPI = "10.1.41.22:4444";
                 COMMON.Settings.WebAPI = "10.1.40.128:1122"; //DTECH NEW

                // COMMON.Settings.WebAPI = "10.1.42.10:1234"; //CEZAR
                //   COMMON.Settings.WebAPI = "10.1.40.128:9999"; //CYMMES_AC ON 10.1.40.34

                //    apiManage.IsSSL = true;
                //    COMMON.Settings.WebAPI = "cmms.puk-hajnowka.pl:1443"; // PUK_EXT
                // COMMON.Settings.WebAPI = "10.16.241.37:4090";
#endif

                if (COMMON.Settings.IsSettings())
                {
                    apiManage.Host = COMMON.Settings.WebAPI;
                    apiManage.Login = COMMON.Settings.Login;
                    apiManage.Pass = COMMON.Settings.Pass;
                }

                apiManage.OnError += Instance_OnError1;
                apiManage.OnSuccess += Instance_OnSuccess1;
            }
            //await SetInitData();
        }

        private void Instance_OnSuccess1(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Current?.MainPage != null)
                {
                    await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");
                }
            });
        }

        private void Instance_OnError1(object? sender, EventArgs e)
        {
            if (sender is string str && !string.IsNullOrEmpty(str))
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (Current?.MainPage != null)
                    {
                        await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Error, str);
                    }
                });
            }
        }
    }
}
