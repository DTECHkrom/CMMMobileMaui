using CMMMobileMaui.SCAN;
using CMMMobileMaui.VIEW;
using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Pages;
using Mopups.Services;

namespace CMMMobileMaui.COMMON
{
    public class ViewModelBase : ObservableObject, IAlertPopup
    {
        #region FIELDS

        private static readonly List<(Guid id, Page page, CUST.SKIAImageTextButton? button)> openedModalPageList = new List<(Guid, Page, CUST.SKIAImageTextButton?)>();
        private static readonly List<(Guid id, Page page, CUST.SKIAImageTextButton? button)> openedModalPopUpList = new List<(Guid, Page, CUST.SKIAImageTextButton?)>();
        private Timer? clickTimer = null;

        private readonly WeakEventManager weakEventManager = new WeakEventManager();

        public event EventHandler<bool> OnModalDisapear
        {
            remove => weakEventManager.RemoveEventHandler(value);
            add => weakEventManager.AddEventHandler(value);
        }

        #endregion

        #region STATIC PROPERTY ClickedButton

        public static CUST.SKIAImageTextButton? ClickedButton
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsUpdateBusy

        private bool isUpdateBusy;
        public bool IsUpdateBusy
        {
            get => isUpdateBusy;
            set
            {
                SetProperty(ref isUpdateBusy, value);
            }
        }

        #endregion

        #region PROPERTY IsBusy

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                SetProperty(ref isBusy, value);
            }
        }

        #endregion

        #region PROPERTY IsNotBusy

        public bool IsNotBusy
        {
            get => !IsBusy;
        }

        #endregion

        #region PROPERTY CanMoveBack

        public bool CanMoveBack
        {
            get;
            set;
        } = true;

        #endregion

        #region PROPERTY WasClicked

        private bool wasClicked;

        private bool WasClicked
        {
            get => wasClicked;
            set
            {
                wasClicked = value;

                if (wasClicked)
                {
                    WaitForNextClick();
                }
            }
        }

        #endregion

        //#region METHOD InitScanManager

        //public void InitScanManager(IScanIcon icons, SCAN.IScanItems scanItems)
        //{
        //    if (App.IsZebraScanIconVisible)
        //    {
        //        App.CurrentScanManager?.Init(icons, scanItems);

        ////        ScanManager = new SCAN.ScanManager(scaner, scanItems);
        //    }
        //}

        //#endregion

        //#region METHOD InitScanIcon

        //public void InitScanIcon(SCAN.IScanIcon scanIcon)
        //{
        //    ScanManager?.InitScanIcon(scanIcon);
        //}

        //#endregion

        #region METHOD WaitForNextClick

        private void WaitForNextClick()
        {
            if (clickTimer == null)
            {
                clickTimer = new Timer(new TimerCallback((obj) =>
                {
                    WasClicked = false;

                    if (clickTimer != null)
                    {
                        clickTimer.Dispose();
                        clickTimer = null;
                    }

                    if (ClickedButton != null
                    && openedModalPageList.FirstOrDefault(tt => tt.button == ClickedButton) == default)
                    {
                        ClickedButton.IsBackwardAnimRunning = true;
                        ClickedButton = null;
                    }

                }), null, 500, 500);
            }
        }

        #endregion

        #region METHOD CanClick

        public bool CanClick()
        {
            if (!WasClicked)
            {
                WasClicked = true;
                return true;
            }

            return false;
        }

        #endregion

        #region METHOD BaseStepActionMethod

        public async Task<T> BaseStepActionMethod<T>(Action<T> action, object obj)
        {
            CanMoveBack = false;

            if (CanClick())
            {
                if (obj != null && obj is T job)
                {
                    T t = job;

                    bool isOk = await ConfirmDialogMessage();

                    if (isOk)
                    {
                        action.Invoke(job);
                    }

                    CanMoveBack = true;

                    return t;
                }
            }


            CanMoveBack = true;

            return default(T);
        }

        public async Task BaseStepActionMethod(Action action)
        {
            CanMoveBack = false;

            if (CanClick())
            {
                bool isOk = await ConfirmDialogMessage();

                if (isOk)
                {
                    action.Invoke();
                }
            }

            CanMoveBack = true;
        }

        public async Task<T> BaseStepActionMethod<T>(Action<T> action, object obj, Func<T, bool> isValid, Func<T, string> getQuest)
        {
            CanMoveBack = false;

            if (CanClick())
            {
                if (obj != null && obj is T job)
                {
                    if (isValid(job))
                    {
                        T t = job;

                        bool isOk = await ConfirmDialogMessage(getQuest(job));

                        if (isOk)
                        {
                            action.Invoke(job);
                        }
                        CanMoveBack = true;

                        return t;
                    }
                }
            }

            CanMoveBack = true;

            return default(T);
        }

        #endregion

        #region METHOD ConfirmDialogMessage

        public async Task<bool> ConfirmDialogMessage(string msg = "q_sure", string value = "")
        {
            string question = CONV.TranslateExtension.GetResourceText(msg);

            if (!string.IsNullOrEmpty(value))
            {
                question = value;
            }

            bool isOk = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, question);

            return isOk;
        }

        #endregion

        #region METHOD OpenModalPage

        public static async Task OpenModalPage(Page page)
        {
            Guid guid = Guid.NewGuid();

            page.Disappearing += (s, e) =>
            {
                var pageToRemove = openedModalPageList.LastOrDefault();

                if (pageToRemove.id == guid)
                {
                    if (pageToRemove.button != null)
                    {
                        pageToRemove.button.IsBackwardAnimRunning = true;
                    }

                    openedModalPageList.Remove(pageToRemove);
                }
            };

            //if (page is PageBase a)
            //{
            //    a.OnPageUnload += (s, e) =>
            //    {
            //        //if (scanItems is not null)
            //        //{
            //        //   // if (openedModalPopUpList.Count == 0)
            //            //{
            //               // App.CurrentScanManager?.Init(scanItems);
            //                //App.CurrentScanManager?.DisableScan();
            //          //  }
            //      //  }
            //    };
            //}

            openedModalPageList.Add((guid, page, ClickedButton));
            ClickedButton = null;

            await Shell.Current.Navigation.PushModalAsync(page);
        }

        #endregion

        #region METHOD OpenPopup

        public async Task OpenPopup(PopupPage popup, bool shouldDisableScan = false)
        {
            Guid guid = Guid.NewGuid();

            var zebra = App.CurrentScanManager;

            if (zebra != null && shouldDisableScan)
            {
                zebra.DisableScan();
            }

            CanMoveBack = false;

            popup.Disappearing += (s, e) =>
            {
                CanMoveBack = true;

                var pageToRemove = openedModalPopUpList.LastOrDefault(tt => tt.id == guid);

                if (pageToRemove != default)
                {
                    if (pageToRemove.button != null)
                    {
                        pageToRemove.button.IsBackwardAnimRunning = true;
                    }

                    openedModalPopUpList.Remove(pageToRemove);

                    if (openedModalPageList.Count > 0)
                    {
                        if (openedModalPageList.LastOrDefault().page.GetType() != typeof(PopupPage))
                        {
                            var pageData = openedModalPageList.LastOrDefault();

                            if (pageData.page is PageBase page)
                            {
                                // App.CurrentScanManager = ((ViewModelBase)page.BindingContext).ScanManager;
                               // App.CurrentScanManager?.EnableScan();
                            }
                        }
                    }
                }
            };

            openedModalPopUpList.Add((guid, popup, ClickedButton));
            ClickedButton = null;

            await MopupService.Instance.PushAsync(popup);
        }

        #endregion

        #region METHOD OpenAlertMessage

        public async void OpenAlertMessage(string msg)
        {
            await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Error, msg);
        }

        #endregion
    }
}
