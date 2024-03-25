using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.Models.Handlers;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.MODEL;
using CommunityToolkit.Mvvm.Input;
using CMMMobileMaui.UIRefresh;

namespace CMMMobileMaui.VM
{
    public class VMWorkOrderListAll : ViewModelBase, IWOCloser
    {
        #region FIELDS

        private WOModel tempWO;

        private readonly IWOController workOrderBLL;
        private readonly IDeviceController deviceCMMBLL;
        private readonly IActController actController;
        private readonly RefreshTimer refreshTimer = new RefreshTimer(TimeSpan.FromSeconds(10));
        private GetWOsRequestBaseHandler currentWOsRequestHandler;

        #endregion

        #region PROPERTY DeviceFilter

        private string deviceFilter;

        public string DeviceFilter
        {
            get
            {
                return deviceFilter;
            }

            set
            {
                SetProperty(ref deviceFilter, value);
            }
        }

        #endregion

        #region PROPERTY IsPartGive

        public bool IsPartGive
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CanAddNewWO

        private bool canAddNewWO;

        public bool CanAddNewWO
        {
            get => canAddNewWO;
            set => SetProperty(ref canAddNewWO, value);
        }

        #endregion

        #region PROPERTY IsActiveWO

        public bool IsActiveWO
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceID

        private int? deviceID = null;

        public int? DeviceID
        {
            get
            {
                return deviceID;
            }
            set
            {
                deviceID = value;
            }
        }

        #endregion

        #region PROPERTY PersonID

        public short? PersonID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOCat

        public DictBase WOCat
        {
            get;
            set;
        }

        #endregion

        #region PROEPRTY IsPlan

        public bool IsPlan
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOReas

        public DictBase WOReas
        {
            get;
            set;
        }


        #endregion

        #region PROPERTY ItemsList

        public ObservableCollection<WOModel> ItemsList
        {
            get;
            set;
        }

        #endregion

        #region PORPERY WOCatList
        public List<DictBase> WOCatList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOReasList

        public List<DictBase> WOReasList
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

        #region COMMAND ShowItemCommand

        public ICommand ShowItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND TakeItemCommand

        public ICommand TakeItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND CloseWOCommand

        public ICommand CloseWOCommand
        {
            get;
        }

        #endregion

        #region COMMAND LoadNextItemCommand

        public IRelayCommand LoadNextItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND RefreshListCommand

        public IAsyncRelayCommand RefreshListCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveCloseCommand

        public ICommand SaveCloseCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelCloseCommand

        public ICommand CancelCloseCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddNewWOCommand

        public ICommand AddNewWOCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddActivityCommand

        public ICommand AddActivityCommand
        {
            get;
        }

        #endregion

        #region Cstr
        public VMWorkOrderListAll(IWOController workOrderBLL
            , IActController actController
            , IDeviceController deviceCMMBLL)
        {
            this.workOrderBLL = workOrderBLL;
            this.deviceCMMBLL = deviceCMMBLL;
            this.actController = actController;

            LoadNextItemCommand = new RelayCommand(LoadNextItems);
            RefreshListCommand = new AsyncRelayCommand(RefreshList);
           
            CloseWOCommand = new Command(async (obj) =>
            {
                await BaseStepActionMethod<WOModel>(CloseWO, obj);
            });

            ShowItemCommand = new Command(async (obj) =>
            {
                if (!CanClick())
                    return;

                if (obj is WOModel model)
                {
                    await ShowItem(model);
                }
            });

            TakeItemCommand = new Command((obj) => TakeItem(obj));
            ItemsList = new ObservableCollection<WOModel>();

            AddNewWOCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.DeviceSearchView deviceView = new VIEW.DeviceSearchView(true);

                    deviceView.OnDeviceSelected += async (s, e) =>
                    {
                        MainObjects.Instance.CurrentDevice = e;

                        if (Shell.Current.Navigation.ModalStack.Count > 0)
                        {
                            await Shell.Current.Navigation.PopModalAsync();
                        }

                        await Task.Delay(100);

                        await OpenModalPage(new VIEW.WorkOrderSingleView(null));
                    };

                    await OpenModalPage(deviceView);
                }
            });

            CancelCloseCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    MopupService.Instance.PopAsync();
                }
            });

            SaveCloseCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (WOCat != null && WOReas != null)
                    {
                        var closeResponse = await workOrderBLL.Close(
                            new UpdateWOCloseRequest()
                            {
                                WorkOrderID = tempWO.BaseItem.WorkOrderID
                            ,
                                PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                                CategoryID = WOCat.ID
                            ,
                                ReasonID = WOReas.ID
                            });

                        if (closeResponse.IsValid)
                        {
                            await MopupService.Instance.PopAsync();
                            IsBusy = true;
                        }

                        tempWO = null;
                        WOCat = null;
                        WOReas = null;
                    }
                }
            });

            AddActivityCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj != null && obj is WOModel model)
                    {
                        AddActivity(model);
                    }
                }
            });
        }

        #endregion

        #region METHOD AddActivity

        private async void AddActivity(WOModel woModel)
        {
            CUST.WOActivityPop pc = new CUST.WOActivityPop();
            VMWOActivityPop vmWOActivityPop = new VMWOActivityPop();
            vmWOActivityPop.Init(woModel);

            pc.BindingContext = vmWOActivityPop;

            pc.Disappearing += (s, e) =>
            {
                CurrentWO = null;

                if (vmWOActivityPop.GetShouldRefresh())
                {
                    IsBusy = true;
                }
            };

            await OpenPopup(pc);
        }

        #endregion

        #region METHOD SetEditDictList

        private async Task SetEditDictList()
        {
            var woTask = workOrderBLL.Get(new GetByWorkOrderIDRequest
            {
                WorkOrderID = CurrentWO.WorkOrderID
            });

            await Task.WhenAll(woTask);

            if (woTask.Result.IsValid)
            {
                var woDictionary = DictionaryResources.Instance.WODictionaryForEdit.ForDevice(MainObjects.Instance.CurrentDevice);

                WOCatList = woDictionary.WOCategoryList;
                WOReasList = woDictionary.WOReasonList;

                if (WOCatList != null)
                {
                    WOCat = WOCatList.FirstOrDefault(tt => tt.ID == woTask.Result.Data.CategoryID);
                    OnPropertyChanged(nameof(WOCat));
                }

                if (WOReasList != null)
                {
                    WOReas = WOReasList.FirstOrDefault(tt => tt.ID == woTask.Result.Data.ReasonID);
                    OnPropertyChanged(nameof(WOReas));
                }

                OnPropertyChanged(nameof(WOCatList));
                OnPropertyChanged(nameof(WOReasList));
            }
        }

        #endregion

        #region METHOD CloseWO

        private async void CloseWO(WOModel wo)
        {
            tempWO = wo;
            CurrentWO = wo.BaseItem;

            if (wo.BaseItem.WO_Category.ToLower().Equals(CONV.TranslateExtension.GetResourceText("wo_default").ToLower())
                           || wo.BaseItem.WO_Reason.ToLower().Equals(CONV.TranslateExtension.GetResourceText("wo_default").ToLower())
                           || wo.BaseItem.WO_State.ToLower().Equals(CONV.TranslateExtension.GetResourceText("wo_default").ToLower()))
            {
                var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                {
                    MachineID = wo.BaseItem.MachineID
                        ,
                    Lang = MainObjects.Instance.Lang
                });

                if (deviceResponse.IsValid)
                {
                    MainObjects.Instance.CurrentDevice = deviceResponse.Data;

                    if (MainObjects.Instance.CurrentDevice != null)
                    {
                        await SetEditDictList();

                        CUST.WOClosePop pc = new CUST.WOClosePop();

                        pc.Disappearing += (s, e) =>
                        {
                            if (WOReasList != null)
                            {
                                WOReasList = null;
                            }

                            if (WOCatList != null)
                            {
                                WOCatList = null;
                            }

                            CurrentWO = null;
                        };

                        pc.BindingContext = this;

                        await OpenPopup(pc);
                    }
                }
            }
            else
            {
                //if (WOCat != null
                //    && WOReas != null)
                //{
                var closeResponse = await workOrderBLL.Close(new UpdateWOCloseRequest()
                {
                    WorkOrderID = wo.BaseItem.WorkOrderID
                    ,
                    PersonID = MainObjects.Instance.CurrentUser.PersonID
                    ,
                    CategoryID = wo.BaseItem.CategoryID
                    ,
                    ReasonID = wo.BaseItem.ReasonID
                }); ;

                if (closeResponse.IsValid)
                {
                    IsBusy = true;
                }
                //}
            }
        }

        #endregion

        #region METHOD ShowItem

        private async Task ShowItem(WOModel wo)
        {
            wo.IsBusy = true;

            if (IsPartGive)
            {
                SConsts.GetGlobalAction<GetWOsResponse>(SConsts.WO_GIVE).Invoke(wo.BaseItem);
            }
            else
            {
                var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                {
                    MachineID = wo.BaseItem.MachineID
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                if (deviceResponse.IsValid)
                {
                    MainObjects.Instance.CurrentDevice = deviceResponse.Data;
                    await OpenModalPage(new VIEW.WorkOrderSingleView(wo.BaseItem));
                }
            }

            wo.IsBusy = false;        
        }

        #endregion

        #region METHOD TakeItem

        private async void TakeItem(object obj)
        {
            if (CanClick())
            {
                bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                if (dialogResult)
                {
                    if (obj != null
                        && obj is WOModel wo)
                    {
                        var takeResponse = await workOrderBLL.Take(new UpdateWOTakeRequest
                        {
                            PersonID = MainObjects.Instance.CurrentUser.PersonID
                            ,
                            WorkOrderID = wo.BaseItem.WorkOrderID
                        });

                        if (takeResponse.IsValid)
                        {
                            IsBusy = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region METHOD InitRequestType

        internal void InitRequestType(GetWOsRequestBaseHandler getWOsRequest)
        {
            currentWOsRequestHandler = getWOsRequest;
            currentWOsRequestHandler.SetFilterList();
        }

        #endregion

        #region METHOD SetListByFilter

        internal void SetListByFilter(GetWOsRequest e)
        {
            currentWOsRequestHandler.AfterFilterChange(e);
            // await RefreshList();
            IsBusy = true;
        }

        #endregion

        #region METHOD GetListRequestHandler

        public GetWOsRequestBaseHandler GetListRequestHandler() =>
            currentWOsRequestHandler;

        #endregion

        #region METHOD RefreshList

        private List<GetWOsResponse> tempWOsList;

        public async Task RefreshList()
        {
            if (CanClick())
            {
                refreshTimer.Stop();
                ItemsList.Clear();

                currentWOsRequestHandler.SetBaseUnselectableFilter(MainObjects.Instance.CurrentUser!.PersonID, DeviceID, null, MainObjects.Instance.Lang);

                var woListResponse = await workOrderBLL.GetList(currentWOsRequestHandler.GetWOsRequest());

                //CanAddNewWO = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.GetUserRightResponse.WO_Add;

                if (woListResponse.IsResponseWithData(this))
                {
                    tempWOsList = SConsts.GetSortedList(woListResponse.Data!, currentWOsRequestHandler.GetWOsRequest()).ToList();
                    refreshTimer.Start();
                    LoadNextItems();
                }
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD LoadNextItems

        private readonly int incValue = 10;

        private void LoadNextItems()
        {
            if(tempWOsList == null || tempWOsList.Count == 0)
            {
                return;
            }

            if(ItemsList.Count != tempWOsList.Count)
            {
                int skipAmount = ItemsList.Count;

                var items = tempWOsList
                    .Skip(skipAmount)
                    .Take(incValue)
                    .ToList();

                items.ForEach(tt =>
                {
                    var woModel = new WOModel(tt, this);
                    ItemsList.Add(woModel);
                    refreshTimer.AddItemToRefresh(woModel);
                });
            }
        }

        #endregion

        #region METHOD Clear

        public void Clear()
        {
            refreshTimer.Stop();
        }

        #endregion
    }
}
