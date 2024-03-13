using Mopups.Services;
using System.Reflection;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VM
{
    public class VMPartDetail : ScannerViewModelBase
    {
        public enum PartDetailViewMode
        {
            View,
            ChangeLocation
        }

        private int partID;
        private IPartController partBLL;

        #region PROPERY ViewMode

        public PartDetailViewMode ViewMode
        {
            get;
            set;
        } = PartDetailViewMode.View;

        #endregion

        #region PROPERTY IsGivePart

        private bool isGivePart;

        public bool IsGivePart
        {
            get => isGivePart;
            set => SetProperty(ref isGivePart, value);
        }

        #endregion

        #region PROPERTY IsLockButtons

        public bool IsLockButtons
        {
            get;
            set;
        } = false;

        #endregion

        #region PROPERTY TakeAmount

        private decimal? takeAmount;

        public decimal? TakeAmount
        {
            get => takeAmount;
            set => SetProperty(ref takeAmount, value);
        }

        #endregion

        #region PROPERTY UnitPrice

        private decimal? unitPrice;

        public decimal? UnitPrice
        {
            get => unitPrice;
            set => unitPrice = value;
        }

        #endregion

        #region PROPERTY CanModPartQuantity

        private bool canModPartQuantity;
        public bool CanModPartQuantity
        {
            get => canModPartQuantity;
            set => SetProperty(ref canModPartQuantity, value);
        }

        #endregion

        #region PROPERTY AddPartCurrency

        private PartDictResponse? addPartCurrency;

        public PartDictResponse? AddPartCurrency
        {
            get => addPartCurrency;
            set => SetProperty(ref addPartCurrency, value);
        }

        #endregion

        #region PROPERTY CurrencyList

        public List<PartDictResponse> CurrencyList =>
            DictionaryResources.Instance.CurrencyList;

        #endregion

        #region PROPERTY IsPartInfo

        private bool isPartInfo;

        public bool IsPartInfo
        {
            get => isPartInfo;
            set => SetProperty(ref isPartInfo, value);
        }

        #endregion

        #region PROPERTY CurrentItem

        private GetPartDetailResponse currentPart;
        public GetPartDetailResponse CurrentPart
        {
            get => currentPart;
            set => SetProperty(ref currentPart, value);
        }

        #endregion

        #region PROPERTY PartDetailList

        private List<GetDeviceDetailsResponse>? partDetailList;

        public List<GetDeviceDetailsResponse>? PartDetailList
        {
            get => partDetailList;
            set => SetProperty(ref partDetailList, value);
        }

        #endregion

        #region PROEPRTY PartChangeList

        private List<GetPartChangeResponse>? partChangeList;
        public List<GetPartChangeResponse>? PartChangeList
        {
            get => partChangeList;
            set => SetProperty(ref partChangeList, value);
        }

        #endregion

        #region PROPERTY CurrentChange

        private GetPartChangeResponse? currentChange;

        public GetPartChangeResponse? CurrentChange
        {
            get => currentChange;
            set => SetProperty(ref currentChange, value);
        }

        #endregion

        #region PROPERTY SelectedLocation

        private GetWarehouseLocationResonse? selectedLocation;

        public GetWarehouseLocationResonse? SelectedLocation
        {
            get => selectedLocation;
            set => SetProperty(ref selectedLocation, value);
        }

        #endregion

        #region PROPERTY CurrentLocation

        private GetPartLocationResponse? currentLocation;

        public GetPartLocationResponse? CurrentLocation
        {
            get => currentLocation;
            set => SetProperty(ref currentLocation, value);
        }

        #endregion

        #region PROPERTY LocationList

        private List<GetWarehouseLocationResonse>? locationList;

        public List<GetWarehouseLocationResonse>? LocationList
        {
            get => locationList;
            set => SetProperty(ref locationList, value);
        }

        #endregion

        #region PROPERTY CurrentLocationList

        private List<GetPartLocationResponse>? currentLocationList;

        public List<GetPartLocationResponse>? CurrentLocationList
        {
            get => currentLocationList;
            set => SetProperty(ref currentLocationList, value);
        }

        #endregion

        #region PROPERTY PictureManager

        public COMMON.PictureOperation.PictureManager PictureManager
        {
            get;
            set;
        } = new COMMON.PictureOperation.PictureManager();

        #endregion

        #region COMMAND GivePartCommand

        public ICommand GivePartCommand
        {
            get;
        }

        #endregion

        #region COMMAND TakePictureCommand

        public ICommand TakePictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectPictureCommand

        public ICommand SelectPictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenImageCommand
        
        public ICommand OpenImageCommand
        {
            get;
        }

        #endregion

        #region PROPERTY IsImagePanelVisible

        private bool isImagePanelVisible;      

        public bool IsImagePanelVisible
        {
            get => isImagePanelVisible;
            set
            {
                SetProperty(ref isImagePanelVisible, value);

            }
        }

        #endregion

        #region COMMAND OrderPartCommand

        public ICommand OrderPartCommand
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

        #region COMMAND ShowChangeCommand

        public ICommand ShowChangeCommand
        {
            get;
        }

        #endregion

        #region COMMAND PutBackCommand

        public ICommand PutBackCommand
        {
            get;
        }

        #endregion

        #region COMMAND ChangeTakeAmountCommand

        public ICommand ChangeTakeAmountCommand
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

        #region COMMAND DeletePictureCommand

        public ICommand DeletePictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenCorrectionCommand

        public ICommand OpenCorrectionCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenChangeLocationCommand
        public ICommand OpenChangeLocationCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenAddCommand

        public ICommand OpenAddCommand
        {
            get;
        }

        #endregion

        //#region COMMAND SaveCorrectionCommand

        //public ICommand SaveCorrectionCommand
        //{
        //    get;
        //}

        //#endregion

        #region COMMAND SaveAddCommand

        public ICommand SaveAddCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveNewLocationCommand

        public ICommand SaveNewLocationCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowImagePanelCommand

        public ICommand ShowImagePanelCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMPartDetail(IPartController partBLL)
        {
            this.partBLL = partBLL;

            PictureManager.OnSelectPicture += PictureManager_OnSelectPicture;
            PictureManager.OnTakePicture += PictureManager_OnTakePicture;

            SConsts.SetGlobalAction(SConsts.PART_MSG_GIVE, async () =>
            {
                await SetMainData();
            });

            ShowImagePanelCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    if (IsImagePanelVisible)
                        return;

                    IsImagePanelVisible = true;

                    await Task.Run(async () =>
                    {
                        await Task.Delay(3000);
                        IsImagePanelVisible = false;
                    });
                }
            });

            OpenChangeLocationCommand = new Command(async (obj) =>
            {
                if (!CanClick())
                    return;

                CUST.PartChangeLocation pc = new CUST.PartChangeLocation();
                pc.BindingContext = this;

                if (CurrentLocationList == null
                || LocationList == null)
                {
                    var warLocTask = partBLL.GetWarehouseLocation(new API.Contracts.v1.Requests.Part.GetByWarehouseIDRequest
                    {
                        WarehouseID = this.CurrentPart.WarehouseID
                    });

                    var locationTask = partBLL.GetLocation(new API.Contracts.v1.Requests.Part.GetByPartIDRequest
                    {
                        PartID = this.CurrentPart.PartID
                    });

                    await Task.WhenAll(warLocTask, locationTask);

                    if (locationTask.Result.IsResponseWithData(this))
                    {
                        CurrentLocationList = locationTask.Result.Data!.ToList();

                        if (CurrentLocationList != null
                        && CurrentLocationList.Count == 1)
                        {
                            CurrentLocation = CurrentLocationList.First();
                        }
                    }

                    if (warLocTask.Result.IsResponseWithData(this))
                    {
                        LocationList = warLocTask.Result.Data!.ToList();
                    }
                }

                this.ViewMode = PartDetailViewMode.ChangeLocation;

                pc.Disappearing += (s, e) =>
                {
                    this.ViewMode = PartDetailViewMode.View;
                };

                await OpenPopup(pc);

            });

            OpenCorrectionCommand = new Command(async (obj) =>
            {
                if(!CanClick())
                {
                    return;
                }

                CUST.PartCorrection pc = new CUST.PartCorrection();
                pc.BindingContext = this;

                await OpenPopup(pc);
            });

            //SaveCorrectionCommand = new Command(async (obj) =>
            //{
            //    if(TakeAmount > 0)
            //    {
            //        var result = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

            //        if(result)
            //        {
            //            IsBusy = true;

            //            PartAdd partAdd = new PartAdd();
            //            partAdd.PartID = CurrentPart.PartID;
            //            partAdd.AddPersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID;
            //            partAdd.Quantity = TakeAmount;


            //            //bool isOk = await partBLL.C(partAdd);

            //            //if (isOk)
            //            //{
            //            //    await MopupService.Instance.PopAsync();

            //            //    if (MopupService.Instance.PopupStack.Count != 0)
            //            //        await MopupService.Instance.PopAsync();

            //            //    await SetMainData();
            //            //}

            //            IsBusy = false;
            //            TakeAmount = 0;
            //        }
            //    }
            //});

            SaveNewLocationCommand = new Command(async (obj) =>
            {
                if (!CanClick())
                    return;

                if (SelectedLocation != null)
                {
                    var result = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                    if (result)
                    {
                        IsBusy = true;

                        var updateLocation = new API.Contracts.v1.Requests.Part.UpdatePartLocationRequest
                        {
                            PartID = CurrentPart.PartID
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            NewLocation = new API.Contracts.v1.Requests.Part.SinglePartLocation
                            {
                                RackID = SelectedLocation.RackID
                            }
                        };

                        if (CurrentLocation != null)
                        {
                            updateLocation.OldLocation = new API.Contracts.v1.Requests.Part.SinglePartLocation
                            {
                                RackID = CurrentLocation.RackID
                            };
                        }

                        var updateResponse = await partBLL.UpdateLocation(updateLocation);

                        if (updateResponse.IsResponseWithData(this))
                        {
                            await MopupService.Instance.PopAsync();

                            if (MopupService.Instance.PopupStack.Count != 0)
                                await MopupService.Instance.PopAsync();

                            await SetMainData();

                            SelectedLocation = null;
                            CurrentLocation = null;
                            LocationList = null;
                            CurrentLocationList = null;
                        }

                        IsBusy = false;
                        TakeAmount = null;
                    }
                }
            });

            OpenAddCommand = new Command(async (obj) =>
            {
                if(!CanClick())
                {
                    return;
                }

                CUST.PartAddQuantity pc = new CUST.PartAddQuantity();
                pc.BindingContext = this;

                if (CurrencyList != null)
                {
                    if (MainObjects.Instance.AppSettings != null)
                    {
                        AddPartCurrency = CurrencyList.FirstOrDefault(tt => tt.ID == MainObjects.Instance.AppSettings.MainCurrencyID);
                    }
                }

                await OpenPopup(pc);
            });

            SaveAddCommand = new Command(async (obj) =>
            {
                if(!CanClick())
                    return;

                if (TakeAmount.HasValue && TakeAmount > 0 && UnitPrice.HasValue && AddPartCurrency != null)
                {
                    var result = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                    if (result)
                    {
                        IsBusy = true;

                        //PartAdd partAdd = new PartAdd();
                        //partAdd.PartID = CurrentPart.PartID;
                        //partAdd.CurrencyID = AddPartCurrency.ID;
                        //partAdd.AddPersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID;
                        //partAdd.Quantity = TakeAmount;
                        //partAdd.UnitPrice = UnitPrice.Value;

                        var addResponse = await partBLL.AddQuantity(new API.Contracts.v1.Requests.Part.CreatePartQuantityRequest
                        {
                            CurrencyID = AddPartCurrency.ID
                            ,
                            PartID = CurrentPart.PartID
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            Quantity = TakeAmount.Value
                            ,
                            UnitPrice = UnitPrice.Value
                        });

                        if (addResponse.IsResponseWithData(this))
                        {
                            await MopupService.Instance.PopAsync();

                            if (MopupService.Instance.PopupStack.Count != 0)
                                await MopupService.Instance.PopAsync();

                            await SetMainData();
                        }

                        IsBusy = false;
                    }

                    TakeAmount = null;
                    UnitPrice = null;
                    AddPartCurrency = null;
                }
            });

            PutBackCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetPartChangeResponse currentChange)
                    {
                        CurrentChange = currentChange;
                        TakeAmount = CurrentChange.AmountStateChange;

                        IsPartInfo = true;

                        CUST.PartPutBack pc = new CUST.PartPutBack();
                        pc.BindingContext = this;

                        await OpenPopup(pc);
                    }
                }
            });

            ChangeTakeAmountCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (TakeAmount.HasValue && TakeAmount > 0 && CurrentChange != null)
                    {
                        if (TakeAmount <= CurrentChange.AmountStateChange)
                        {
                            var returnResponse = await partBLL.Return(new API.Contracts.v1.Requests.Part.PartReturnRequest
                            {
                                PartID = CurrentPart.PartID
                                ,
                                PersonID = MainObjects.Instance.CurrentUser!.PersonID
                                ,
                                StockLevelChangeID = CurrentChange.StockLevelChangeID
                                ,
                                Quantity = TakeAmount.Value
                            });

                            if (returnResponse.IsValid)
                            {
                                TakeAmount = null;
                                await MopupService.Instance.PopAsync(true);
                                CurrentChange = null;
                                await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");

                                await OpenChangePage(false);
                            }
                        }
                    }
                }
            });

            ShowChangeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentPart != null)
                    {
                        await OpenChangePage(true);
                    }
                }
            });

            TakePictureCommand = PictureManager.TakePictureCommand;

            SelectPictureCommand = PictureManager.SelectPictureCommand;

            OpenImageCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentPart != null && CurrentPart.Picture != null)
                    {
                        await OpenModalPage(new VIEW.ImageView(CurrentPart.Picture));
                    }
                }
            });

            TakePartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    IsPartInfo = true;

                    CUST.PopCust pc = new CUST.PopCust();
                    pc.BindingContext = this;

                    await OpenPopup(pc);
                }
            });

            GivePartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    CanMoveBack = false;
                    CUST.PartGiverView pg = new CUST.PartGiverView(CurrentPart);

                    pg.Disappearing += (s, e) =>
                    {
                        this.CanMoveBack = true;
                    };

                    pg.Title = CurrentPart.PartNo;
                    await OpenModalPage(pg);
                }
            });

            OrderPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentPart.StockLevel.HasValue
                    && TakeAmount > 0)
                    {
                        var takeResponse = await partBLL.CreateOrder(new API.Contracts.v1.Requests.Part.CreatePartOrderRequest
                        {
                            PartID = CurrentPart.PartID
                            ,
                            Quantity = TakeAmount.Value
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                        });

                        if (takeResponse.IsResponseWithData(this))
                        {
                            TakeAmount = null;
                            await MopupService.Instance.PopAsync(true);
                            await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");

                            await SetMainData();
                        }
                    }
                }
            });

            CancelTakeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await MopupService.Instance.PopAsync(true);
                }
            });

            DeletePictureCommand = new Command(async (obj) =>
            {
                bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                if (dialogResult)
                {
                    var updateResponse = await partBLL.UpdateImage(new API.Contracts.v1.Requests.Part.UpdatePartImageRequest
                    {
                        Image = null
                        ,
                        PartID = CurrentPart.PartID
                        ,
                        PersonID = MainObjects.Instance.CurrentUser!.PersonID
                    });

                    if (updateResponse.IsResponseWithData(this))
                    {
                        CurrentPart.Picture = null;
                     //   OnPropertyChanged("CurrentItem");
                    //    OnPropertyChanged("CurrentPart");
                    }
                }
            });
        }

        private async Task OpenChangePage(bool isOpenWindow)
        {
            PartChangeList = null;

            var changeResponse = await partBLL.GetChange(new API.Contracts.v1.Requests.Part.GetPartChangeRequest
            {
                Lang = MainObjects.Instance.Lang
                ,
                PartID = CurrentPart.PartID
                ,
                PersonID = MainObjects.Instance.CurrentUser!.PersonID
                ,
                OnlyTaken = false
            });

            if (changeResponse.IsResponseWithData(this))
            {
                PartChangeList = changeResponse.Data!
                .OrderByDescending(tt => tt.AddDate)
                .ToList();

                if (isOpenWindow)
                {
                    var view = new VIEW.PartChangeView();
                    view.Title = CurrentPart.PartNo;
                    view.BindingContext = this;

                    await OpenModalPage(SConsts.GetBaseNavigationPage(view));
                }
            }
        }

        private async void PictureManager_OnTakePicture(object? sender, (WOFile info, object commandParameter) e)
        {
            if (e.info != null)
            {
                IsBusy = true;

                var updateResponse = await partBLL.UpdateImage(new API.Contracts.v1.Requests.Part.UpdatePartImageRequest
                {
                    Image = e.info.Data
                        ,
                    PartID = CurrentPart.PartID
                        ,
                    PersonID = MainObjects.Instance.CurrentUser!.PersonID
                });

                if (updateResponse.IsValid)
                {
                    CurrentPart.Picture = e.info.Data;
                }

                IsBusy = false;
            }
        }

        private async void PictureManager_OnSelectPicture(object? sender, (WOFile info, object commandParameter) e)
        {
            if (e.info != null)
            {
                IsBusy = true;

                var updateResponse = await partBLL.UpdateImage(new API.Contracts.v1.Requests.Part.UpdatePartImageRequest
                {
                    Image = e.info.Data
                        ,
                    PartID = CurrentPart.PartID
                        ,
                    PersonID = MainObjects.Instance.CurrentUser!.PersonID
                });

                if (updateResponse.IsResponseWithData(this))
                {
                    CurrentPart.Picture = e.info.Data;
                }

                IsBusy = false;
            }
        }

        #endregion

        #region METHOD SetBasePartID

        public void SetBasePartID(int partID)
        {
            this.partID = partID;
        }

        #endregion

        #region METHOD SetMainData

        public async Task SetMainData()
        {
            IsBusy = true;

            var partResponse = await partBLL.GetDetail(new API.Contracts.v1.Requests.Part.GetPartDetailRequest
            {
                PartID = partID
                ,
                Lang = MainObjects.Instance.Lang
            });

            if (partResponse.IsResponseWithData(this))
            {
                CurrentPart = partResponse.Data!.First();

                if ((CurrentPart.LockedOrders == false || !CurrentPart.LockedOrders)
                    && (!CurrentPart.Obsolete || !CurrentPart.Obsolete)
                    && CurrentPart.StockLevel.HasValue && CurrentPart.StockLevel.Value > 0)
                {
                    IsGivePart = MainObjects.Instance.CurrentUser!.GetUserRightResponse.PART_Give;
                }

                var tempList = new List<GetDeviceDetailsResponse>();

                SetSingleData(nameof(CurrentPart.Warehouse), tempList);
                SetSingleData(nameof(CurrentPart.Locations), tempList);
                SetSingleData(nameof(CurrentPart.GeneralCategory), tempList);
                SetSingleData(nameof(CurrentPart.Category), tempList);
                SetSingleData(nameof(CurrentPart.Description), tempList);
                SetSingleData(nameof(CurrentPart.Remarks), tempList);
                SetSingleData(nameof(CurrentPart.Producent), tempList);
                SetSingleData(nameof(CurrentPart.ManufacturerCode), tempList);
                SetSingleData(nameof(CurrentPart.StockMinTarget), tempList);
                SetSingleData(nameof(CurrentPart.StockMaxTarget), tempList);
                SetSingleData(nameof(CurrentPart.CurrentNeeds), tempList);
                SetSingleData(nameof(CurrentPart.OrderedQuantity), tempList);
                SetSingleData(nameof(CurrentPart.DefaultSupplier), tempList);
                SetSingleData(nameof(CurrentPart.LockedOrders), tempList);
                SetSingleData(nameof(CurrentPart.Obsolete), tempList);
                SetSingleData(nameof(CurrentPart.IsExchangeable), tempList);

                PartDetailList = tempList;
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD SetSingleData

        private void SetSingleData(string name, List<GetDeviceDetailsResponse> list)
        {
            PropertyInfo? pi = CurrentPart!.GetType().GetProperty(name);

            if (pi != null)
            {
                GetDeviceDetailsResponse vd = new GetDeviceDetailsResponse();

                vd.Property_Name = name;

                var val = pi.GetValue(CurrentPart);

                if (val is not null)
                {
                    vd.Property_Value = val.ToString() ?? string.Empty;

                    Type? t = Nullable.GetUnderlyingType(pi.PropertyType);

                    if (t != null)
                    {
                        vd.Property_Type = GetTypeName(t);
                    }
                    else
                    {
                        vd.Property_Type = GetTypeName(pi.PropertyType);
                    }

                    list.Add(vd);
                }
            }
        }

        #endregion

        #region METHOD GetTypeName

        private string GetTypeName(Type type)
        {
            if (type.Name.ToLower() == "string")
            {
                return "String";
            }
            else if (type.Name.ToLower() == "decimal")
            {
                return "Decimal";
            }
            else if (type.Name.ToLower() == "int")
            {
                return "Int32";
            }
            else if (type.Name.ToLower() == "long")
            {
                return "Int64";
            }
            else if (type.Name.ToLower() == "datetime")
            {
                return "DateTime";
            }
            else if (type.Name.ToLower() == "boolean")
            {
                return "Boolean";
            }

            return "String";
        }

        #endregion

        #region METHOD IsCurrentWarehouseLocationsExsists

        private bool IsCurrentWarehouseLocationsExsists() =>
            LocationList != null && LocationList.Count > 0;

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanItems = new List<IScanType>();

            if (ViewMode == PartDetailViewMode.ChangeLocation)
            {
                if (IsCurrentWarehouseLocationsExsists())
                {
                    var partLoc = new PartLocScan(LocationList!);
                    partLoc.UIMethod = (obj) =>
                    {
                        if (obj is GetWarehouseLocationResonse loc)
                        {
                            SelectedLocation = loc;
                        }
                    };

                    scanItems.Add(partLoc);
                }
            }

            return scanItems;
        }

        #endregion
    }
}
