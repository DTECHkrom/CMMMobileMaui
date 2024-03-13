using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CommunityToolkit.Mvvm.Input;

namespace CMMMobileMaui.VM
{
    public enum ScanLoginState
    {
        AppAccess,
        HostAccess,
    }

    public class VMStartPage : ViewModelBase
    {
        #region FIELDS

        private VMMainShell shellVM;
        private IAPIManage apiManage;
        private IOtherController otherController;
        private IUserController userController;
        private IIdentityController identityController;

        private bool wasUpdateCheck = false;
        private readonly WeakEventManager eventWeakManager = new WeakEventManager();

        public event EventHandler<ScanLoginState> OnScanLoginStateChange
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        public event EventHandler<bool> OnUpdateDownloaded
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        #endregion

        #region PROPERTY CurrentAccessLoginState

        private ScanLoginState currentAccessLoginState = ScanLoginState.AppAccess;

        public ScanLoginState CurrentAccessLoginState
        {
            get => currentAccessLoginState;
            set
            {
                SetProperty(ref currentAccessLoginState, value);
                eventWeakManager.HandleEvent(this, value, nameof(OnScanLoginStateChange));
            }
        }

        #endregion

        #region PROPERTY HostLoginMessage

        public string HostLoginMessage
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsHostControlsEnable

        public bool IsHostControlsEnable
        {
            get;
            set;
        } = true;

        #endregion

        #region PROPERTY ProgressValue

        public decimal ProgressValue
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ProgressMessage

        public string ProgressMessage
        {
            get;
            set;
        }

        #endregion   

        #region PROPERTY APIHost

        public string APIHost
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY APILogin

        public string APILogin
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY APIPass

        public string APIPass
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY LastAppVersion

        public static string LastAppVersion
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ApplicationLanguageList

        public List<string> ApplicationLanguageList
        {
            get;
            set;
        }

        #endregion

        #region COMMAND CheckHostCommand

        public IAsyncRelayCommand CheckHostCommand
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

        #region COMMAND CancelUpdateCommand

        public ICommand CancelUpdateCommand
        {
            get;
        }

        #endregion

        #region COMMAND LoginCommand

        public ICommand LoginCommand
        {
            get;
        }

        #endregion

        #region COMMAND ScanUserCodeCommand

        public ICommand ScanUserCodeCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowHostCommand

        public ICommand ShowHostCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMStartPage(VMMainShell shellVM, IAPIManage apiManage
            , IOtherController otherController
            , IUserController userController
            , IIdentityController identityController)
        {
            this.shellVM = shellVM;
            this.apiManage = apiManage;
            this.identityController = identityController;
            this.otherController = otherController;
            this.userController = userController;
            CONV.TranslateExtension.RefreshAppText = shellVM.MainParent.SetShellTextAfterLanguageChanged;

            CheckHostCommand = new AsyncRelayCommand(ConnectToHost);

            ShowHostCommand = new Command(() =>
            {
                GoToHostContentWithData(false);
            });
        }

        #endregion     

        #region METHOD SetProgressData

        private void SetProgressData(SConsts.StartPageProgressType type)
        {
            switch (type)
            {
                case SConsts.StartPageProgressType.Host:

                    ProgressValue = ((decimal)1 / 3);
                    ProgressMessage = CONV.TranslateExtension.GetResourceText("prog_host");

                    break;

                case SConsts.StartPageProgressType.Login:

                    ProgressValue = ((decimal)3 / 3);
                    ProgressMessage = CONV.TranslateExtension.GetResourceText("prog_login");

                    break;

                case SConsts.StartPageProgressType.Update:

                    ProgressValue = ((decimal)2 / 3);
                    ProgressMessage = CONV.TranslateExtension.GetResourceText("prog_update");

                    break;
            }

            OnPropertyChanged("ProgressValue");
            OnPropertyChanged("ProgressMessage");
        }

        #endregion

        #region METHOD InitData

        public async void InitData()
        {
            // App.CurrentPage = this;
            ProgressValue = 0;
            ProgressMessage = string.Empty;

            await Task.Delay(500);
            await CheckHostData();
        }

        #endregion

        #region METHOD CheckHostData

        private async Task CheckHostData()
        {
            SetProgressData(SConsts.StartPageProgressType.Host);

            if (Settings.IsSettings())
            {
                apiManage.Host = Settings.WebAPI;
                apiManage.Login = Settings.Login;
                apiManage.Pass = Settings.Pass;

                if (apiManage.IsDataSet())
                {
                    var isHostResp = await otherController.IsHost();

                    if (isHostResp.IsResponseWithData())
                    {
                        await CONV.TranslateExtension.InitLanguage(MainObjects.Instance.Lang, true);
                        await SetCompanyData();
                    }
                    else
                    {
                        GoToHostContentWithData(true);
                    }
                }
                else
                {
                    GoToHostContentWithData(true);
                }
            }
            else
            {
                GoToHostContentWithData(false);
            }
        }

        #endregion

        #region METHOD ConnectToHost

        private async Task ConnectToHost()
        {
            IsBusy = true;

            apiManage.Host = Settings.WebAPI;
            apiManage.Login = Settings.Login;
            apiManage.Pass = Settings.Pass;

            if (apiManage.IsDataSet())
            {
                var isHostResp = await otherController.IsHost();

                if (isHostResp.IsValid
                    && isHostResp.Data)
                {
                    await Shell.Current.GoToAsync("..");
                }
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD SetCompanyData
        private async Task SetCompanyData()
        {
            var appResponse = await otherController.GetAppSett();

            if (appResponse.IsResponseWithData(this))
            {
                MainObjects.Instance.AppSettings = appResponse.Data;
                App.CompanyData = COMPANY.CompanyFactory.GetAppCompany(appResponse.Data!);

                if (App.CompanyData != null)
                {
                    shellVM.MainParent.SetCompanyData();

                    if (!wasUpdateCheck)
                    {
#if RELEASEPROD
              //  wasUpdateCheck = true;
                      //  CheckApplicationVersion();

                        GoToLoginPage();
#else
                        CheckApplicationVersion();
#endif
                    }
                    else
                    {
                        GoToLoginPage();
                    }
                }
            }
        }

        #endregion

        #region METHOD GoToHostContentWithData

        private async void GoToHostContentWithData(bool isWithSettings)
        {
            // APIHost = CMMMobileMaui.API.APIManage.Instance.Host;
            //  APILogin = CMMMobileMaui.API.APIManage.Instance.Login;
            //  APIPass = CMMMobileMaui.API.APIManage.Instance.Pass;

            if (isWithSettings)
            {
                //ContentType = SConsts.StartPageViewType.Host;
                await Shell.Current.GoToAsync($"//{nameof(VIEW.START.StartPage)}/{nameof(VIEW.START.StartHostPage)}");

            }
            else
            {
                //  await Shell.Current.GoToAsync("..");
                await Shell.Current.CurrentPage.Navigation.PushModalAsync(new VIEW.TestPage());
                //   await Shell.Current.GoToAsync($"//{nameof(VIEW.START.StartPage)}/{nameof(VIEW.TestPage)}Host");
                // ContentType = SConsts.StartPageViewType.CheckConn;
            }
        }

        #endregion

        #region METHOD GoToLoginPage

        public async void GoToLoginPage()
        {
            SetProgressData(SConsts.StartPageProgressType.Login);

            if (!Settings.CMMIsLogin
                || string.IsNullOrEmpty(Settings.CMMPass))
            {
                await DictionaryResources.Instance.InitDataBeforeLogin();
                await Shell.Current.GoToAsync($"//{nameof(VIEW.TestPage)}Login");
            }
            else
            {
                var serialNumber = DependencyService.Get<ISerialNumberService>()
                    .GetSerialNumber();

                var personResponse = await identityController.LoginPass(new API.Contracts.v1.Requests.Identity.LoginPassRequest
                {
                    PersonID = (short)Settings.CMMId
                    ,
                    Password = Settings.CMMPass
                    ,
                    UserName = Settings.CMMLogin
                    ,
                    Mobile_Info = serialNumber
                });

                if (personResponse.IsValid
                    && personResponse.Data != null)
                {
                    MainObjects.Instance.CurrentUser = personResponse.Data;
                    MainObjects.Instance.Lang = personResponse.Data.LangCode;

                    await CONV.TranslateExtension.InitLanguage(MainObjects.Instance.Lang, true);

                    SetContentAfterLogin();
                }
                else
                {
                    await DictionaryResources.Instance.InitDataBeforeLogin();
                    await Shell.Current.GoToAsync($"//{nameof(VIEW.TestPage)}Login");
                }
            }
        }

        #endregion

        #region METHOD SetContentAfterLogin

        private async void SetContentAfterLogin()
        {
            shellVM.MainParent.SetUIForCurrentUserRights();

            var observedDevicesResult = await userController.GetObservedDevices(new API.Contracts.v1.Requests.User.GetUserObservedDeviceRequest
            {
                PersonID = MainObjects.Instance.CurrentUser.PersonID
            });

            if (observedDevicesResult.IsValid)
            {
                MainObjects.Instance.ObservedDevices = observedDevicesResult.Data;
            }

            var shouldOpenStartPageAfterLogin = await App.CompanyData.IsExtraContentAfterLogin();

            if (shouldOpenStartPageAfterLogin)
            {
                await Shell.Current.GoToAsync($"//AfterLoginPage");
            }
            else
            {
                await Shell.Current.GoToAsync($"//device");
            }
        }

        #endregion

        #region METHOD CheckApplicationVersion

        private async void CheckApplicationVersion()
        {
            SetProgressData(SConsts.StartPageProgressType.Update);
            shellVM.CurrentVersion = App.Version;

            var appVersionResponse = await otherController.GetLastVersion();

            if (appVersionResponse.IsValid)
            {
                LastAppVersion = appVersionResponse.Data.Version;

                if (IsCurrentVersionActual())
                {
                    GoToLoginPage();
                }
                else
                {
                    shellVM.NewVersion = appVersionResponse.Data.Version;
                    shellVM.IsNewVersion = true;
                    await Shell.Current.GoToAsync($"{nameof(VIEW.UpdateAppView)}");
                    wasUpdateCheck = true;
                }
            }
            else
            {
                GoToLoginPage();
            }
        }

        #endregion

        #region METHOD IsCurrentVersionActual

        private bool IsCurrentVersionActual() =>
           string.IsNullOrEmpty(LastAppVersion)
            || App.Version == LastAppVersion;


        #endregion
    }
}
