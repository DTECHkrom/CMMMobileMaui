using System.Collections.ObjectModel;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VM
{
    public class VMPartGiver2 : ScannerViewModelBase
    {
        #region FIELDS

        private readonly WeakEventManager eventWeakManager = new WeakEventManager();

        public event EventHandler<bool> OnCurrentPartChange
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        private IPartController partBLL;

        #endregion

        #region PROPETRY WOName

        private string wOName;

        public string WOName
        {
            get => wOName;
            set => SetProperty(ref wOName, value);
        }

        #endregion

        #region PROPERTY WarehouseList

        public List<PartDictResponse> WarehouseList =>
            DictionaryResources.Instance.WarehouseList;

        #endregion

        #region PROPERTY PartList

        public List<GetPartDetailShortResponse>? PartList
        {
            get;
            set;
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

        #region PROPERTY TakeAmount

        private decimal takeAmount;

        public decimal TakeAmount
        {
            get => takeAmount;
            set => SetProperty(ref takeAmount, value);
        }

        #endregion

        #region PROPERTY CanSelectWarehouse

        public bool CanSelectWarehouse => true;

        #endregion

        #region PROPERTY IsDevice

        private bool isDevice;

        public bool IsDevice
        {
            get => isDevice;
            set => SetProperty(ref isDevice, value);
        }

        #endregion  

        #region PROPERTY CurrentDevice

        private GetDeviceListResponse? _currentDevice;

        public GetDeviceListResponse? CurrentDevice
        {
            get
            {
                return _currentDevice;
            }
            set
            {
                SetProperty(ref _currentDevice, value);

                if (_currentDevice != null)
                {
                    IsDevice = true;
                }
            }
        }

        #endregion

        #region PROPERTY CurrentPart

        private GetPartDetailShortResponse currentPart;

        public GetPartDetailShortResponse CurrentPart
        {
            get => currentPart;
            set
            {
                SetProperty(ref currentPart, value);

                if (currentPart != null)
                {
                    if (currentPart.IsExchangeable)
                    {
                        TakeAmount = 0;
                    }
                }
            }
        }

        #endregion

        #region PROEPRTY CurrentWO

        private GetWOsResponse? _currentWO;
        public GetWOsResponse? CurrentWO
        {
            get
            {
                return _currentWO;
            }
            set
            {
                SetProperty(ref _currentWO, value);

                WOName = string.Empty;

                if (value != null)
                {
                    WOName = $"[{_currentWO.Mod_Date!.Value.ToString("yyyy-MM-dd HH:mm")}]\n[{_currentWO.Mod_Person}]";
                }
            }
        }

        #endregion

        #region PROPERTY TakenPartsList

        public ObservableCollection<PartTake> TakenPartsList
        {
            get;
            set;
        }

        #endregion

        #region COMMAND SelectDeviceCommand

        public ICommand SelectDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectWOCommand

        public ICommand SelectWOCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCommand

        public ICommand SaveCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenSearchPartCommand

        public ICommand OpenSearchPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenScanPartCodeCommand

        public ICommand OpenScanPartCodeCommand
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

        #region COMMAND FilterCommand

        public ICommand FilterCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCurrentPartCommand

        public ICommand SaveCurrentPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelCurrentPartCommand

        public ICommand CancelCurrentPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND RemovePartCommand

        public ICommand RemovePartCommand
        {
            get;
        }

        #endregion

        #region COMMAND ClearCommand

        public ICommand ClearCommand
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

        #region Cstr

        public VMPartGiver2(IPartController partBLL)
        {
            this.partBLL = partBLL;
        //    DeviceName = string.Empty;
            WOName = string.Empty;
            TakeAmount = 1;

            TakenPartsList = new ObservableCollection<PartTake>();

            SConsts.SetGlobalAction<GetWOsResponse>(SConsts.WO_GIVE, (item) =>
            {
                if (item is GetWOsResponse wo)
                {
                    CurrentWO = wo;
                    Shell.Current.Navigation.PopModalAsync();
                }
            });

            SConsts.SetGlobalAction<GetPartDetailShortResponse>(SConsts.PART_SCAN_GIVER, (item) =>
            {
                if (item is GetPartDetailShortResponse part)
                {
                    Shell.Current.Navigation.PopModalAsync();
                    SetTakeAmountForCurrentPart(part);
                }
            });

            ShowDetailsCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetPartDetailShortResponse part)
                    {
                        CurrentPart = part;
                        await OpenModalPage(new VIEW.PartDetailView(CurrentPart.PartID, true));
                    }
                }               
            });

            ClearCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    TakenPartsList.Clear();
                    PartList = null;
                    CurrentDevice = null;
                    TakeAmount = 1;
                //    DeviceName = string.Empty;
                    CurrentWO = null;
                }
            });

            RemovePartCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj != null && obj is PartTake part)
                    {
                        TakenPartsList.Remove(part);
                    }
                }
            });

            SaveCurrentPartCommand = new Command(() =>
            {
                if (CanClick())
                {
                    if (CanSaveCurrentPart())
                    {
                        SaveCurrentPart();
                    }
                }
            });

            CancelCurrentPartCommand = new Command(() =>
            {
                if (CanClick())
                {
                    PartList = null;

                    eventWeakManager.HandleEvent(this, false, nameof(OnCurrentPartChange));
                    TakeAmount = 1;
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
                    if (obj != null
                    && obj is GetPartDetailShortResponse part)
                    {
                        Shell.Current.Navigation.PopModalAsync();
                        SetTakeAmountForCurrentPart(part);
                    }
                }
            });

            OpenSearchPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.PartSearchView vPart = new VIEW.PartSearchView();
                    vPart.SetSearchMode(VMWorkOrderPartMain.PartSearchMode.Giver);

                    // vPart.BindingContext = this;
                    vPart.Disappearing += (s, e) =>
                    {
                        CurrentPart = vPart.ViewModel.CurrentPart;
                        //  PartList.Clear();
                        //   CurrentWar = null;
                        //   PartFilter = string.Empty;

                        if (CurrentPart != null)
                        {
                            SetTakeAmountForCurrentPart(CurrentPart);
                        }
                    };

                    await OpenModalPage(vPart);
                }
            });

            OpenScanPartCodeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.ScanView(ScanType.PartGiver));
                }
            });

            SelectDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.DeviceSearchView vm = new VIEW.DeviceSearchView(true);
                    vm.OnDeviceSelected += Vm_OnDeviceSelected;
                    await OpenModalPage(vm);
                }
            });

            SelectWOCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentDevice != null)
                    {
                        VIEW.WorkOrderListAllView vm = new VIEW.WorkOrderListAllView(false, true, deviceId: CurrentDevice.MachineID);

                        await OpenModalPage(vm);
                    }
                }
            });

            SaveCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (IsDataSet())
                    {
                        var items = TakenPartsList.Where(tt => tt.Amount > tt.OldAmount
                        || (tt.Amount == 0 && !tt.IsExchangeable)).Count();

                        if (items == 0)
                        {
                            bool isOk = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                            if (isOk)
                            {
                                List<SinglePartTake> partTakeList = new List<SinglePartTake>();

                                foreach (var part in TakenPartsList)
                                {
                                    SinglePartTake partTake = new SinglePartTake();

                                    partTake.MachineID = CurrentDevice!.MachineID;

                                    if (CurrentWO != null)
                                    {
                                        partTake.WorkOrderID = CurrentWO.WorkOrderID;
                                    }

                                    partTake.Quantity = part.Amount;
                                    partTake.PartID = part.PartID;
                                    partTake.PersonID = MainObjects.Instance.CurrentUser!.PersonID;
                                    partTake.OtherPersonID = MainObjects.Instance.CurrentUser.PersonID;
                                    partTake.IsExchangeable = part.IsExchangeable;

                                    partTakeList.Add(partTake);
                                }

                                IsBusy = true;

                                var takeResponse = await partBLL.Take(new PartTakeRequest
                                {
                                    PartTakeList = partTakeList
                                });

                                if (takeResponse.IsValid)
                                {
                                    ClearCommand.Execute(null);
                                    await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");
                                }

                                IsBusy = false;
                            }
                        }
                    }
                }
            });
        }

        #endregion

        #region METHOD SaveCurrentPart

        private void SaveCurrentPart()
        {
            eventWeakManager.HandleEvent(this, false, nameof(OnCurrentPartChange));
            // OnCurrentPartChange?.Invoke(this, false);

            PartTake part = new PartTake();
            part.PartID = CurrentPart.PartID;
            part.OldAmount = CurrentPart.StockLevel ?? 0;

            if (CurrentPart.IsExchangeable)
            {
                part.Amount = 0;
                part.IsExchangeable = true;
            }
            else
            {
                part.Amount = TakeAmount;
            }

            part._PartNo = CurrentPart.PartNo;
            part.PersonID = MainObjects.Instance.CurrentUser!.PersonID;
            part.OtherPersonID = MainObjects.Instance.CurrentUser!.PersonID;

            TakenPartsList.Add(part);
            OnPropertyChanged(nameof(TakenPartsList));
            TakeAmount = 1;
        }

        #endregion

        #region METHOD CanSaveCurrentPart

        private bool CanSaveCurrentPart() =>
            CurrentPart != null
            && !CurrentPart.IsExchangeable && TakeAmount > 0
                && CurrentPart.StockLevel.HasValue
                && CurrentPart.StockLevel >= TakeAmount;

        #endregion

        #region METHOD RefreshFilterMethod

        private async void RefreshFilterMethod()
        {
            if (!string.IsNullOrEmpty(PartFilter) && CurrentWar != null)
            {
                IsBusy = true;

                var partsResponse = await partBLL.GetDetailShort(new GetPartDetailShortRequest
                {
                    Filter = PartFilter.ToLower()
                    ,
                    WarID = CurrentWar.ID
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                if (partsResponse.IsResponseWithData(this))
                {
                    PartList = partsResponse.Data!.ToList();
                }

                IsBusy = false;
            }
        }

        #endregion

        //#region METHOD SetMainData

        //private async void SetMainData()
        //{
        //    var warsResponse = await partBLL.GetWars(new CMMMobileMaui.API.Contracts.v1.Requests.GetDictRequest
        //    {
        //        PersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID
        //        , Lang = CMMMobileMaui.API.MainObjects.Instance.Lang
        //    });

        //    if(warsResponse.IsValid)
        //    {
        //        WarehouseList = warsResponse.Data.ToList();
        //        OnPropertyChanged(nameof(WarehouseList));
        //    }
        //}

        //#endregion

        #region METHOD IsDataSet
        private bool IsDataSet() =>
            CurrentDevice != null
            && TakenPartsList.Count > 0;

        #endregion

        #region EVENT Vm_OnDeviceSelected

        private void Vm_OnDeviceSelected(object? sender, GetDeviceListResponse e)
        {
            MainObjects.Instance.CurrentDevice = CurrentDevice = e;
            CurrentWO = null;
        }

        #endregion

        #region METHOD SetTakeAmountForCurrentPart

        private void SetTakeAmountForCurrentPart(GetPartDetailShortResponse part)
        {
            var oldPart = TakenPartsList.FirstOrDefault(tt => tt.PartID == part.PartID);

            if (oldPart == null)
            {
                CurrentPart = part;

                if (!part.IsExchangeable)
                {
                    eventWeakManager.HandleEvent(this, true, nameof(OnCurrentPartChange));
                    OnPropertyChanged(nameof(CurrentPart));
                }
                else
                {
                    SaveCurrentPart();
                }
            }
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
                    SetTakeAmountForCurrentPart(part);
                }
            };

            scanTypes.Add(partScan);

            return scanTypes;
        }

        #endregion
    }
}
