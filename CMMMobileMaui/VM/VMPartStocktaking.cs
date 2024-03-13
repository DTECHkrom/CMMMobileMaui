using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Requests.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Windows.Input;

namespace CMMMobileMaui.VM
{
    public class VMPartStocktaking : COMMON.ViewModelBase
    {
        #region FIELDS

        private readonly IPartController partBLL;

        #endregion

        #region PROPERTY StackList

        private List<GetStocktakingViewResponse>? stackList;
        public List<GetStocktakingViewResponse>? StackList
        {
            get => stackList;
            set => SetProperty(ref stackList, value);
        }

        #endregion

        #region PROPERTY WarehouseList

        public List<PartDictResponse> WarehouseList =>
            DictionaryResources.Instance.WarehouseList;

        #endregion

        #region PROPERTY EditStocktaking

        public CreateStocktakingRequest? EditStocktaking
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentWarehouse

        public PartDictResponse? CurrentWarehouse
        {
            get;
            set;
        }

        #endregion

        #region COMMAND ShowItemCommand

        public ICommand ShowItemCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (obj is GetStocktakingViewResponse stock)
                    {
                        if (CanClick())
                        {
                            await OpenModalPage(new VIEW.PartInvenMainView(stock));
                        }
                    }
                });
            }
        }

        #endregion

        #region COMMAND AddStocktakingCommand

        public ICommand AddStocktakingCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CanClick())
                    {
                        SetAddMainData();
                    }
                });
            }
        }

        #endregion

        #region COMMAND SaveAddCommand

        public ICommand SaveAddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (CanClick())
                    {
                        if (EditStocktaking != null
                        && CurrentWarehouse != null)
                        {
                            EditStocktaking.PersonID = MainObjects.Instance.CurrentUser!.PersonID;
                            EditStocktaking.WarehouseID = CurrentWarehouse.ID;

                            var createResponse = await partBLL.CreateStocktaking(EditStocktaking);

                            await MopupService.Instance.PopAsync(true);

                            if (!IsBusy)
                            {
                                IsBusy = true;
                            }
                        }
                    }
                });
            }
        }

        #endregion

        #region COMMAND CancelAddCommand

        public ICommand CancelAddCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CanClick())
                    {
                        MopupService.Instance.PopAsync(true);
                    }
                });
            }
        }

        #endregion

        #region COMMAND EndStocktakingCommand

        public ICommand EndStocktakingCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (obj != null
                    && obj is GetStocktakingViewResponse stock)
                    {
                        if (CanClick())
                        {
                            var isOK = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                            if (isOK)
                            {
                                var endResponse = await partBLL.EndStocktaking(new UpdateStocktakingEndRequest
                                {
                                    StocktakingID = stock.SparePartStocktakingID
                                    ,
                                    PersonID = MainObjects.Instance.CurrentUser!.PersonID
                                });

                                if (endResponse.IsValid)
                                {
                                    if (!IsBusy)
                                    {
                                        IsBusy = true;
                                    }
                                }
                            }
                        }
                    }
                });
            }
        }

        #endregion

        #region COMMAND DeleteItemCommand

        public ICommand DeleteItemCommand
        {
            get;
        }


        #endregion

        #region COMMAND LoadCommand

        public IAsyncRelayCommand LoadCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMPartStocktaking(IPartController partBLL)
        {
            this.partBLL = partBLL;
            LoadCommand = new AsyncRelayCommand(SetMainData);
            StackList = new List<GetStocktakingViewResponse>();

            DeleteItemCommand = new Command(async (obj) =>
            {
                if (obj != null
                && obj is GetStocktakingViewResponse stock)
                {
                    if (CanClick())
                    {
                        bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure_delete"));

                        if (dialogResult)
                        {
                            DeleteItemMethod(stock);
                        }
                    }
                }
            });
        }

        #endregion

        #region METHOD DeleteItemMethod

        private async void DeleteItemMethod(GetStocktakingViewResponse stock)
        {
            var deleteResponse = await partBLL.DeleteStocktaking(new GetByStocktakingIDRequest
            {
                StocktakingID = stock.SparePartStocktakingID
            });

            if (deleteResponse.IsValid)
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                }
            }
        }

        #endregion

        #region METHOD SetAddMainData

        private async void SetAddMainData()
        {
            EditStocktaking = new CreateStocktakingRequest();
            CUST.PartStocktaking pc = new CUST.PartStocktaking();
            pc.BindingContext = this;

            await OpenPopup(pc);
        }

        #endregion

        #region METHOD SetMainData

        private async Task SetMainData()
        {
            var stockResponse = await partBLL.GetStocktaking();

            StackList?.Clear();

            if (stockResponse.IsResponseWithData(this))
            {
                StackList = stockResponse.Data!
                    .OrderByDescending(tt => tt.ModDate)
                    .ToList();
            }

            IsBusy = false;
        }

        #endregion
    }
}
