using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.OS;
using Uri = Android.Net.Uri;

namespace CMMMobileMaui.DeviceIdentifiresWrapper
{
	public class ResultCallbacks : IDIResultCallbacks
	{
		private IDIResultCallbacks callbackInterface;
		private Context _context;
		private Uri _uri;
		private System.Action<ICursor,Uri, IDIResultCallbacks> _getUriValue;

		public ResultCallbacks(IDIResultCallbacks resultCallbacks)
		{
			callbackInterface = resultCallbacks;
		}

		public ResultCallbacks(IDIResultCallbacks resultCallbacks, Context context, Uri uri, System.Action<ICursor,Uri, IDIResultCallbacks> getUriValue)
		{
			callbackInterface = resultCallbacks;
			_context = context;
			_uri = uri;
			_getUriValue = getUriValue;
		}

		public void OnDebugStatus(string message)
		{
			if (callbackInterface != null)
			{
				callbackInterface.OnDebugStatus(message);
			}
		}

		public void OnError(string message)
		{
			if (callbackInterface != null)
			{
				callbackInterface.OnError(message);
				return;
			}
		}

        public void OnResult(string message)
        {
			callbackInterface.OnResult(message);
        }

        public void OnSuccess(string message)
		{
			// The app has been registered
			// Let's try again to get the identifier

			if(_uri == null)
            {
				OnResult(message);
				return;
            }

			var cursor2 = _context.ContentResolver.Query(_uri, null, null, null, null);
			if (cursor2 == null || cursor2.Count < 1)
			{
				if (callbackInterface != null)
				{
					callbackInterface.OnError("Fail to register the app for OEM Service call:" + _uri + "\nIt's time to debug this app ;)");
					return;
				}
			}
			_getUriValue(cursor2, _uri, callbackInterface);
			return;
		}
	}

	public class RetrieveOEMInfoTask : AsyncTask<object, bool, bool>
	{
		private static void RetrieveOEMInfo(Context context, Uri uri, IDIResultCallbacks callbackInterface)
		{
			//  For clarity, this code calls ContentResolver.query() on the UI thread but production code should perform queries asynchronously.
			//  See https://developer.android.com/guide/topics/providers/content-provider-basics.html for more information
			var cursor = context.ContentResolver.Query(uri, null, null, null, null);
			if (cursor == null || cursor.Count < 1)
			{
				if (callbackInterface != null)
				{
					callbackInterface.OnDebugStatus("App not registered to call OEM Service:" + uri.ToString() + "\nRegistering current application using profile manger, this may take a couple of seconds...");
				}
				// Let's register the application
				RegisterCurrentApplication(context, uri, new ResultCallbacks(callbackInterface, context, uri, (cursor, uri, callbacks) => { GetURIValue(cursor, uri, callbacks); }));
			}
			else
			{
				// We have the right to call this service, and we obtained some data to parse...
				GetURIValue(cursor, uri, callbackInterface);
			}
		}
		protected override bool RunInBackground(params object[] @params)
		{
			var context = (Context)@params[0];
			var uri = (Uri)@params[1];
			var idiResultCallbacks = (IDIResultCallbacks)@params[2];
			RetrieveOEMInfo(context, uri, idiResultCallbacks);
			return true;
		}

		private static void RegisterCurrentApplication(Context context, Uri serviceIdentifier, IDIResultCallbacks callbackInterface)
		{
			var profileName = "AccessMgr-1";
			var profileData = "";
			try
			{
				var packageInfo = context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.SigningCertificates);
				var path = context.ApplicationInfo.SourceDir;
				var strName = packageInfo.ApplicationInfo.LoadLabel(context.PackageManager).ToString();
				var strVendor = packageInfo.PackageName;
				//var sig = DIHelper.apkCertificate;

				//// Let's check if we have a custom certificate
				//if (sig == null)
				//{
				//	// Nope, we will get the first apk signing certificate that we find
				//	// You can copy/paste this snippet if you want to provide your own
				//	// certificate
				//	// TODO: use the following code snippet to extract your custom certificate if necessary
				//	var arrSignatures = packageInfo.SigningInfo.GetApkContentsSigners();
				//	if (arrSignatures == null || arrSignatures.Length == 0)
				//	{
				//		if (callbackInterface != null)
				//		{
				//			callbackInterface.OnError("Error : Package has no signing certificates... how's that possible ?");
				//			return;
				//		}
				//	}
				//	sig = arrSignatures[0];
				//}

				///*
				// * Get the X.509 certificate.
				// */
				//var rawCert = sig.ToByteArray();

				//// Get the certificate as a base64 string
				//var encoded = Base64.EncodeToString(rawCert, Base64Flags.Default);

				var encoded = "MIIDDTCCAfWgAwIBAgIEb/HyKzANBgkqhkiG9w0BAQsFADA3MQswCQYDVQQGEwJVUzEQMA4GA1UEChMHQW5kcm9pZDEWMBQGA1UEAxMNQW5kcm9pZCBEZWJ1ZzAeFw0xOTAzMTQwOTI4MDlaFw00OTAzMDYwOTI4MDlaMDcxCzAJBgNVBAYTAlVTMRAwDgYDVQQKEwdBbmRyb2lkMRYwFAYDVQQDEw1BbmRyb2lkIERlYnVnMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtLplUPNpR+VKiGr/DeW9ijukMBrMkmesnAmw5Z0rQzHlkDjYW/HzvHSvtgjm8SIkPKRd1uOeQRW4XGI5ur20//ujbe2HGAyOX8C/ILLa3leIG6rrIOgopCpo75Kwnbygk7qoum4vi4+3e7N4bYAt/CzbYfip9tsNYkct6fa2d5JCsk6GEaDgkv3xLvQc+4idLRr24OLtSZJflwuNei4WJ/hjNRJDiNhsb/vmK/G++MxRTR59cDzjC+EldIePJhGpUxlevQwcb+iSte8kZw2dpGssa1ay386Xqtiq0s/W458/uBdAoIh9T+n8TuHK9XsOnN9Rpgyh44fKpgjcXT4OlQIDAQABoyEwHzAdBgNVHQ4EFgQUkHmmCFDpmI3JxHw49Ocqhwe/8sswDQYJKoZIhvcNAQELBQADggEBAFZ6tn1nwO5fw6L0gcIgUiBUYhXhL1uLjdpgLq+xVv462xIqfLuUonaNIuqYngFnJliGSl0UF7IzQi6Yjv6CCdaeeZkezGokyhbG6XhZ9yVtqfO13uGVT0+8LPfzwJtvTPSN+BuvuIQ0Hw88hDeIMn5P4QOU0sV7HoWIl9A+6mMBn2ux6LjeDtwrtOLEje5k1AjSXxg8CnzKUjtsw967pEeutJX7w15XxBy7RWGu5NP23bcQxSbnUc/J+6dIvBG7LgtRYf6COJpAsjE5JcQ0KmfuSF9twXGapCWYogPb79rCbbT6uJjGhOcdLOYcgRnDaNsiQwwgMuFcREl7tpMvyF0=";

				profileData =
						"<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
								"<characteristic type=\"Profile\">" +
								"<parm name=\"ProfileName\" value=\"" + profileName + "\"/>" +
								"<characteristic type=\"AccessMgr\" version=\"10.1\">" +
								"<parm name=\"OperationMode\" value=\"1\" />" +
								"<parm name=\"ServiceAccessAction\" value=\"4\" />" +
								"<parm name=\"ServiceIdentifier\" value=\"" + serviceIdentifier + "\" />" +
								"<parm name=\"CallerPackageName\" value=\"" + context.PackageName.ToString() + "\" />" +
								"<parm name=\"CallerSignature\" value=\"" + encoded + "\" />" +
								"</characteristic>" +
								"</characteristic>";
				DIProfileManagerCommand profileManagerCommand = new DIProfileManagerCommand(context);
				profileManagerCommand.execute(profileData, profileName, callbackInterface);
			}
			catch (System.Exception e)
			{
				if (callbackInterface != null)
				{
					callbackInterface.OnError("Error on profile: " + profileName + "\nError:" + e.Message + "\nProfileData:" + profileData);
				}
			}
		}

		public static bool GetURIValue(ICursor cursor,Uri uri, IDIResultCallbacks resultCallbacks)
		{
			while (cursor.MoveToNext())
			{
				if (cursor.ColumnCount == 0)
				{
					//  No data in the cursor.  I have seen this happen on non-WAN devices
					var errorMsg = "Error: " + uri + " does not exist on this device";
					resultCallbacks.OnDebugStatus(errorMsg);
				}
				else
				{
					for (int i = 0; i < cursor.ColumnCount; i++)
					{
						try
						{
							var data = cursor.GetString(cursor.GetColumnIndex(cursor.GetColumnName(i)));
							resultCallbacks.OnSuccess(data);
							cursor.Close();
							return true;
						}
						catch (System.Exception e)
						{
							resultCallbacks.OnDebugStatus(e.Message);
						}
					}
				}
			}
			cursor.Close();
			resultCallbacks.OnError("Data not found in Uri:" + uri);
			return true;
		}
	}
}