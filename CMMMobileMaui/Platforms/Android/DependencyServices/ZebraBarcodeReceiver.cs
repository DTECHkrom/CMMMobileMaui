using Android.Content;
using Android.OS;
using CMMMobileMaui.COMMON;


[assembly:Dependency(typeof(CMMMobileMaui.ZebraBrodcastReceiver))]

namespace CMMMobileMaui
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class ZebraBrodcastReceiver : BroadcastReceiver, IScannerService
    {
        private Action<string> itemScanned;

        public Action<string> ItemScanned
        {
            get => itemScanned;
            set => itemScanned = value;
        }

        public ZebraBrodcastReceiver()
        {
            Console.WriteLine("Constructor ZebraBrodcastReceiver");
        }

        public override void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;
            Bundle b = intent.Extras;
            var r = context.Resources;

            if (action.Equals(r.GetString(Resource.String.activity_intent_filter_action)))
            {
                try
                {
                    displayScanResult(intent);
                }
                catch (Exception e)
                {
                    //  Catch if the UI does not exist when we receive the broadcast
                }
            }
        }

        private void displayScanResult(Intent initiatingIntent)
        {
            String decodedSource = initiatingIntent.GetStringExtra("com.symbol.datawedge.source");
            String decodedData = initiatingIntent.GetStringExtra("com.symbol.datawedge.data_string");
            String decodedLabelType = initiatingIntent.GetStringExtra("com.symbol.datawedge.label_type");

            if (!String.IsNullOrEmpty(decodedData))
            {
                ItemScanned?.Invoke(decodedData);
            }
        }
    }
}