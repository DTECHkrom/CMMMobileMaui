using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartGiverView : ContentPage
    {
        #region FIELDS

        private VM.VMPartGiver _vmMain;
        private bool wasFirst = false;

        #endregion

        #region Cstr
        public PartGiverView(GetPartDetailResponse part)
        {
            InitializeComponent();

            var personBLL = (IUserController)API.MainObjects.Instance.ServiceProvider.GetService(typeof(IUserController));
            var partBLL = (IPartController)API.MainObjects.Instance.ServiceProvider.GetService(typeof(IPartController));

            _vmMain = new VM.VMPartGiver(partBLL, personBLL);
            _vmMain.CurrentPart = part;
            _vmMain.SetMainData();
            this.BindingContext = _vmMain;
        }

        #endregion

        #region METHOD OnAppearing

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (!wasFirst)
            //{
            //    COMMON.SConsts.RunMainFrameAnimationOnPage(this,
            //        new List<Frame>() { framePartGive }
            //        , null
            //        , new List<VisualElement>() { stackPartGive }
            //        , 1000);

            //    wasFirst = true;
            //}
        }

        #endregion

        //private void CustomAutoCompleteView_SuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
        //{
        //    if (e.DataItem != null)
        //    {
        //        _vmMain.CurrentPerson = e.DataItem as APIRES.OBJECTS.vPerson;
        //    }
        //}

        //private void CustomAutoCompleteView_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(e.NewTextValue))
        //    {
        //        _vmMain.CurrentPerson = null;
        //    }
        //}
    }
}