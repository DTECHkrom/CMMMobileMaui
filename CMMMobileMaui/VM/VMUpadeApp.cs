using CMMMobileMaui.API.Interfaces;
using System.Windows.Input;

namespace CMMMobileMaui.VM
{
    public class VMUpadeApp : COMMON.ViewModelBase
    {
        #region FIELDS

        private COMMON.IDownloadVersion? _downLoadVersion;
        private readonly IAPIManage _aPIManage;
        private readonly VMStartPage _vMParent;

        #endregion

        #region PROPERTY MainParent

        public VIEW.UpdateAppView MainParent
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentVersion

        private string? currentVersion;

        public string? CurrentVersion
        {
            get => currentVersion;
            set => SetProperty(ref currentVersion, value);
        }

        #endregion

        #region PROPERTY ProgressValue

        private float progressValue;

        public float ProgressValue
        {
            get => progressValue;
            set => SetProperty(ref progressValue, value);
        }

        #endregion

        #region PROPERTY NewVersion

        private string? newVersion;

        public string? NewVersion
        {
            get => newVersion;
            set => SetProperty(ref newVersion, value);
        }

        #endregion

        #region COMMAND DownloadAppCommnad

        public ICommand DownloadAppCommnad
        {
            get
            {
                return new Command((obj) =>
                {
                    if (CanClick())
                    {
                        DownloadWebClient();
                    }
                });
            }
        }

        #endregion

        #region COMMAND CancelCommand

        public ICommand CancelCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    if (CanClick())
                    {
                        CancelMethod();
                    }
                });
            }
        }

        #endregion

        #region Cstr

        public VMUpadeApp(IAPIManage aPIManage
            , VMStartPage vMParent)
        {
            _aPIManage = aPIManage;
            _vMParent = vMParent;
        }

        #endregion

        #region METHOD CancelMethod

        private void CancelMethod()
        {
            if (_vMParent != null)
            {

                _vMParent.GoToLoginPage();
            }
        }

        #endregion

        #region METHOD DownloadWebClient

        private void DownloadWebClient()
        {
            if (string.IsNullOrEmpty(NewVersion))
                return;

            _downLoadVersion = DependencyService.Get<COMMON.IDownloadVersion>();

            if(_downLoadVersion == null)           
                return;
            
            IsBusy = true;
            _downLoadVersion.OnStopInstallation += _downLoadVersion_OnStopInstallation;
            _downLoadVersion.DownloadApp(NewVersion, MainParent, _aPIManage.IsSSL);
        }

        private void _downLoadVersion_OnStopInstallation(object? sender, bool e)
        {
            IsBusy = false;
        }

        #endregion
    }
}
