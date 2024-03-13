namespace CMMMobileMaui.COMMON
{
    public static class Settings
    {
        #region CONST

        private const string _webAPI = "WebAPI";
        private const string _login = "LOGIN";
        private const string _pass = "PASS";
        private const string _cmmLogin = "CMMLOGIN";
        private const string _cmmPass = "CMMPASS";
        private const string _cmmIsLogin = "CMMISLOGIN";
        private const string _cmmID = "CMMID";

        #endregion

        #region PROPERTY AppSettings

        public static IPreferences AppSettings
        {
            get
            {
                return Preferences.Default;
            }
        }

        #endregion

        #region PROPERTY WebAPI

        public static string WebAPI
        {
            get
            {
                return AppSettings.Get(_webAPI, string.Empty);
            }

            set
            {
                AppSettings.Set(_webAPI, value);
            }
        }

        #endregion

        #region PROPERTY Pass

        public static string Pass
        {
            get
            {
                return AppSettings.Get(_pass, string.Empty);
            }

            set
            {
                AppSettings.Set(_pass, value);
            }
        }

        #endregion

        #region PROPERTY Login

        public static string Login
        {
            get
            {
                return AppSettings.Get(_login, string.Empty);
            }

            set
            {
                AppSettings.Set(_login, value);
            }
        }

        #endregion

        #region PROPERTY CMMLogin

        public static string CMMLogin
        {
            get
            {
                return AppSettings.Get(_cmmLogin, string.Empty);
            }

            set
            {
                AppSettings.Set(_cmmLogin, value);
            }
        }

        #endregion

        #region PROPERTY CMMPass

        public static string CMMPass
        {
            get
            {
                return AppSettings.Get(_cmmPass, string.Empty);
            }

            set
            {
                AppSettings.Set(_cmmPass, value);
            }
        }

        #endregion

        #region PROPERTY CMMIsLogin

        public static bool CMMIsLogin
        {
            get
            {
                return AppSettings.Get(_cmmIsLogin, false);
            }

            set
            {
                AppSettings.Set(_cmmIsLogin, value);
            }
        }

        #endregion

        #region PROPERTY CMMId

        public static int CMMId
        {
            get
            {
                return (int)AppSettings.Get(_cmmID, -1);
            }

            set
            {
                AppSettings.Set(_cmmID, value);
            }
        }

        #endregion

        #region METHOD IsSettings

        public static bool IsSettings()
        {
            return !string.IsNullOrEmpty(Login)
                && !string.IsNullOrEmpty(Pass)
                && !string.IsNullOrEmpty(WebAPI);
        }

        #endregion

        #region METHOD Logout

        public static void Logout()
        {
            CMMId = -1;
            CMMIsLogin = false;
            CMMLogin = null;
            CMMPass = null;
            API.MainObjects.Instance.CurrentDevice = null;
        }

        #endregion
    }
}
