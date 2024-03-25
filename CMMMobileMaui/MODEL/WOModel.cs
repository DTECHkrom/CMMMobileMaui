using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.UIRefresh;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CMMMobileMaui.MODEL
{
    public class WOModel : ObservableObject, IUIRefresh
    {
        #region FIELDS

        private readonly WOIconsSetter woIconsSetter;
        private readonly WOFunctionsSetter woFunctionsSetter;

        #endregion

        #region PROPERTY BaseItem
        public GetWOsResponse BaseItem
        {
            get;
            private set;
        }

        #endregion

        #region PROPERTY WOItems

        public List<DisplayImage> WOItems =>
            woIconsSetter.GetIcons();

        #endregion

        #region PROPERTY WOFunctionts

        public List<FunctionItem> WOFunctions =>
            woFunctionsSetter.GetFunctions();

        #endregion

        #region METHOD

        public Color StateColor =>
            Color.FromArgb(BaseItem.State_Color);


        #endregion

        #region PROPETY IsBusy

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        #endregion

        #region PROPERTY IsTaken

        public bool IsTaken =>
           BaseItem.Person_Take_Date.HasValue;

        #endregion

        #region PROPERTY TakenTimeText

        //public string TakenTimeText =>
        //    !IsTaken ? string.Empty :
        //    SConsts.GetTimeTextFromMinutes(GetMinutesFromTaken());

        private string? takenTimeText;

        public string? TakenTimeText
        {
            get => takenTimeText;
            set => SetProperty(ref takenTimeText, value);
        }


        #endregion

        #region Cstr

        public WOModel(GetWOsResponse baseItem, VM.VMWorkOrderListAll vmWO)
        {
            BaseItem = baseItem;
            woIconsSetter = new WOIconsSetter(baseItem);
            woFunctionsSetter = new WOFunctionsSetter(baseItem, vmWO);

            InitTakenTime();
        }

        #endregion

        #region METHOD GetMinutesFromTaken

        public long GetMinutesFromTaken() =>
            (long)(DateTime.Now - BaseItem.Person_Take_Date.Value).TotalMinutes;

        #endregion

        #region METHOD GetCalculatedWorkLoad

        public decimal GetCalculatedWorkLoad() =>
            !BaseItem.Person_Take_Date.HasValue ? 0
            : (decimal)Math.Min(10, Math.Round((DateTime.Now - BaseItem.Person_Take_Date.Value).TotalHours, 2));

        #endregion

        #region METHOD UIRefresh

        public void UIRefresh()
        {
            InitTakenTime();
        }

        #endregion

        #region METHOD InitTakenTime

        private void InitTakenTime()
        {
            TakenTimeText = !IsTaken ? string.Empty :
                    SConsts.GetTimeTextFromMinutes(GetMinutesFromTaken());
        }

        #endregion
    }
}
