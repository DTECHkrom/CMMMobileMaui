using System.Globalization;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VM
{
    public class VMLogin : ScannerViewModelBase
    {
        #region FIELDS

        private readonly IIdentityController identityController;
        private readonly IUserController userController;


        #endregion

        #region PROPERTY IsLogin

        public bool IsLogin
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Login

        public string Login
        {
            get;
            set;
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

                if (_id > 0)
                {
                    IsOk = true;
                }
            }
        }

        #endregion

        #region PROPERTY CurrentPerson

        private GetAllUsersResponse _currentPerson;

        public GetAllUsersResponse CurrentPerson
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

        public List<GetAllUsersResponse> PersonList =>
            DictionaryResources.Instance.PersonList;

        #endregion

        #region PROPERTY IsOk

        private bool _isOk;

        public bool IsOk
        {
            get => _isOk;
            set
            {
                _isOk = value;
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY IsFromStart

        public bool IsFromStart
        {
            get;
            set;
        }

        #endregion

        #region COMMAND CheckLoginCommand

        public ICommand CheckLoginCommand
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

        #region COMMAND CloseCommand

        public ICommand CloseCommand
        {
            get;
        }

        #endregion

        #region COMMAND ClearHistoryCommand

        public ICommand ClearHistoryCommand
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
                        await OpenModalPage(new VIEW.ScanView(ScanType.Person));
                    }
                });
            }
        }

        #endregion

        #region Cstr

        public VMLogin(IUserController userController, IIdentityController identityController)
        {
            this.identityController = identityController;
            this.userController = userController;

            SConsts.SetGlobalAction(SConsts.SUB_SCAN_PERSON_SAVE, async () =>
            {
                await Shell.Current.Navigation.PopModalAsync();
            });

            SaveCommand = new Command(async () => await Save(), () =>
            {
                if (!IsOk)
                {
                    return false;
                }

                return true;
            });

            CloseCommand = new Command(() => Close());

            CheckLoginCommand = new Command(async () => await CheckLogin(), () =>
            {
                if (CurrentPerson == null || string.IsNullOrEmpty(Pass))
                {
                    return false;
                }

                return true;
            });

            IsFromStart = true;
            IsLogin = Settings.CMMIsLogin;
            Login = Settings.CMMLogin;
            Pass = Settings.CMMPass;
            ID = Settings.CMMId;

            ClearHistoryCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    var isOk = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure_delete"));

                    if (isOk)
                    {

                        DBMain.Engine en = new DBMain.Engine();

                        try
                        {
                            await en.DeleteHistory(MainObjects.Instance.CurrentUser!.PersonID);
                            await en.DeletePartInv(MainObjects.Instance.CurrentUser!.PersonID);
                        }
                        catch (Exception) { }

                        en.CloseConnection();
                    }
                }
            });

            SetMainPersonList();
        }

        #endregion

        #region METHOD SetMainPersonList

        private void SetMainPersonList()
        {
            if (ID != -1)
            {
                if (PersonList != null)
                {
                    CurrentPerson = PersonList!.FirstOrDefault(tt => tt.PersonID == ID);
                }
            }
        }

        #endregion

        #region METHOD Save

        private async Task Save()
        {
            if (CanClick())
            {
                bool isOk = await CheckLogin();

                if (isOk)
                {
                    //var userResponse = await personBLL.GetUser(new CMMMobileMaui.API.Contracts.v1.Requests.User.GetByPersonIDRequest
                    //{
                    //    PersonID = CurrentPerson.PersonID
                    //});

                    //if (userResponse.IsValid)
                    //{
                    //   // CMMMobileMaui.API.MainObjects.Instance.CurrentUser = userResponse.Data;

                    //    COMMON.Settings.CMMId = CurrentPerson.PersonID;
                    //    COMMON.Settings.CMMIsLogin = IsLogin;
                    //    COMMON.Settings.CMMLogin = CurrentPerson.Name;
                    //    COMMON.Settings.CMMPass = Pass;

                    //    MessagingCenter.Send(this, "LoginClose");
                    //}
                }
            }
        }

        #endregion

        #region METHOD Close

        private void Close()
        {
            MessagingCenter.Send(this, "LoginClose");
        }

        #endregion

        #region METHOD CheckLogin

        private async Task<bool> CheckLogin()
        {
            if (CurrentPerson != null)
            {
                if (!string.IsNullOrEmpty(Pass))
                {
                    if (CanClick())
                    {
                        var serialNumber = DependencyService.Get<ISerialNumberService>()
                    .GetSerialNumber();

                        var personResponse = await identityController.LoginPass(new API.Contracts.v1.Requests.Identity.LoginPassRequest
                        {
                            Password = Pass
                             ,
                            PersonID = CurrentPerson.PersonID
                             ,
                            UserName = CurrentPerson.Name
                            ,
                            Mobile_Info = serialNumber
                        });

                        if (personResponse.IsValid)
                        {
                            IsOk = true;

                            if (IsFromStart)
                            {
                                var observedDeviceResponse = await userController.GetObservedDevices(new API.Contracts.v1.Requests.User.GetUserObservedDeviceRequest
                                {
                                    PersonID = CurrentPerson.PersonID
                                });

                                if (observedDeviceResponse.IsValid)
                                {
                                    MainObjects.Instance.ObservedDevices = observedDeviceResponse.Data;
                                }

                                MainObjects.Instance.CurrentUser = personResponse.Data;
                                MainObjects.Instance.Lang = personResponse.Data!.LangCode;

                                if (IsLogin)
                                {
                                    Settings.CMMId = CurrentPerson.PersonID;
                                    Settings.CMMIsLogin = IsLogin;
                                    Settings.CMMLogin = CurrentPerson.Name;
                                    Settings.CMMPass = Pass;
                                }
                                else
                                {
                                    Settings.CMMId = 0;
                                    Settings.CMMIsLogin = IsLogin;
                                    Settings.CMMLogin = string.Empty;
                                    Settings.CMMPass = string.Empty;
                                }
                            }
                            else
                            {
                                await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");
                            }

                            return IsOk;
                        }
                        else
                        {
                            await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Error, "Błędne hasło!");
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            var userSaveScan = new UserSaveScan();

            scanTypes.Add(userSaveScan);

            return scanTypes;
        }

        #endregion
    }
}
