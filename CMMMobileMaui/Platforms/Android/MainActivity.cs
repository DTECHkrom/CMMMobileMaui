using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.Content;
using CMMMobileMaui.DeviceIdentifiresWrapper;

namespace CMMMobileMaui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private ZebraBrodcastReceiver? zebraBrodcast;
        internal static MainActivity? Instance { get; private set; }
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            Instance = this;

            if (Build.Manufacturer.Contains("Zebra Technologies")
               || Build.Manufacturer.Contains("Motorola Solutions"))
            {
                zebraBrodcast = new ZebraBrodcastReceiver();

                IntentFilter filter = new IntentFilter();

                filter.AddAction(Instance.Resources?.GetString(Resource.String.activity_intent_filter_action));
                filter.AddCategory(Intent.CategoryDefault);
                
                RegisterReceiver(zebraBrodcast, filter);

                App.GetInstance()
                    .InitScanner(zebraBrodcast);
            }

            GetSerialNumber();

            base.OnCreate(savedInstanceState);

        }

        protected override void OnResume()
        {
            base.OnResume();

            if(ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.Camera) != Permission.Granted)
            {
                Console.WriteLine("aaaa");
            }

            if (zebraBrodcast != null)
            {
                setIntentOutputPluginConfiguration();
            }
        }

        public void setIntentOutputPluginConfiguration()
        {
            Bundle bMain = new Bundle();

            Bundle bConfig = new Bundle();
            Bundle bParams = new Bundle();

            bParams.PutString("intent_output_enabled", "true");
            bParams.PutString("intent_action", Instance.Resources.GetString(Resource.String.activity_intent_filter_action));
            bParams.PutInt("intent_delivery", 2); //Use "0" for Start Activity, "1" for Start Service, "2" for Broadcast

            bConfig.PutString("PLUGIN_NAME", "INTENT");
            bConfig.PutBundle("PARAM_LIST", bParams);

            Bundle kConfig = new Bundle();
            Bundle kParams = new Bundle();

            kParams.PutString("keystroke_output_enabled", "false");
            kConfig.PutString("PLUGIN_NAME", "KEYSTROKE");

            kConfig.PutBundle("PARAM_LIST", kParams);

            Bundle bundleApp1 = new Bundle();

            bundleApp1.PutString("PACKAGE_NAME", Instance.PackageName);
            bundleApp1.PutStringArray("ACTIVITY_LIST", new String[] { "*" });
            // Add app list bundle into the main bundle
            bMain.PutParcelableArray("APP_LIST", new Bundle[]{
                bundleApp1 //other entries
            });

            var profileName = $"{this.Title.Replace(" ", "")}";

            bMain.PutBundle("PLUGIN_CONFIG", bConfig);
            bMain.PutString("PROFILE_NAME", profileName);
            bMain.PutString("PROFILE_ENABLED", "true");
            bMain.PutString("CONFIG_MODE", "CREATE_IF_NOT_EXIST");

            Intent i = new Intent();
            i.SetAction("com.symbol.datawedge.api.ACTION");
            i.PutExtra("com.symbol.datawedge.api.SET_CONFIG", bMain);
            i.PutExtra("SEND_RESULT", "LAST_RESULT");
            i.PutExtra("COMMAND_IDENTIFIER", "INTENT_API");

            this.SendBroadcast(i);

            bMain.PutBundle("PLUGIN_CONFIG", kConfig);
            bMain.PutString("PROFILE_NAME", profileName);
            bMain.PutString("PROFILE_ENABLED", "true");
            bMain.PutString("CONFIG_MODE", "CREATE_IF_NOT_EXIST");

            i.SetAction("com.symbol.datawedge.api.ACTION");
            i.PutExtra("com.symbol.datawedge.api.SET_CONFIG", bMain);
            i.PutExtra("SEND_RESULT", "LAST_RESULT");
            i.PutExtra("COMMAND_IDENTIFIER", "INTENT_API");

            this.SendBroadcast(i);
        }

        private void GetSerialNumber()
        {
            DIHelper.getSerialNumber(this, new ResultCallbacks(
                new CustomCallback(
                    (message) => { AddMessageToStatusText(message); },
                    (message) => { AddMessageToStatusText(message); },
                    (message) => { UpdateTextViewContent(message); },
                    (message) => { ResultTextViewContent(message); }
            )));
        }


        private void UpdateTextViewContent(string text)
        {
        }

        private void AddMessageToStatusText(string message)
        {
        }

        private void ResultTextViewContent(string text)
        {
            SerialNumberService.SerialNumber = text;
        }
    }

    public class CustomCallback : IDIResultCallbacks
    {
        private readonly System.Action<string> _debug;
        private readonly System.Action<string> _error;
        private readonly System.Action<string> _success;
        private readonly System.Action<string> _result;

        public CustomCallback(System.Action<string> debug
            , System.Action<string> error
            , System.Action<string> success
            , System.Action<string> result)
        {
            _debug = debug;
            _error = error;
            _success = success;
            _result = result;
        }

        public void OnDebugStatus(string message)
        {
            _debug(message);
        }

        public void OnError(string message)
        {
            _error(message);
        }

        public void OnSuccess(string message)
        {
            _success(message);
        }

        public void OnResult(string message) =>
            _result(message);
    }
}
