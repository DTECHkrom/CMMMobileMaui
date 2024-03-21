using Mopups.Services;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VM
{
    public class VMPartInvenMain : ScannerViewModelBase
    {
        #region FIELDS

        private IPartController partBLL;

        #endregion

        #region PROPERTY currentStack

        private GetStocktakingViewResponse currentStack;

        public GetStocktakingViewResponse CurrentStack
        {
            get => currentStack;
            set
            {
                SetProperty(ref currentStack, value);

                CurrentWar = WarehouseList.FirstOrDefault(tt=> tt.ID == currentStack.WarehouseID);
                CanSelectWarehouse = false;
            }
        }

        #endregion

        #region PROEPRTY StackPartList

        private List<GetPartStocktakingResponse> _stackPartOrgList;

        public List<GetPartStocktakingResponse> StackPartOrgList
        {
            get
            {
                return _stackPartOrgList;
            }
            set
            {
                _stackPartOrgList = value;

                if (_stackPartOrgList != null)
                {
                    IsSaveDBCommand = _stackPartOrgList.Count(tt => !tt.IsSet) != 0;
                    OnPropertyChanged("IsSaveDBCommand");
                }

                SetStackPartList();
            }
        }

        #endregion

        #region PROPERTY StackPartList

        private List<GetPartStocktakingResponse> stackPartList;

        public List<GetPartStocktakingResponse> StackPartList
        {
            get => stackPartList;
            set => SetProperty(ref stackPartList, value);
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

        #region PROPERTY IsDataToRefresh

        public bool IsDataToRefresh
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

        public List<GetPartDetailShortResponse> PartList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrencyList

        public List<PartDictResponse> CurrencyList =>
            DictionaryResources.Instance.CurrencyList;


        #endregion  

        #region PROPERTY CurrentCurrency

        public PartDictResponse? CurrentCurrency
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentWar

        public PartDictResponse? CurrentWar
        {
            get;
            set;
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
            get => currentPart;
            set => SetProperty(ref currentPart, value);
        }

        #endregion

        #region PROPERTY TakeAmount

        private decimal? takeAmount;

        public decimal? TakeAmount
        {
            get => takeAmount;
            set
            {
                SetProperty(ref takeAmount, value);

                IsUnitPriceEnabled = (takeAmount.HasValue) && (!CurrentPart.StockLevel.HasValue
                || CurrentPart.StockLevel.Value < takeAmount.Value);
            }
        }

        #endregion

        #region PROPERTY IsUnitPriceEnabled

        private bool isUnitPriceEnabled;
        private GetPartDetailShortResponse currentPart;

        public bool IsUnitPriceEnabled
        {
            get => isUnitPriceEnabled;
            set
            {
                SetProperty(ref isUnitPriceEnabled, value);
            }
        }

        #endregion

        #region PROPERTY UnitPrice

        public decimal? UnitPrice
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ShouldChangeState

        public bool ShouldChangeState
        {
            get;
            set;
        }


        #endregion

        #region PROPERTY IsSaveDBCommand

        public bool IsSaveDBCommand
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY StackPartFilter

        public string StackPartFilter
        {
            get;
            set;
        }

        #endregion

        #region COMMAND EditItemCommand

        public ICommand EditItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND SearchPartCommand

        public ICommand SearchPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND FilterPartCommand

        public ICommand FilterPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND DeletePartCommand

        public ICommand DeletePartCommand
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

        #region COMMAND SaveDBCommand

        public ICommand SaveDBCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCheckedItemsCommand

        public ICommand SaveCheckedItemsCommand
        {
            get;
        }

        #endregion

        #region COMMAND CloseCheckedItemsCommand

        public ICommand CloseCheckedItemsCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenPartInfoCommand

        public ICommand OpenPartInfoCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMPartInvenMain(IPartController partBLL)
        {
            this.partBLL = partBLL;

            CanSelectWarehouse = true;

            SConsts.SetGlobalAction<GetPartDetailShortResponse>(SConsts.PART_SCAN_INV, (item) =>
            {
                if (item is GetPartDetailShortResponse part)
                {
                    CurrentPart = part;
                    Shell.Current.Navigation.PopModalAsync();

                    OpenStackDetailsForm();
                }
            });

            OpenPartInfoCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetPartStocktakingResponse partStack)
                    {
                        VIEW.PartDetailView pView = new VIEW.PartDetailView(partStack.PartID, true);
                        await OpenModalPage(pView);
                    }
                }
            });

            CloseCheckedItemsCommand = new Command(async (obj) =>
             {
                 if (CanClick())
                 {
                     await Shell.Current.Navigation.PopModalAsync();
                 }
             });

            SaveCheckedItemsCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    //List<StocktakingPart> list = new List<StocktakingPart>();

                    //foreach (var item in StackPartCheckedList)
                    //{
                    //    list.Add(new StocktakingPartBuilder(item)
                    //            .Other.AddedBy(CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID)
                    //                    .ShouldChangePartState(true));
                    //}

                    //bool isOk = await partBLL.UpdateStocktaking(new CMMMobileMaui.API.Contracts.v1.Requests.Part.UpdateStocktakingRequest
                    //{

                    //});

                    //if (isOk)
                    //{
                    //    if (PartList != null)
                    //    {
                    //        PartList.Remove(CurrentPart);
                    //        OnPropertyChanged("PartList");
                    //    }

                    //    CurrentPart = null;
                    //    await App.Current.MainPage.Navigation.PopModalAsync();
                    //    RefreshDataAfterUpdate();
                    //}
                }
            });

            FilterPartCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    SetStackPartList();
                }
            });

            //SaveDBCommand = new Command((obj) =>
            //{
            //    if(CanClick())
            //    {
            //        SetSparePartListToCheck();
            //    }
            //});

            EditItemCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj != null)
                    {

                        //var sPart = obj as vStackPart;

                        //if (sPart != null)
                        //{
                        //    CurrentPart = new GetPartDetailShortResponse();
                        //    CurrentPart.PartID = sPart.PartID;
                        //    CurrentPart.PartNo = sPart.Part_No;
                        //    CurrentPart.UnitPrice = sPart.Unit_Price;
                        //    TakeAmount = sPart.New_Value;
                        //    OnPropertyChanged("TakeAmount");
                        //    CurrentPart.WarehouseID = CurrentStack.WarehouseID;
                        //    CurrentPart.CurrencyName = sPart.CurrencyName;

                        //    OpenStackDetailsForm();
                        //}
                    }

                }
            });

            SearchPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.PartSearchView vPart = new VIEW.PartSearchView();
                    vPart.SetSearchMode(VMWorkOrderPartMain.PartSearchMode.Inventory);
                    vPart.ViewModel.CurrentWar = CurrentWar;
                    vPart.ViewModel.CanSelectWarehouse = false;
                    // vPart.BindingContext = this;

                    vPart.Disappearing += (s, e) =>
                    {
                        if (vPart.ViewModel.CurrentPart != null)
                        {
                            SetViewForSelectedPart(vPart.ViewModel.CurrentPart);
                        }

                        PartFilter = string.Empty;
                    };

                    await OpenModalPage(vPart);
                }
            });

            FilterCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    RefreshFilterMethod();
                }
            });

            TakePartCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetPartDetailShortResponse part)
                    {
                        CurrentPart = part;
                        Shell.Current.Navigation.PopModalAsync();

                        OpenStackDetailsForm();
                    }
                }
            });

            ScanPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.ScanView(ScanType.PartInv));
                }
            });

            CancelTakeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await MopupService.Instance.PopAsync(true);
                }
            });

            ConfirmTakeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CanTakeSinglePart())
                    {
                        var createResponse = await partBLL.CreateStocktakingPart(new API.Contracts.v1.Requests.Part.CreateStocktakingPartRequest
                        {
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            StocktakingID = CurrentStack.SparePartStocktakingID
                            ,
                            PartList = new List<API.Contracts.v1.Requests.Part.PartStocktakingSingle>
                            {
                                new API.Contracts.v1.Requests.Part.PartStocktakingSingle
                                {
                                    PartID = CurrentPart!.PartID
                                    , UnitPrice = UnitPrice.Value
                                    , NewValue = TakeAmount.Value
                                    , OldValue = CurrentPart.StockLevel.Value
                                    , CurrencyID = CurrentCurrency.ID
                                }
                            }
                        });

                        if (createResponse.IsResponseWithData(this))
                        {
                            TakeAmount = 0;
                            CurrentPart = null;
                            CurrentCurrency = null;
                            UnitPrice = 0;

                            await MopupService.Instance.PopAsync(true);

                            RefreshDataAfterUpdate();
                        }

                    }
                }
            });

            DeletePartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null
                    && obj is GetPartStocktakingResponse partStock)
                    {
                        bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure_delete"));

                        if (dialogResult)
                        {
                            DeletePartMethod(partStock);
                        }
                    }
                }
            });
        }

        #endregion

        #region METHOD CanTakeSinglePart
        private bool CanTakeSinglePart()
        {
            return (!IsUnitPriceEnabled && TakeAmount.HasValue &&
                                TakeAmount.Value >= 0)
                                || (IsUnitPriceEnabled && TakeAmount.HasValue &&
                                TakeAmount.Value >= 0 && CurrentCurrency != null
                                && UnitPrice.HasValue && UnitPrice.Value >= 0);
        }

        #endregion

        #region METHOD DeletePartMethod

        private async void DeletePartMethod(GetPartStocktakingResponse getPartStocktaking)
        {
            var deleteResponse = await partBLL.DeleteStocktakingPart(new API.Contracts.v1.Requests.Part.DeleteStocktakingPartRequest
            {
                PartID = getPartStocktaking.PartID
                ,
                StocktakingID = CurrentStack.SparePartStocktakingID
            });


            if (deleteResponse.IsValid)
            {
                RefreshDataAfterUpdate();
            }
        }

        #endregion

        //#region METHOD SetSparePartListToCheck

        //private async void SetSparePartListToCheck()
        //{
        //    await OpenModalPage(new VIEW.PartStocktakingPartCheckView() { BindingContext = this });

        //    IsBusy = true;

        //    var items = StackPartList.Where(tt => !tt.IsSet).ToList();

        //    var ids = items.Select(tt => tt.PartID).ToList();

        //    var parts = await partBLL.GetByIDs(ids);

        //    foreach (var item in items)
        //    {
        //        var part = parts.FirstOrDefault(tt => tt.PartID == item.PartID);

        //        if (part != null)
        //        {
        //            item._Org_Value = part.Stock_Change;
        //            item._New_Value = item.New_Value;
        //        }
        //    }

        //    StackPartCheckedList = items.ToList();

        //    OnPropertyChanged("StackPartCheckedList");


        //    IsBusy = false;
        //}

        //#endregion

        #region METHOD SetStackPartList

        private void SetStackPartList()
        {
            if (StackPartOrgList == null)
            {
                StackPartList = new List<GetPartStocktakingResponse>();
                OnPropertyChanged(nameof(StackPartList));
                return;
            }

            if (string.IsNullOrEmpty(StackPartFilter))
            {
                StackPartList = StackPartOrgList.ToList();
            }
            else
            {
                StackPartList = StackPartOrgList.Where(tt => tt.PartNo.ToLower().Contains(StackPartFilter.ToLower())).ToList();
            }

            OnPropertyChanged(nameof(StackPartList));
        }

        #endregion

        #region METHOD RefreshFilterMethod

        private async void RefreshFilterMethod()
        {
            if (CurrentWar != null)
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
                    PartList = partsResponse.Data!
                        .Where(t => !StackPartOrgList.Select(dd => dd.PartID).Contains(t.PartID)).ToList();
                }

                IsBusy = false;
            }
        }

        #endregion

        #region METHOD OpenStackDetailsForm

        private async void OpenStackDetailsForm()
        {
            UnitPrice = CurrentPart.UnitPrice ?? 0;

            if (CurrentCurrency == null)
            {
                string currencyName = !string.IsNullOrEmpty(CurrentPart.CurrencyName) ? CurrentPart.CurrencyName : MainObjects.Instance.AppSettings!.CurrencyName;
                CurrentCurrency = CurrencyList.FirstOrDefault(tt => tt.Name == currencyName);
            }

            //TODO get currencylist if is null and 

            CUST.PartInven pc = new CUST.PartInven();
            pc.BindingContext = this;

            await OpenPopup(pc, true);
        }

        #endregion

        #region METHOD SetMainData

        public void SetMainData()
        {
            RefreshDataAfterUpdate();
        }

        #endregion

        #region METHOD RefreshDataAfterUpdate

        private async void RefreshDataAfterUpdate()
        {
            IsBusy = true;

            if (CurrentStack != null)
            {
                var stockResponse = await partBLL.GetPartStocktaking(new API.Contracts.v1.Requests.Part.GetByStocktakingIDRequest
                {
                    StocktakingID = CurrentStack.SparePartStocktakingID
                });

                if (stockResponse.IsResponseWithData(this))
                {
                    StackPartList = stockResponse.Data!.ToList();
                }
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            var partScan = new PartScan();

            partScan.UIMethod = (obj) =>
            {
                if (obj is GetPartDetailShortResponse part)
                {
                    SetViewForSelectedPart(part);
                }
            };

            scanTypes.Add(partScan);

            return scanTypes;
        }

        #endregion

        #region METHOD SetViewForSelectedPart

        private void SetViewForSelectedPart(GetPartDetailShortResponse part)
        {
            if (part.WarehouseID == CurrentStack.WarehouseID)
            {
                CurrentPart = part;
                OpenStackDetailsForm();
            }
            else
            {
                App.CurrentScanManager?.ShowScanResult(ScanMode.Fail);
            }
        }

        public override string GetVisualScanPresentation() => "extension";

        #endregion

    }
}
