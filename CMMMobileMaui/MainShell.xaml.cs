using CMMMobileMaui.API;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.VIEW;

namespace CMMMobileMaui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : ShellBase<VM.VMMainShell>
    {
        #region FIELDS

        private bool wasDataSet = false;

        #endregion

        #region Cstr

        public MainShell()
        {
            InitializeComponent();

            ViewModel.MainParent = this;

            COMMON.SConsts.SetGlobalAction(COMMON.SConsts.SHELL_LOGIN, () =>
            {
                SetUIForCurrentUserRights();
            });

        }

        #endregion

        #region METHOD SetUIForCurrentUserRights

        public async void SetUIForCurrentUserRights()
        {
            if (MainObjects.Instance.CurrentUser != null)
            {
                await DictionaryResources.Instance.InitDataAfterLogin();

                ViewModel.UserName = MainObjects.Instance.CurrentUser.Name;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    fiDevice.Items[3].IsVisible = MainObjects.Instance.CurrentUser.GetUserRightResponse.MD_Add;
                    fiPart.Items[3].IsVisible = MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Add;
                    fiPart.Items[2].IsVisible = MainObjects.Instance.CurrentUser.GetUserRightResponse.PART_Add;
                });
            }
        }

        #endregion

        #region METHOD SetShellTextAfterLanguageChanged

        public void SetShellTextAfterLanguageChanged()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                fiDevice.Title = CONV.TranslateExtension.GetResourceText("sh_devices");
                tabWO.Title = CONV.TranslateExtension.GetResourceText("sh_wo");
                tabDevHist.Title = CONV.TranslateExtension.GetResourceText("history");
                tabDevSearch.Title = CONV.TranslateExtension.GetResourceText("search");
           //     tabDevScan.Title = CONV.TranslateExtension.GetResourceText("scan_code");
                tabDevAdd.Title = CONV.TranslateExtension.GetResourceText("add");

                fiSet.Title = CONV.TranslateExtension.GetResourceText("sh_lines");

                fiPart.Title = CONV.TranslateExtension.GetResourceText("wo_parts");
                tabPartSearch.Title = CONV.TranslateExtension.GetResourceText("search");
             //   tabPartScan.Title = CONV.TranslateExtension.GetResourceText("scan_code");
                tabPartGive.Title = CONV.TranslateExtension.GetResourceText("sh_part_give");
                tabPartStock.Title = CONV.TranslateExtension.GetResourceText("sh_stocktaking");
                tabPartAdd.Title = CONV.TranslateExtension.GetResourceText("add");

                fiUser.Title = CONV.TranslateExtension.GetResourceText("sh_user");
                tabUserCal.Title = CONV.TranslateExtension.GetResourceText("sh_user_sch");
                tabUserList.Title = CONV.TranslateExtension.GetResourceText("sh_user_wo");
                tabUserLog.Title = CONV.TranslateExtension.GetResourceText("sh_user_login");

                //   miSett.Text = CONV.TranslateExtension.GetResourceText("sh_api");


                //if(mouldCal != null)
                //{
                //    mouldCal.Title = CONV.TranslateExtension.GetResourceText("sh_mc");
                //}

                //if(mouldLoc != null)
                //{
                //    mouldLoc.Title = CONV.TranslateExtension.GetResourceText("sh_ml");
                //}

                if (App.CompanyData != null)
                {
                    App.CompanyData.SetTitleForExtraContent();
                }
            });
        }

        #endregion

        #region METHOD SetCompanyData

        //private ShellContent mouldLoc = null;
        //private ShellContent mouldCal = null;

        public async void SetCompanyData()
        {

            if (!wasDataSet)
            {
                var otherController = (IOtherController)MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IOtherController));
                var isSetResponse = await otherController.GetIsSetUsed();

                if (!isSetResponse.IsValid
                    || !isSetResponse.Data)
                {
                    mainShell.Items.Remove(fiSet);
                }

                App.CompanyData.InitExtraContent();

                //if (App.CompanyData.IsMouldChangeLocation)
                //{
                //    if (mouldLoc == null)
                //    {
                //        var t = new DeviceLocationManagePage();  //Assembly.GetExecutingAssembly().CreateInstance($"CMMMobileMaui.{CMMMobileMaui.API.MainObjects.AppSettings.Company_data}.MouldChangeLocationView");

                //        if (t != null)
                //        {
                //            mouldLoc = new ShellContent()
                //            {
                //                Title = CONV.TranslateExtension.GetResourceText("sh_ml"),
                //                Icon = "location"
                //                ,
                //                ContentTemplate = new DataTemplate(t.GetType())
                //            };

                //            MainThread.BeginInvokeOnMainThread(() =>
                //            {
                //                mainShell.Items.Add(mouldLoc);
                //            });
                //        }
                //    }

                //    if (mouldCal == null)
                //    {
                //        var cal = Assembly.GetExecutingAssembly().CreateInstance($"CMMMobileMaui.{CMMMobileMaui.API.MainObjects.AppSettings.CompanyData}.MouldCalendarChange");

                //        if (cal != null)
                //        {
                //            mouldCal = new ShellContent()
                //            {
                //                Title = CONV.TranslateExtension.GetResourceText("sh_mc"),
                //                Icon = "calendar"
                //                ,
                //                ContentTemplate = new DataTemplate(cal.GetType())
                //            };

                //            MainThread.BeginInvokeOnMainThread(() =>
                //            {
                //                mainShell.Items.Add(mouldCal);
                //            });
                //        }
                //    }


                // }

                wasDataSet = true;

            }

            SetShellTextAfterLanguageChanged();
        }

        #endregion

        #region EVENT OnBackButtonPressed

        private bool isDouble = false;
        private Timer _timer = null;

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            if (isDouble)
            {
                isDouble = false;
                var closer = DependencyService.Get<COMMON.ICloseApplication>();
                closer?.CloseApplication();

                return false;
            }

            isDouble = true;

            _timer = new Timer(new TimerCallback((obj) =>
            {
                isDouble = false;
            }), null, 400, 0);

            return true;
        }

        #endregion
    }
}