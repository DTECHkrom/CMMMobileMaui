using Mopups.Services;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.VM
{
    public class VMWorkOrderPartMain : ScannerViewModelBase
    {
        public enum PartSearchMode : int
        {
            Main = 0,
            Giver,
            Inventory,
            WO
        }

        #region FIELDS

        private IWOController woController;
        private IPartController partBLL;

        #endregion

        #region PROPERTY CurrentSearchMode

        public PartSearchMode CurrentSearchMode
        {
            get;
            set;
        } = PartSearchMode.Main;

        #endregion

        #region PROPERTY WorkOrderID

        private int _workOrderID;

        public int WorkOrderID
        {
            get
            {
                return _workOrderID;
            }

            set
            {
                _workOrderID = value;
                //   SetMainData();
            }
        }

        #endregion

        #region PROPERTY PartStockList

        public ObservableCollection<GetWOPartsResponse> PartStockList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WarehouseList

        public List<PartDictResponse> WarehouseList =>
            DictionaryResources.Instance.WarehouseList;

        #endregion

        #region PROPERTY PartList

        private List<GetPartDetailShortResponse> partList;

        public List<GetPartDetailShortResponse> PartList
        {
            get => partList;
            set => SetProperty(ref partList, value);
        }

        #endregion

        #region PROPERTY CurrentWar

        private PartDictResponse? currentWar;

        public PartDictResponse? CurrentWar
        {
            get => currentWar;
            set => SetProperty(ref currentWar, value);
        }

        #endregion

        #region PROPERTY PartFilter

        public string PartFilter
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentPart

        public GetPartDetailShortResponse CurrentPart
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY TakeAmount

        public decimal TakeAmount
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CanSelectWarehouse

        private bool canSelectWarehouse;

        public bool CanSelectWarehouse
        {
            get => canSelectWarehouse;
            set => SetProperty(ref canSelectWarehouse, value);
        }

        #endregion

        #region COMMAND SearchPartCommand

        public ICommand SearchPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND ScanPartCommand

        public ICommand ScanPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND FilterCommand

        public ICommand FilterCommand
        {
            get;
        }

        #endregion

        #region COMMAND TakePartCommand

        public ICommand TakePartCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelTakeCommand

        public ICommand CancelTakeCommand
        {
            get;
        }

        #endregion

        #region COMMAND ConfirmTakeCommand

        public ICommand ConfirmTakeCommand
        {
            get;
        }

        #endregion

        #region COMMAND OrderPartCommand

        public ICommand OrderPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowDetailsCommand

        public ICommand ShowDetailsCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddNewPartCommand

        public ICommand AddNewPartCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMWorkOrderPartMain(IPartController partBLL, IWOController woController)
        {
            this.partBLL = partBLL;
            this.woController = woController;

            CanSelectWarehouse = true;
            PartStockList = new ObservableCollection<GetWOPartsResponse>();

            //TODO check part scan
            SConsts.SetGlobalAction<GetPartDetailShortResponse>(SConsts.PART_SCAN, async (item) =>
            {
                if (item != null)
                {
                    if (Shell.Current.Navigation.ModalStack.Count > 0)
                        await Shell.Current.Navigation.PopModalAsync();

                    CurrentPart = item as GetPartDetailShortResponse;

                    if (CurrentSearchMode == PartSearchMode.WO)
                    {
                        CUST.PopCust pc = new CUST.PopCust();
                        pc.BindingContext = this;

                        await OpenPopup(pc);
                    }
                    else if (CurrentSearchMode == PartSearchMode.Main)
                    {
                        await OpenModalPage(new VIEW.PartDetailView(CurrentPart.PartID));
                    }
                }
            });

            SearchPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.PartSearchView vPart = new VIEW.PartSearchView();
                    vPart.BindingContext = vPart.ViewModel = this;
                    vPart.SetSearchMode(CurrentSearchMode);

                    vPart.Disappearing += (s, e) =>
                    {
                        CurrentWar = null;

                        if (PartList != null)
                        {
                            PartList.Clear();
                        }

                        PartFilter = string.Empty;
                    };

                    await OpenModalPage(vPart);
                }
            });

            AddNewPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(SConsts.GetBaseNavigationPage(new VIEW.NewPartView()));
                }
            });


            FilterCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (!string.IsNullOrEmpty(PartFilter) && CurrentWar != null)
                    {
                        IsBusy = true;

                        var partsResponse = await partBLL.GetDetailShort(new API.Contracts.v1.Requests.Part.GetPartDetailShortRequest
                        {
                            Filter = PartFilter.ToLower()
                            ,
                            WarID = CurrentWar.ID
                            ,
                            Lang = MainObjects.Instance.Lang
                        });

                        if (partsResponse.IsResponseWithData(this))
                        {
                            PartList = partsResponse.Data
                            .OrderBy(tt => tt.PartNo)
                            .ToList();

                            OnPropertyChanged("PartList");
                        }

                        IsBusy = false;
                    }
                }
            });

            TakePartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null
                    && obj is GetPartDetailShortResponse part)
                    {
                        CurrentPart = part;
                        await Shell.Current.Navigation.PopModalAsync();

                        if (CurrentSearchMode == PartSearchMode.WO)
                        {
                            CUST.PopCust pc = new CUST.PopCust();
                            pc.BindingContext = this;
                            await OpenPopup(pc);
                        }
                    }
                }
            });

            ScanPartCommand = new Command(async (obj) =>
            {
                await OpenModalPage(new VIEW.ScanView(ScanType.Part));
            });

            CancelTakeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await MopupService.Instance.PopAsync(true);
                }
            });

            ShowDetailsCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null && obj
                    is GetPartDetailShortResponse part)
                    {
                        var page = new VIEW.PartDetailView(part.PartID);

                        if (CurrentSearchMode == PartSearchMode.Inventory
                        || CurrentSearchMode == PartSearchMode.Giver)
                        {
                            page.ViewModel.IsLockButtons = true;
                        }

                        await OpenModalPage(page);
                    }
                }
            });

            ConfirmTakeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (IsDataSet())
                    {
                        var takeResponse = await partBLL.Take(new API.Contracts.v1.Requests.Part.PartTakeRequest
                        {
                            PartTakeList = new List<API.Contracts.v1.Requests.Part.SinglePartTake>
                            {
                                new API.Contracts.v1.Requests.Part.SinglePartTake
                                {
                                    WorkOrderID = this.WorkOrderID
                                    , PersonID = MainObjects.Instance.CurrentUser!.PersonID
                                    , PartID = CurrentPart!.PartID
                                    , Quantity = TakeAmount
                                }
                            }
                        });

                        if (takeResponse.IsResponseWithData(this))
                        {
                            await MopupService.Instance.PopAsync(true);
                            SetMainData();
                        }
                    }
                }
            });

            OrderPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (TakeAmount > 0)
                    {
                        var createResponse = await partBLL.CreateOrder(new API.Contracts.v1.Requests.Part.CreatePartOrderRequest
                        {
                            WorkOrderID = WorkOrderID
                            ,
                            PartID = CurrentPart!.PartID
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            Quantity = TakeAmount
                        });

                        if (createResponse.IsResponseWithData(this))
                        {
                            TakeAmount = 0;
                            await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");

                            await MopupService.Instance.PopAsync(true);

                            SetMainData();
                        }
                    }
                }
            });
        }

        #endregion

        private bool IsDataSet() =>
            (CurrentPart.StockLevel.HasValue && !CurrentPart.IsExchangeable && TakeAmount > 0 && TakeAmount <= CurrentPart.StockLevel)
            || CurrentPart.IsExchangeable;

        #region METHOD SetMainData

        public async void SetMainData()
        {
            IsBusy = true;

            var stockTask = woController.GetParts(new API.Contracts.v1.Requests.WO.GetByWorkOrderIDRequest
            {
                WorkOrderID = _workOrderID
            });

            await Task.WhenAll(stockTask);

            var stockResult = stockTask.Result;

            SetMainPartData();

            PartStockList.Clear();

            if (stockResult.IsResponseWithData(this))
            {
                stockResult.Data!
                    .ToList()
                    .ForEach(PartStockList.Add);
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD SetMainPartData

        public void SetMainPartData()
        {
            if (WarehouseList != null
            && WarehouseList.Count == 1)
            {
                CurrentWar = WarehouseList.FirstOrDefault();
            }
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> list = new List<IScanType>();

            var partScan = new PartScan();

            partScan.UIMethod = async (obj) =>
            {
                if (obj is GetPartDetailShortResponse part)
                {
                    if (CurrentSearchMode == PartSearchMode.Main)
                    {
                        await OpenModalPage(new VIEW.PartDetailView(part.PartID));
                    }
                    else if (CurrentSearchMode == PartSearchMode.Giver
                    || CurrentSearchMode == PartSearchMode.Inventory)
                    {
                        CurrentPart = part;
                        await Shell.Current.Navigation.PopModalAsync();
                    }
                    else if (CurrentSearchMode == PartSearchMode.WO)
                    {
                        if (Shell.Current.Navigation.ModalStack.Count == 3)
                        {
                            await Shell.Current.Navigation.PopModalAsync();
                        }

                        CurrentPart = part;
                        CUST.PopCust pc = new CUST.PopCust();

                        pc.BindingContext = this;
                        await OpenPopup(pc);
                    }
                }
            };

            list.Add(partScan);

            return list;
        }

        #endregion
    }
}
