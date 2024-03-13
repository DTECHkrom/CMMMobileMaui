using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using CMMMobileMaui.COMMON;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using FileProvider = AndroidX.Core.Content.FileProvider;
//using Uri = Android.Net.Uri;
using Net = Android.Net;

[assembly: Dependency(typeof(CMMMobileMaui.DownloadVersion))]

namespace CMMMobileMaui
{
    public class DownloadVersion : IDownloadVersion
    {
        private CUST.SKIAImageTextButton btn;
        //  private RadButton btnCancel;
        private ProgressBar pbar;
        private Label lblInfo;
        private WebClient wc;
        private string filePath;
        private string basePath;
        public event EventHandler<bool> OnStopInstallation;

        public void DownloadApp(string version
            , VisualElement parent
            , bool isHttps)
        {
            CheckAppPermissions();

            var context = MainActivity.Instance.ApplicationContext;
            var packageName = context.PackageName;

            basePath = context.GetExternalFilesDir("").Path;

            var fileName = $"{packageName}_{version}.apk";

            filePath = Path.Combine(basePath, fileName);

            try
            {
                btn = parent.FindByName<CUST.SKIAImageTextButton>("btnDownload");
                // btnCancel = parent.FindByName<RadButton>("btnDownCancel");
                pbar = parent.FindByName<ProgressBar>("pbar");
                lblInfo = parent.FindByName<Label>("lblInfo");

                btn.IsEnabled = false;
                //  btnCancel.IsVisible = true;
                pbar.IsVisible = true;
                lblInfo.IsVisible = true;

                if (File.Exists(filePath))
                {
                    var temp = Path.GetFileNameWithoutExtension(fileName);
                    temp = $"{temp}_{DateTime.Now.ToString("yyMMddHHmmss")}.apk";
                    filePath = Path.Combine(basePath, temp);
                }

                ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;

                wc = new WebClient();
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                //TODO FIX THIS

                Uri uri = null;

                if (isHttps)
                {
                    uri = new Uri($"https://{Settings.WebAPI}/downloads/{fileName}");
                }
                else
                {
                    uri = new Uri($"http://{Settings.WebAPI}/downloads/{fileName}");
                }

                wc.DownloadFileAsync(uri, filePath);
            }
            catch (Exception ex)
            {
                OnStopInstallation?.Invoke(this, false);
                new AlertDialog.Builder(MainActivity.Instance)
                    .SetMessage(ex.Message).Show();
            }
        }

        private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pbar.Progress = ((double)e.ProgressPercentage / 100);
            lblInfo.Text = $"Trwa pobieranie... [{((double)e.BytesReceived / 1000000).ToString("0.##")} / {((double)e.TotalBytesToReceive / 1000000).ToString("0.00")}]";
        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            btn.IsEnabled = true;
            pbar.IsVisible = false;
            lblInfo.IsVisible = false;
            //   btnCancel.IsVisible = false;

            if (e.Cancelled)
            {
                wc.Dispose();
                wc = null;
                pbar.Progress = 0;
                OnStopInstallation?.Invoke(this, false);

                return;
            }
            else
            {
                OnStopInstallation?.Invoke(this, true);

                StartAppInstallAfterDownload();
            }
        }

        private void StartAppInstallAfterDownload()
        {
            string Extension = global::Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(filePath);
            string MimeType = global::Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(Extension);

            var file = new Java.IO.File(filePath);
            var fileURI = Net.Uri.FromFile(file);

            // if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            //  {
            fileURI = FileProvider.GetUriForFile(MainActivity.Instance.ApplicationContext
                , MainActivity.Instance.ApplicationContext.PackageName + ".fileprovider",
                    file);
            //  }

            Intent install = new Intent(Intent.ActionView, fileURI);
            install.PutExtra(Intent.ExtraNotUnknownSource, true);
            install.SetDataAndType(fileURI, MimeType);
            install.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
            install.AddFlags(ActivityFlags.GrantReadUriPermission);
            MainActivity.Instance.StartActivity(install);
        }

        private void CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                if (ContextCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(MainActivity.Instance, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
                }

                if (ContextCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(MainActivity.Instance, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
                }
            }
        }

        public void CancelDownload()
        {
            if (wc != null)
            {
                wc.CancelAsync();
            }
        }
    }
}