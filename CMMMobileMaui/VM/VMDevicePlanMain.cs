using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CMMMobileMaui.API.Interfaces;
using System.Linq;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VM
{
    public class VMDevicePlanMain: COMMON.ViewModelBase
    {
        #region PROPERTY ItemsList

        public List<GetWOsResponse> ItemsList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentWO

        public GetWOsResponse CurrentWO
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceID

        public int? DeviceID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY TopItems

        public int TopItems
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY TopItemsText

        public string TopItemsText
        {
            get
            {
                return TopItems.ToString();
            }
        }

        private IWOController workOrderBLL;

        #endregion

        #region COMMAND ShowItemCommand

        public ICommand ShowItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND LoadNextItemCommand

        public ICommand LoadNextItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND RefreshListCommand

        public ICommand RefreshListCommand
        {
            get;
        }

        #endregion

        #region COMMAND SwipeLeftCommand

        public ICommand SwipeLeftCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDevicePlanMain(IWOController workOrderBLL)
        {
            this.workOrderBLL = workOrderBLL;
            ShowItemCommand = new Command((obj) => ShowItem((GetWOsResponse)obj));
            LoadNextItemCommand = new Command(async (obj) => await RefreshList(20));
            RefreshListCommand = new Command(async (obj) => await RefreshList(0));

            SwipeLeftCommand = new Command(async (obj) =>
            {
                if (obj != null)
                {
                    if (CanClick())
                    {
                        CurrentWO = obj as GetWOsResponse;

                        await OpenModalPage(new VIEW.WorkOrderSingleView(CurrentWO));
                    }
                }
            });
        }

        #endregion

        #region METHOD ShowItem

        private async void ShowItem(GetWOsResponse wo)
        {
            if (CanClick())
            {
                CurrentWO = wo;
                await OpenModalPage(new VIEW.WorkOrderSingleView(CurrentWO));
            }
        }

        #endregion

        #region METHOD RefreshList

        public async Task RefreshList(int value)
        {
            if (CanClick())
            {
                if (TopItems <= 100)
                {
                    IsBusy = true;

                    var wosResponse = await this.workOrderBLL.GetList(new API.Contracts.v1.Requests.WO.GetWOsRequest
                    {
                        DeviceID = DeviceID
                        , IsPlan = true
                    });

                    if (wosResponse.IsValid)
                    {
                        ItemsList = wosResponse.Data.ToList();

                        if (ItemsList != null && ItemsList.Count == TopItems)
                        {
                            if (TopItems < 100)
                            {
                                TopItems += value;
                            }
                        }

                        OnPropertyChanged("TopItems");
                        OnPropertyChanged("ItemsList");
                    }

                    IsBusy = false;
                }
            }
        }

        #endregion

        #region METHOD SetMainData

        public async Task SetMainData()
        {
            IsBusy = true;

            TopItems = 20;

            var wosResponse = await this.workOrderBLL.GetList(new API.Contracts.v1.Requests.WO.GetWOsRequest
            {
                DeviceID = DeviceID
                        ,
                IsPlan = true
            });

            if (wosResponse.IsValid)
            {
                ItemsList = wosResponse.Data.ToList();

                if (ItemsList != null && ItemsList.Count == 20)
                {
                    TopItems = 40;
                }
            }

            OnPropertyChanged("TopItems");
            OnPropertyChanged("ItemsList");

            IsBusy = false;
        }

        #endregion
    }
}
