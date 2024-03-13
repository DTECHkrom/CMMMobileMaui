using Mopups.Services;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.MODEL;

namespace CMMMobileMaui.VM
{
    public class VMWOActivityPop : ViewModelBase
    {
        #region FIELDS

        private IActController actController;
        private int workOrderID;
        private bool shouldRefresh;

        #endregion

        #region PROPERTY ActCatList

        public List<WOActDictResponse> ActCatList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ActCat

        public WOActDictResponse ActCat
        {
            get;
            set;
        }

        #endregion

        #region PROPERY WorkLoad

        private decimal workLoad;

        public decimal WorkLoad
        {
            get => workLoad;
            set => workLoad = value;
        }

        #endregion

        #region PROPERTY Description

        private string description;

        public string Description
        {
            get => description;
            set => description = value;
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

        #region Cstr

        public VMWOActivityPop()
        {
            actController = MainObjects.Instance.ServiceProvider.GetRequiredService<IActController>();

            SaveCommand = new Command(() =>
            {
                if (CanClick())
                {
                    if (CanSave())
                    {
                        Save();
                    }
                }
            });

            CloseCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    await MopupService.Instance.PopAsync();
                }
            });

        }

        #endregion

        #region METHOD Save

        private async void Save()
        {

            var result = await actController.Create(new API.Contracts.v1.Requests.Act.CreateWOActivityRequest
            {
                CategoryID = ActCat.ID
                ,
                PersonID = MainObjects.Instance.CurrentUser.PersonID
                ,
                Description = Description
                ,
                WorkLoad = workLoad
                ,
                WorkOrderID = workOrderID
            });

            if (result.IsValid)
            {
                shouldRefresh = true;
                await MopupService.Instance.PopAsync();
            }
        }

        #endregion

        #region METHOD GetShouldRefresh

        public bool GetShouldRefresh() =>
            shouldRefresh;

        #endregion

        #region METHOD CanSave

        private bool CanSave() =>
            ActCat != null
            && WorkLoad > 0
            && !string.IsNullOrEmpty(Description);

        #endregion

        #region METHOD Init

        public void Init(WOModel woModel)
        {
            workOrderID = woModel.BaseItem.WorkOrderID;

            WorkLoad = woModel.GetCalculatedWorkLoad();

            ActCatList = DictionaryResources.Instance.GetWOActDictListFor(woModel.BaseItem.DeviceCategoryID);
            OnPropertyChanged(nameof(ActCatList));
            OnPropertyChanged(nameof(WorkLoad));
        }

        #endregion
    }
}
