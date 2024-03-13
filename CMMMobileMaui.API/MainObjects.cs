using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Identity;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Contracts.v1.Responses.User;

namespace CMMMobileMaui.API
{
    public class MainObjects
    {
        private static Lazy<MainObjects> _instance = new Lazy<MainObjects>(() => new MainObjects());

        private MainObjects()
        { }

        public static MainObjects Instance =>
            _instance.Value;

        private LoginResponse? currentUser;
        public LoginResponse? CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }

        public GetUserObservedDeviceResponse? ObservedDevices
        {
            get;
            set;
        }

        public IServiceProvider? ServiceProvider
        {
            get;
            set;
        }

        public GetDeviceListResponse? CurrentDevice
        {
            get;
            set;
        }

        public GetAppSettResponse? AppSettings
        {
            get;
            set;
        }

        #region PROPERTY Lang

        public string Lang
        {
            get;
            set;
        } = "en-US";

        public void Logout(string lang)
        {
            CurrentUser = null;
            CurrentDevice = null;
            ObservedDevices = null;
            Lang = lang;


        }

        #endregion
    }
}
