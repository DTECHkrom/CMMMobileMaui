using System.Windows.Input;
using CMMMobileMaui.API;

namespace CMMMobileMaui.VM
{
    public class VMMainShell : COMMON.ViewModelBase
    {
        #region PROPERTY MainParent
        public MainShell MainParent
        {
            get;
            set;
        }

        #endregion

        #region COMMAND APISettingsCommand

        public ICommand APISettingsCommand
        {
            get;
        }

        #endregion

        #region COMMAND DownloadUpdateCommand

        public ICommand DownloadUpdateCommand
        {
            get;
        }

        #endregion

        #region COMMAND LogoutUserCommand

        public ICommand LogoutUserCommand
        {
            get;
        }

        #endregion

        #region PROPERTY IsNewVersion

        public bool IsNewVersion
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY NewVersion

        public string NewVersion
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentVersion

        private string currentVersion;

        public string CurrentVersion
        {
            get => currentVersion;
            set => SetProperty(ref currentVersion, value);
        }

        #endregion

        #region PROPERTY UserName

        private string userName;

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        #endregion

        #region Cstr

        public VMMainShell()
        {
            IsNewVersion = false;

            Routing.RegisterRoute($"//{nameof(VIEW.START.StartPage)}/{nameof(VIEW.UpdateAppView)}", typeof(VIEW.UpdateAppView));
            Routing.RegisterRoute($"//{nameof(VIEW.START.StartPage)}/{nameof(VIEW.START.StartHostPage)}", typeof(VIEW.START.StartHostPage));
            Routing.RegisterRoute($"//{nameof(VIEW.START.StartPage)}/{nameof(VIEW.START.StartHostPage)}/{nameof(VIEW.TestPage)}Host", typeof(VIEW.TestPage));
            Routing.RegisterRoute($"//{nameof(VIEW.START.StartPage)}/{nameof(VIEW.TestPage)}Host", typeof(VIEW.TestPage));
            // Routing.RegisterRoute($"//{nameof(VIEW.TestPage)}", typeof(VIEW.TestPage));

            APISettingsCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.TestPage(true, false));
                }
            });

            DownloadUpdateCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.UpdateAppView());
                }
            });

            LogoutUserCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    // MainObjects.Instance.Lang = CrossMultilingual.Current.DeviceCultureInfo.Name;
                   
                    MainObjects.Instance.Logout(Thread.CurrentThread.CurrentCulture.Name);
                    await CONV.TranslateExtension.InitLanguage(MainObjects.Instance.Lang, true);

                    //   CMMMobileMaui.API.MainObjects.Instance.CurrentUser = null;
                    COMMON.Settings.CMMPass = string.Empty;
                    // DictionaryResources.Instance.ClearDictionaries();
                    MainParent.FlyoutIsPresented = false;

                    await Shell.Current.GoToAsync($"//{nameof(VIEW.START.StartPage)}");
                }
            });
        }

        #endregion

    }
}
