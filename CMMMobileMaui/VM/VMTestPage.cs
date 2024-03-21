using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Responses.Identity;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMMMobileMaui.VM
{
    public class VMTestPage : ScannerViewModelBase
    {
        #region FIEDLS

        private COMPANY.Company company;
        private VMMainShell shellVM;
        private IAPIManage apiManage;
        private IUserController userController;
        private IOtherController otherController;
        private IIdentityController identityController;
        private GetAppSettResponse vApp;
        private LoginResponse person;
        private readonly WeakEventManager personSetWeakManager = new WeakEventManager();

        public event EventHandler OnPersonSet
        {
            add => personSetWeakManager.AddEventHandler(value);
            remove => personSetWeakManager.RemoveEventHandler(value);
        }

        #endregion

        #region PROPERTY WebAPI

        private string _webAPI;

        public string WebAPI
        {
            get => _webAPI;
            set
            {
                _webAPI = value;
                ((Command)CheckConnectionCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY Login

        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                ((Command)CheckConnectionCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY Pass

        private string _pass;

        public string Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                ((Command)CheckConnectionCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY IsOkAPI

        private bool _isOkAPI;

        public bool IsOkAPI
        {
            get => _isOkAPI;
            set
            {
                _isOkAPI = value;
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY IsOkLogin

        private bool _isOkLogin;

        public bool IsOkLogin
        {
            get => _isOkLogin;
            set
            {
                _isOkLogin = value;
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY IsLogin

        private bool isLogin;

        public bool IsLogin
        {
            get => isLogin;
            set => SetProperty(ref isLogin, value);
        }

        #endregion

        #region PROPERTY Login

        public string CMMLogin
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CMMPass

        private string _CMMpass;

        public string CMMPass
        {
            get => _CMMpass;
            set
            {
                SetProperty(ref _CMMpass, value);
                ((Command)CheckLoginCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY ID

        private int _id;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        #endregion

        #region PROPERTY CurrentPerson

        private GetAllUsersResponse? _currentPerson;

        public GetAllUsersResponse? CurrentPerson
        {
            get => _currentPerson;
            set
            {
                _currentPerson = value;
                ((Command)CheckLoginCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY PersonList

        private ObservableCollection<GetAllUsersResponse>? personList;

        public ObservableCollection<GetAllUsersResponse>? PersonList
        {
            get => personList;
            set => SetProperty(ref personList, value);
        }

        #endregion

        #region PROPERTY IsAPINeed

        private bool isAPINeed;

        public bool IsAPINeed
        {
            get => isAPINeed;
            set => SetProperty(ref isAPINeed, value);
        }

        #endregion

        #region PROPERTY IsLoginNeed

        private bool isLoginNeed;

        public bool IsLoginNeed
        {
            get => isLoginNeed;
            set => SetProperty(ref isLoginNeed, value);
        }

        #endregion

        #region COMMAND CheckConnectionCommand

        public ICommand CheckConnectionCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCommand

        public ICommand SaveCommand
        {
            get;
        }

        #endregion

        #region COMMAND CheckLoginCommand

        public ICommand CheckLoginCommand
        {
            get;
        }

        #endregion

        #region COMMAND ScanUserCodeCommand

        public ICommand ScanUserCodeCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (CanClick())
                    {
                        await OpenModalPage(new VIEW.ScanView(ScanType.PersonSett));
                    }
                });
            }
        }

        #endregion

        #region COMMAND 

        public ICommand LabelClickedCommand
        {
            get;
        }

        #endregion

        #region Cstr
        public VMTestPage(VMMainShell shellVM
            , IAPIManage apiManage
            , IUserController userController
            , IOtherController otherController
            , IIdentityController identityController)
        {
            this.shellVM = shellVM;
            this.apiManage = apiManage;
            this.userController = userController;
            this.otherController = otherController;
            this.identityController = identityController;

            SConsts.SetGlobalAction(SConsts.SUB_SCAN_PERSON_SETT, async () =>
            {
                await Shell.Current.Navigation.PopModalAsync();
                SetContentAfterLogin();
            });


            LabelClickedCommand = new Command(() =>
            {
                if (CanClick())
                {
                    IsLogin = !IsLogin;
                }
            });

            SaveCommand = new Command(() => Save(),
                () =>
                {
                    return IsOkAPI && IsOkLogin;
                });

            CheckLoginCommand = new Command(async () => await CheckLogin(), () =>
            {
                if (CurrentPerson == null || string.IsNullOrEmpty(CMMPass))
                {
                    return false;
                }

                return true;
            });

            CheckConnectionCommand = new Command(async () => await CheckConnection(),
            () =>
            {
                if (string.IsNullOrEmpty(WebAPI) || string.IsNullOrEmpty(Login)
                || string.IsNullOrEmpty(Pass))
                {
                    return false;
                }

                return true;
            });
        }

        #endregion

        #region METHOD SetContentAfterLogin

        private async void SetContentAfterLogin()
        {
            shellVM.MainParent.SetUIForCurrentUserRights();

            var observedDevicesResult = await userController.GetObservedDevices(new API.Contracts.v1.Requests.User.GetUserObservedDeviceRequest
            {
                PersonID = MainObjects.Instance.CurrentUser!.PersonID
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

        #region METHOD InitStartData

        public void InitStartData(bool isAPIneed, bool isLoginNeed)
        {
            IsLoginNeed = isLoginNeed;
            IsAPINeed = isAPIneed;

            IsLogin = Settings.CMMIsLogin;

            if (IsAPINeed || IsLogin)
            {
                CMMLogin = Settings.CMMLogin;
                CMMPass = Settings.CMMPass;
                ID = Settings.CMMId;
            }

            WebAPI = Settings.WebAPI;
            Login = Settings.Login;
            Pass = Settings.Pass;
            IsOkAPI = !isAPIneed;

            if (!string.IsNullOrEmpty(WebAPI)
                && !string.IsNullOrEmpty(Login)
                && !string.IsNullOrEmpty(Pass)
                && IsLoginNeed)
            {
                SetMainPersonList();
            }
        }

        #endregion

        #region METHOD SetMainPersonList

        private void SetMainPersonList()
        {
            if (IsAPINeed || IsLogin)
            {
                if (ID != -1
                    && PersonList != null)
                {
                    CurrentPerson = PersonList?.FirstOrDefault(tt => tt.PersonID == ID);
                    personSetWeakManager.HandleEvent(this, EventArgs.Empty, nameof(OnPersonSet));
                }
            }
        }

        #endregion

        #region METHOD CheckLogin

        private async Task<bool> CheckLogin()
        {
            IsBusy = true;

            if (CurrentPerson != null)
            {
                if (!string.IsNullOrEmpty(CMMPass))
                {
                    if (CanClick())
                    {
                        IsOkLogin = false;

                        var serialNumber = DependencyService.Get<ISerialNumberService>()
                    .GetSerialNumber();

                        var personResponse = await identityController.LoginPass(new API.Contracts.v1.Requests.Identity.LoginPassRequest
                        {
                            PersonID = CurrentPerson.PersonID
                            ,
                            Password = CMMPass
                            ,
                            UserName = CurrentPerson.Name
                            ,
                            Mobile_Info = serialNumber
                        });

                        if (personResponse.IsResponseWithData())
                        {
                            person = personResponse.Data!;

                            IsOkLogin = true;

                            if (IsAPINeed)
                            {
                                await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");
                            }
                            else
                            {
                                if (IsLogin)
                                {
                                    Settings.CMMId = person.PersonID;
                                    Settings.CMMIsLogin = IsLogin;
                                    Settings.CMMLogin = person.Name;
                                    Settings.CMMPass = CMMPass;
                                }
                                else
                                {
                                    Settings.CMMId = 0;
                                    Settings.CMMIsLogin = IsLogin;
                                    Settings.CMMLogin = string.Empty;
                                    Settings.CMMPass = string.Empty;
                                }

                                MainObjects.Instance.CurrentUser = person;
                                MainObjects.Instance.Lang = person.LangCode;

                                await CONV.TranslateExtension.InitLanguage(MainObjects.Instance.Lang, true);

                                shellVM.UserName = person.Name;

                                SetContentAfterLogin();

                                IsBusy = false;

                                return true;
                            }

                            return IsOkLogin;
                        }
                        else
                        {
                            await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Error, CONV.TranslateExtension.GetResourceText("login_err_pass"));
                        }
                    }
                }
            }

            IsBusy = false;

            return false;
        }

        #endregion

        #region METHOD CheckConnection

        private async Task CheckConnection()
        {
            if (CanClick())
            {
                IsBusy = true;

                apiManage.Host = WebAPI;
                apiManage.Login = Login;
                apiManage.Pass = Pass;

                var isHostResponse = await otherController.IsHost();

                if (isHostResponse.IsValid
                    && isHostResponse.Data)
                {
                    IsOkAPI = true;
                    await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");

                    //var appResponse = await otherController.GetAppSett();

                    //if (appResponse.IsValid)
                    //{
                    //    vApp = appResponse.Data;

                    //    company = COMPANY.CompanyFactory.GetAppCompany(vApp);
                    //    SetMainPersonList();
                    //}
                    //else
                    //{
                    //    IsOkAPI = false;
                    //}
                }
                else
                {
                    IsOkAPI = false;
                }

                IsBusy = false;
            }
        }

        #endregion

        #region METHOD Save

        private async void Save()
        {
            if (CanClick())
            {
                bool wasAPIChanged = false;

                if (!string.IsNullOrEmpty(WebAPI) &&
                    !string.IsNullOrEmpty(Login)
                    && !string.IsNullOrEmpty(Pass))
                {
                    if (IsOkAPI)
                    {
                        if (Settings.WebAPI != WebAPI)
                        {
                            DBMain.Engine dn = new DBMain.Engine();
                            await dn.DeleteHistoryAll();
                            await dn.DeletePartInvAll();
                            dn.CloseConnection();
                        }

                        Settings.WebAPI = WebAPI;
                        Settings.Login = Login;
                        Settings.Pass = Pass;

                        apiManage.Login = Login;
                        apiManage.Host = WebAPI;
                        apiManage.Pass = Pass;
                    }
                }

                if (IsOkLogin)
                {
                    if (person != null && !string.IsNullOrEmpty(CMMPass))
                    {
                        if (IsLogin)
                        {
                            Settings.CMMId = person.PersonID;
                            Settings.CMMIsLogin = IsLogin;
                            Settings.CMMLogin = person.Name;
                            Settings.CMMPass = CMMPass;
                        }
                        else
                        {
                            Settings.CMMId = 0;
                            Settings.CMMIsLogin = IsLogin;
                            Settings.CMMLogin = string.Empty;
                            Settings.CMMPass = string.Empty;
                        }

                        MainObjects.Instance.CurrentUser = person;

                        shellVM.MainParent.SetUIForCurrentUserRights();
                    }
                }

                if (IsOkLogin && IsOkAPI)
                {
                    App.isHost = true;

                    if (vApp != null)
                    {
                        MainObjects.Instance.AppSettings = vApp;
                        App.CompanyData = company;
                    }

                    if (Shell.Current.Navigation.ModalStack.Count == 0)
                    {
                        if (IsAPINeed)
                        {
                            await Shell.Current.GoToAsync($"//{nameof(VIEW.START.StartPage)}");
                        }
                        else
                        {
                            await Shell.Current.GoToAsync("//device");
                        }
                    }
                    else
                    {
                        if (WasAPIDataChanged())
                        {
                            await Shell.Current.Navigation.PopModalAsync();
                            await Shell.Current.GoToAsync($"//{nameof(VIEW.START.StartPage)}");
                        }
                        else
                        {
                            await Shell.Current.Navigation.PopModalAsync();
                        }
                    }

                    Shell.Current.FlyoutIsPresented = false;
                }
                else
                {
                    if (IsOkAPI && !IsLoginNeed)
                    {
                        await Shell.Current.GoToAsync($"../..");
                        // await Shell.Current.GoToAsync($"//{nameof(VIEW.START.StartPage)}?back=true");
                    }
                }
            }
        }

        #endregion

        #region METHOD WasAPIDataChanged

        private bool WasAPIDataChanged() =>
            !Settings.WebAPI.Equals(WebAPI)
            || !Settings.Login.Equals(Login)
            || !Settings.Pass.Equals(Pass);

        #endregion

        #region METHOD GetScanItems
        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            var userScan = new UserScan();

            userScan.UIMethod = async (obj) =>
            {
                if (!IsAPINeed && IsOkAPI)
                {
                    if (MainObjects.Instance.CurrentUser != null)
                    {
                        MainObjects.Instance.Lang = MainObjects.Instance.CurrentUser.LangCode;

                        await CONV.TranslateExtension.InitLanguage(MainObjects.Instance.Lang, true);

                        shellVM.UserName = MainObjects.Instance.CurrentUser.Name;

                        SetContentAfterLogin();
                    }
                }
                else
                {
                    if (IsOkAPI)
                    {
                        IsOkLogin = true;
                        person = MainObjects.Instance.CurrentUser!;
                        Save();
                    }
                }
            };

            if (IsLoginNeed)
            {
                scanTypes.Add(userScan);
            }

            return scanTypes;
        }

        public override string GetVisualScanPresentation() => "person";
        #endregion

    }
}
