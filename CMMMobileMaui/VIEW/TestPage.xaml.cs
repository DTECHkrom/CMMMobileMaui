using Telerik.Maui.Controls.AutoComplete;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Interfaces;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : PageBase<VM.VMTestPage>
    {
        #region FIELDS

        private readonly bool isStart = false;
        private double width = 0;
        private double height = 0;
        private readonly bool isPanelVisible = false;

        #endregion

        #region Cstr

        public TestPage() : this(true, false, true)
        {
        }

        public TestPage(bool isHostNeed, bool isLoginNeed, bool isStart = false)
        {
            InitializeComponent();

            this.isStart = isStart;

            ViewModel.InitStartData(isHostNeed, isLoginNeed);

            ViewModel.IsOkLogin = !isStart;

            isPanelVisible = !isHostNeed;

            if (!App.IsZebraScanIconVisible)
            {
                var parent = zebraIcon.Parent as Grid;

                if (parent != null)
                {
                    parent.Children.Remove(zebraIcon);
                }
            }
            else
            {
                if (isLoginNeed)
                {
                    zebraIcon.IsVisible = true;
                    ViewModel.InitScannerOnPage(zebraIcon);
                }
                else
                {
                    zebraIcon.IsVisible = false;
                }
            }

            ViewModel.PersonList = new ObservableCollection<GetAllUsersResponse>(DictionaryResources.Instance.PersonList);
        }

        #endregion

        #region EVENT

        protected override void OnAppearing()
        {
            if (isStart)
            {
                ViewModel.CMMPass = string.Empty;
            }

            ViewModel.CurrentPerson = null;

            base.OnAppearing();
        }

        #endregion

        #region EVENT OnSizeAllocated

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;

                if (width > height)
                {
                    stackLogin.IsVisible = false;
                }
                else
                {
                    stackLogin.IsVisible = isPanelVisible;
                }
            }
        }

        #endregion

        #region EVENT OnBackButtonPressed

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel != null)
            {
                var apiManage = (IAPIManage)MainObjects.Instance.ServiceProvider.GetService(typeof(IAPIManage));

                apiManage.Login = COMMON.Settings.Login;
                apiManage.Host = COMMON.Settings.WebAPI;
                apiManage.Pass = COMMON.Settings.Pass;

                if (!isStart)
                {
                    if (ViewModel.IsLoginNeed
                        && !ViewModel.IsOkLogin)
                    {
                        return true;
                    }

                    if (ViewModel.IsAPINeed
                        && !ViewModel.IsOkAPI)
                    {
                        return true;
                    }
                }
            }

            return base.OnBackButtonPressed();
        }

        #endregion

        #region EVENT cboxLogin_SuggestionItemSelected

        private void cboxLogin_SuggestionItemSelected(object sender, SuggestionItemSelectedEventArgs e)
        {
            if (e.DataItem != null)
            {
                ViewModel.CurrentPerson = e.DataItem as GetAllUsersResponse;
            }
        }

        #endregion

        #region EVENT cboxLogin_TextChanged

        private void cboxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ViewModel.CurrentPerson = null;
            }
        }

        #endregion
    }
}