using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts.v1.Requests.Act;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Contracts.v1.Responses.Plan;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMMMobileMaui.VM
{
    public class VMWorkOrderSingle : ScannerViewModelBase, IWOCloser
    {
        #region FIELDS

        private readonly IWOController workOrderBLL;
        private readonly IDeviceController deviceCMMBLL;
        private readonly IActController woActivityBLL;
        private readonly IPlanController planController;
        private readonly IActPerController actPerController;

        private readonly AsyncAwaitBestPractices.WeakEventManager eventWeakManager = new AsyncAwaitBestPractices.WeakEventManager();

        public event EventHandler<bool> OnWOViewChange
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        #endregion

        #region PROPERTY ActsList

        private ObservableCollection<GetWOActsResponse>? actsList;

        public ObservableCollection<GetWOActsResponse>? ActsList
        {
            get => actsList;
            set => SetProperty(ref actsList, value);
        }

        #endregion

        #region PROPERTY IsDeviceInfo

        public bool IsDeviceInfo
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentWO

        private GetWOsResponse? _currentWO = null;

        public GetWOsResponse? CurrentWO
        {
            get
            {
                return _currentWO;
            }
            set
            {
                SetProperty(ref _currentWO, value);

                if (_currentWO == null
                    || _currentWO.WorkOrderID == 0)
                {
                    IsEdit = true;
                    CurrentDevice = MainObjects.Instance.CurrentDevice!;
                    SetAddDictList();
                }
                else
                {
                    IsClose = _currentWO.Close_Date.HasValue;
                    IsAssignPerson = !string.IsNullOrEmpty(_currentWO.Assigned_Person);
                }
            }
        }

        #endregion

        #region PROPERTY WOCatList

        public List<DictBase>? WOCatList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOLvlList

        public List<DictBase>? WOLvlList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOReasList

        public List<DictBase>? WOReasList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOStateList

        public List<DictBase>? WOStateList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DepList

        public List<DictBase>? DepList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsPlanList

        private bool _isPlanList;

        public bool IsPlanList
        {
            get
            {
                return _isPlanList;
            }

            set
            {
                SetProperty(ref _isPlanList, value);
            }
        }

        #endregion

        #region PROPERTY IsWorkOrderVisible

        private bool isWorkOrderVisible;

        public bool IsWorkOrderVisible
        {
            get => isWorkOrderVisible;
            set => SetProperty(ref isWorkOrderVisible, value);
        }

        #endregion

        #region PROPERTY IsActivityVisible

        private bool isActivityVisible;

        public bool IsActivityVisible
        {
            get => isActivityVisible;
            set => SetProperty(ref isActivityVisible, value);
        }

        #endregion

        #region PROPERTY WOPlanList


        private ObservableCollection<GetWOPlanActsResponse>? wOPlanList;
        public ObservableCollection<GetWOPlanActsResponse>? WOPlanList
        {
            get => wOPlanList;
            set => SetProperty(ref wOPlanList, value);
        }

        #endregion

        #region PROPERTY PersonList

        public List<GetAllUsersResponse>? PersonList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsEdit

        private bool isEdit;
        public bool IsEdit
        {
            get => isEdit;
            set => SetProperty(ref isEdit, value);
        }

        #endregion

        #region PROPERTY IsClose

        private bool isClose;
        public bool IsClose
        {
            get => isClose;
            set => SetProperty(ref isClose, value);
        }

        #endregion

        #region PROPERTY IsDescEdit

        public bool IsDescEdit
        {
            get
            {
                return IsAddNewWO() == true ? true : MainObjects.Instance.CurrentUser!.GetUserRightResponse.WO_Edit_Description;
            }
        }

        #endregion

        #region PROPERTY IsCanAddWO

        public bool IsCanAddWO
        {
            get
            {
                return MainObjects.Instance.CurrentUser!.GetUserRightResponse.WO_Add;
            }
        }

        #endregion

        #region PROPERTY IsCanEditWO

        public bool IsCanEditWO
        {
            get
            {
                return MainObjects.Instance.CurrentUser!.GetUserRightResponse.WO_Edit;
            }
        }

        #endregion

        #region PROPERTY IsCanAddAct

        public bool IsCanAddAct
        {
            get
            {
                return MainObjects.Instance.CurrentUser!.GetUserRightResponse.ACT_Add;
            }
        }

        #endregion

        #region PROPERTY IsCanAddPart

        public bool IsCanAddPart
        {
            get
            {
                return MainObjects.Instance.CurrentUser!.GetUserRightResponse.PART_WO_Take;
            }
        }

        #endregion

        #region PROPERTY IsCanAssignPerson

        public bool IsCanAssignPerson
        {
            get
            {
                return MainObjects.Instance.CurrentUser!.GetUserRightResponse.WO_SET_AssignedPerson;
            }
        }

        #endregion

        #region PROPERTY IsAssignPerson

        private bool isAssignPerson;

        public bool IsAssignPerson
        {
            get => isAssignPerson;
            set => SetProperty(ref isAssignPerson, value);
        }

        #endregion

        #region PROPERTY CurrentDevice

        private GetDeviceListResponse? currentDevice;

        public GetDeviceListResponse? CurrentDevice
        {
            get => currentDevice;
            set => SetProperty(ref currentDevice, value);
        }

        #endregion

        #region PROPERTY Description

        private string? description;

        public string? Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        #endregion

        #region PROPERTY Location

        private string? location;

        public string? Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        #endregion

        #region PROPERTY StartDate
        public DateTime? StartDate
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY EndDate
        public DateTime? EndDate
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOCat

        public DictBase? WOCat
        {
            get;
            set;
        }


        #endregion

        #region PROPERTY WOLvl

        public DictBase? WOLvl
        {
            get;
            set;
        }


        #endregion

        #region PROPERTY WOState

        public DictBase? WOState
        {
            get;
            set;
        }


        #endregion

        #region PROPERTY WOReas

        public DictBase? WOReas
        {
            get;
            set;
        }


        #endregion

        #region PROPERTY Dep

        public DictBase? Dep
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY AssignedPerson

        private GetAllUsersResponse? assignedPerson;

        public GetAllUsersResponse? AssignedPerson
        {
            get => assignedPerson;
            set => SetProperty(ref assignedPerson, value);
        }

        #endregion

        #region PROPERTY IsActivityAdd

        private bool isActivityAdd;

        public bool IsActivityAdd
        {
            get => isActivityAdd;
            set
            {
                SetProperty(ref isActivityAdd, value);
            }
        }

        #endregion

        #region PROPERTY CatList

        private List<DictBase>? catActList;

        public List<DictBase>? CatActList
        {
            get => catActList;
            set => SetProperty(ref catActList, value);
        }

        #endregion

        #region PROPERTY CatAct

        private DictBase? catAct;

        public DictBase? CatAct
        {
            get => catAct;
            set
            {
                catAct = value;

                IsActDepList = false;

                if (catAct != null)
                {
                    if (catAct.ID == SConsts.ACT_CAT_OTHER_DEP)
                    {
                        IsActDepList = true;
                    }
                }
            }
        }

        #endregion

        #region PROPERTY IsActDepList

        private bool isActDepList;
        public bool IsActDepList
        {
            get => isActDepList;
            set => SetProperty(ref isActDepList, value);
        }

        #endregion

        #region PROPERTY ActTemp

        public CreateWOActivityRequest? ActTemp
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CanClose

        private bool canClose;
        public bool CanClose
        {
            get => canClose;
            set => SetProperty(ref canClose, value);
        }

        #endregion

        #region PROPERTY CanTake

        private bool canTake;
        public bool CanTake
        {
            get => canTake;
            set => SetProperty(ref canTake, value);
        }

        #endregion

        #region PROPERTY CanEdit

        private bool canEdit;
        public bool CanEdit
        {
            get => canEdit;
            set => SetProperty(ref canEdit, value);
        }

        #endregion

        #region PROPERTY IsTaken

        private bool isTaken;

        public bool IsTaken
        {
            get => isTaken;
            set => SetProperty(ref isTaken, value);
        }

        #endregion

        #region PROPERTY IsCloseWOOpen

        private bool isCloseWOOpen;

        public bool IsCloseWOOpen
        {
            get => isCloseWOOpen;
            set => SetProperty(ref isCloseWOOpen, value);
        }

        #endregion

        #region COMMAND AddUserAsAssignedCommand

        public ICommand AddUserAsAssignedCommand
        {
            get;
        }

        #endregion

        #region COMMAND EditItemCommand

        public ICommand EditItemCommand
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

        #region COMMAND CancelCommand

        public ICommand CancelCommand
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

        #region COMMAND SwipeLeftCommand

        public ICommand SwipeLeftCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddItemCommand

        public ICommand AddItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddPersonCommand

        public ICommand AddPersonCommand
        {
            get;
        }

        #endregion

        //#region COMMAND RefreshListCommand

        //public ICommand RefreshListCommand
        //{
        //    get;
        //}

        //#endregion

        #region COMMAND SaveItemCommand

        public ICommand SaveItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelCommand

        public ICommand CancelItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenPartCommand

        public ICommand OpenPartCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenPictureCommand

        public ICommand OpenPictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowListCommand

        public ICommand ShowListCommand
        {
            get;
        }

        #endregion

        #region COMMAND DoPlanItemCommand

        public ICommand DoPlanItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND SaveActivityCommand

        public ICommand SaveActivityCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelActivityCommand

        public ICommand CancelActivityCommand
        {
            get;
            set;
        }

        #endregion

        #region COMMAND TakeCommand

        public ICommand TakeCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowDeviceInfoCommand

        public ICommand ShowDeviceInfoCommand
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

        //#region COMMAND LongPressCommand

        //public ICommand LongPressCommand
        //{
        //    get;
        //}

        //#endregion

        #region COMMAND ShowWorkOrderCommand

        public ICommand ShowWorkOrderCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowActivitiesCommand

        public ICommand ShowActivitiesCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMWorkOrderSingle(IWOController workOrderBLL
            , IDeviceController deviceCMMBLL
            , IActPerController actPerController
            , IPlanController planController
            , IActController woActivityBLL)
        {
            this.workOrderBLL = workOrderBLL;
            this.deviceCMMBLL = deviceCMMBLL;
            this.woActivityBLL = woActivityBLL;
            this.actPerController = actPerController;
            this.planController = planController;

            // TempWO = new CreateWORequest();
            IsActivityAdd = false;
            IsWorkOrderVisible = true;
            IsDeviceInfo = true;

            AddUserAsAssignedCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    var personToAssign = PersonList?.FirstOrDefault(tt => tt.PersonID == MainObjects.Instance.CurrentUser!.PersonID);

                    if (personToAssign != null)
                    {
                        AssignedPerson = personToAssign;
                    }
                }
            });

            ShowActivitiesCommand = new Command((obj) =>
            {
                IsWorkOrderVisible = false;
                IsActivityVisible = true;
            });

            ShowWorkOrderCommand = new Command((obj) =>
            {
                IsWorkOrderVisible = true;
                IsActivityVisible = false;
            });

            CancelCloseCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    //ScanManager.FillWithScanType(this);
                    MopupService.Instance.PopAsync();
                }
            });

            SaveCloseCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (WOCat != null && WOReas != null)
                    {
                        var closeResponse = await this.workOrderBLL.Close(new UpdateWOCloseRequest()
                        {
                            WorkOrderID = CurrentWO!.WorkOrderID
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

                            //var wosResponse = await this.workOrderBLL.GetList(new GetWOsRequest()
                            //{
                            //    WorkOrderID = CurrentWO.WorkOrderID
                            //,
                            //    DeviceID = CurrentWO.MachineID
                            //,
                            //    IsPlan = IsPlanList
                            //    , Lang = CMMMobileMaui.API.MainObjects.Instance.Lang

                            //});

                            //      if (wosResponse.IsValid)
                            //    {
                            //    CurrentWO = wosResponse.Data.FirstOrDefault();

                            SConsts.GetGlobalAction(SConsts.DEV_WO_ADD)?.Invoke();

                            // MessagingCenter.Send<string>(string.Empty, COMMON.SConsts.DEV_WO_ADD);
                            await Shell.Current.Navigation.PopModalAsync();
                            //      }

                            //  SetMainData();
                        }
                    }
                }
            });

            ShowDeviceInfoCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    var deviceResponse = await this.deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                    {
                        MachineID = CurrentWO!.MachineID
                        ,
                        Lang = MainObjects.Instance.Lang
                    });

                    if (deviceResponse.IsResponseWithData(this))
                    {
                        await OpenModalPage(new VIEW.DeviceView(deviceResponse.Data!, false));
                    }
                }
            });

            DoPlanItemCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null
                    && obj is GetWOPlanActsResponse org)
                    {
                        if (!org._WorkLoad.HasValue
                        || org._WorkLoad.Value <= 0)
                            return;

                        var createResponse = await this.planController.CreateAct(new API.Contracts.v1.Requests.Plan.CreatePlanActivityRequest
                        {
                            CategoryID = org.WOACategoryID,
                            Description = string.IsNullOrEmpty(org._Description) ? "OK" : org._Description,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID,
                            PlanTaskActivityID = org.PlanTaskActivityID,
                            WorkLoad = org._WorkLoad.Value,
                            WorkOrderID = CurrentWO!.WorkOrderID
                        });

                        if (createResponse.IsValid)
                        {
                            var plansResponse = await this.planController.GetActs(new API.Contracts.v1.Requests.Plan.GetWOPlanActsRequest
                            {
                                WoID = this.CurrentWO.WorkOrderID
                                ,
                                Lang = MainObjects.Instance.Lang
                            });

                            if (plansResponse.IsResponseWithData(this))
                            {
                                SetMainWOPlansList(plansResponse.Data!);
                            }
                        }
                    }
                }
            });

            SaveCommand = new Command(() =>
            {
                if (CanClick())
                {
                    Save();
                }
            });

            CancelCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    if (CurrentWO is null)
                        return;

                    if (CurrentWO.WorkOrderID == 0)
                    {
                        await Shell.Current.Navigation.PopModalAsync();
                    }
                    else
                    {
                        IsEdit = false;
                        SetToobarView();
                    }

                    eventWeakManager.RaiseEvent(this, false, nameof(OnWOViewChange));
                }
            });

            EditItemCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    IsEdit = !IsEdit;

                    if (IsEdit)
                    {
                        await SetEditDictList(true);
                    }
                }
            });

            SwipeLeftCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (!IsEdit)
                    {
                        if (CurrentWO != null)
                        {
                            await OpenModalPage(new VIEW.WorkOrderHistoryListView(CurrentWO));
                        }
                    }
                }
            });

            ShowListCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    var view = new VIEW.WorkOrderPlanMainView();
                    view.BindingContext = this;
                    await OpenModalPage(SConsts.GetBaseNavigationPage(view));
                }
            });

            OpenPartCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.WorkOrderPartMainView(this.CurrentWO!.WorkOrderID));
                }
            });

            OpenPictureCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.PictureListView(new COMMON.PictureOperation.DBWOPictureOperation(new MODEL.WOPicModel(CurrentWO!), CurrentWO!)));
                }
            });

            CloseCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    var isOk = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_wo_close"));

                    if (isOk)
                    {
                        if (IsPlanList)
                        {
                            if (WOPlanList?.Count(tt => !tt.WorkOrderActivityID.HasValue) != 0)
                            {
                                await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Error, CONV.TranslateExtension.GetResourceText("wo_err_close"));
                                return;
                            }
                        }

                        if (CurrentWO!.WO_Category.ToLower().Equals(CONV.TranslateExtension.GetResourceText("wo_default").ToLower())
                            || CurrentWO.WO_Reason.ToLower().Equals(CONV.TranslateExtension.GetResourceText("wo_default").ToLower())
                            || CurrentWO.WO_State.ToLower().Equals(CONV.TranslateExtension.GetResourceText("wo_default").ToLower()))
                        {

                            var deviceResponse = await this.deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                            {
                                MachineID = CurrentWO.MachineID
                        ,
                                Lang = MainObjects.Instance.Lang
                            });


                            if (deviceResponse.IsResponseWithData(this))
                            {
                                MainObjects.Instance.CurrentDevice = deviceResponse.Data;

                                await SetEditDictList(false);

                                CUST.WOClosePop pc = new CUST.WOClosePop();

                                IsCloseWOOpen = true;
                                CanMoveBack = false;

                                pc.Disappearing += (s, e) =>
                                {
                                    IsCloseWOOpen = false;
                                    CanMoveBack = true;
                                };

                                pc.BindingContext = this;
                                await OpenPopup(pc);
                            }
                        }
                        else
                        {
                            var takeResponse = await this.workOrderBLL.Close(new UpdateWOCloseRequest()
                            {
                                WorkOrderID = CurrentWO.WorkOrderID
                                ,
                                PersonID = MainObjects.Instance.CurrentUser!.PersonID
                                ,
                                CategoryID = CurrentWO.CategoryID
                                ,
                                ReasonID = CurrentWO.ReasonID
                            }); ;

                            if (takeResponse.IsResponseWithData(this))
                            {
                                SConsts.GetGlobalAction(SConsts.DEV_WO_ADD)?.Invoke();

                                // MessagingCenter.Send<string>(string.Empty, COMMON.SConsts.DEV_WO_ADD);
                                await Shell.Current.Navigation.PopModalAsync();
                            }
                        }
                    }
                }
            });

            AddItemCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    ActTemp = new CreateWOActivityRequest();

                    var userWorkLoad = await GetUserWorkLoadTime();
                    ActTemp.WorkLoad = userWorkLoad;
                    OnPropertyChanged(nameof(ActTemp));

                    IsActivityAdd = true;
                    SetActMainData();
                }
            });

            AddPersonCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetWOActsResponse woActs)
                    {
                        var userWorkLoad = await GetUserWorkLoadTime();
                        woActs._WorkLoad = userWorkLoad;
                        woActs._IsAddPerson = true;

                       // OnPropertyChanged(nameof(ActsList));
                    }
                }
            });

            SaveItemCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is GetWOActsResponse actPer)
                    {
                        var createResponse = await this.actPerController.Create(new API.Contracts.v1.Requests.ActPer.CreateWOActivityPersonRequest
                        {
                            ActivityID = actPer.ActivityID
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            Work_Load = actPer._WorkLoad
                        });

                        if (createResponse.IsResponseWithData(this))
                        {
                            SetMainData();
                        }
                    }
                }
            });

            CancelItemCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj != null)
                    {
                        ((GetWOActsResponse)obj)._IsAddPerson = false;
                       // OnPropertyChanged(nameof(ActsList));
                    }
                }
            });

            SaveActivityCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (ActTemp is null)
                        return;

                    if (CatAct != null
                    && !string.IsNullOrEmpty(ActTemp.Description)
                    && ActTemp.WorkLoad >= 0)
                    {
                        if (IsActDepList)
                        {
                            if (Dep == null)
                                return;
                        }

                        var createResponse = await this.woActivityBLL.Create(new CreateWOActivityRequest
                        {
                            CategoryID = CatAct.ID
                            ,
                            Description = ActTemp.Description
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            WorkLoad = ActTemp.WorkLoad
                            ,
                            WorkOrderID = CurrentWO!.WorkOrderID
                            ,
                            DepartmentID = Dep == null ? null : (int?)Dep.ID
                        });

                        if (createResponse.IsResponseWithData(this))
                        {
                            ActTemp = null;
                            CatAct = null;
                            Dep = null;

                            var woResponse = await this.workOrderBLL.GetList(new GetWOsRequest()
                            {
                                WorkOrderID = CurrentWO.WorkOrderID,
                                DeviceID = CurrentWO.MachineID,
                                IsPlan = IsPlanList,
                                Lang = MainObjects.Instance.Lang
                            });

                            if (woResponse.IsResponseWithData(this))
                            {
                                CurrentWO = woResponse.Data!.FirstOrDefault();
                            }

                            IsActivityAdd = false;
                            SetMainData();
                        }
                    }
                }
            });

            CancelActivityCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    IsActivityAdd = false;
                    ActTemp = null;
                    CatAct = null;
                }
            });

            TakeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentWO != null)
                    {
                        var takeResponse = await this.workOrderBLL.Take(new UpdateWOTakeRequest
                        {
                            WorkOrderID = CurrentWO.WorkOrderID
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                        });

                        if (takeResponse.IsValid)
                        {
                            var woResponse = await this.workOrderBLL.GetList(new GetWOsRequest()
                            {
                                WorkOrderID = CurrentWO.WorkOrderID,
                                DeviceID = CurrentWO.MachineID,
                                IsPlan = IsPlanList,
                                Lang = MainObjects.Instance.Lang
                            });

                            if (woResponse.IsResponseWithData(this))
                            {
                                CurrentWO = woResponse.Data!.FirstOrDefault();
                            }

                            SetMainData();
                        }
                    }
                }
            });
        }

        #endregion

        #region METHOD GetUserWorkLoadTime

        private async Task<decimal> GetUserWorkLoadTime()
        {
            var takeDate = await workOrderBLL.GetWOPersonTake(new GetWOPersonTakeRequest
            {
                PersonID = MainObjects.Instance.CurrentUser!.PersonID,
                WorkOrderID = CurrentWO!.WorkOrderID
            });

            if (takeDate != null && takeDate.IsValid)
            {
                if (takeDate.Data.HasValue)
                {
                    var value = (decimal)(Math.Round(TimeSpan.FromTicks(DateTime.Now.Ticks - takeDate.Data.Value.Ticks).TotalHours, 2));

                    if (value > 10)
                    {
                        value = 10;
                    }

                    return value;
                }
            }

            return 0;
        }

        #endregion

        #region METHOD SetActMainData
        private void SetActMainData()
        {
            //var actDictResponse = await woActivityBLL.GetDict(new GetActDictRequest
            //{
            //    DeviceCategoryID = CurrentDevice.CategoryID,
            //    Lang = MainObjects.Instance.Lang,
            //    PersonID = MainObjects.Instance.CurrentUser.PersonID
            //});

            //if (actDictResponse.IsValid)
            //{
            //    CatActList = actDictResponse.Data.Where(tt => tt.ListType == 1)
            //        .Cast<DictBase>()
            //        .OrderBy(tt => tt.Name)
            //        .ToList();
            //}

            if (CatActList == null)
            {

                var tempActList = DictionaryResources.Instance.GetWOActDictListFor(MainObjects.Instance.CurrentDevice!.CategoryID);

                CatActList = tempActList.Cast<DictBase>().ToList();
            }


            SetAddDictList();
        }

        #endregion

        #region METHOD SetToobarView

        private void SetToobarView()
        {
            if (_currentWO!.WorkOrderID != 0)
            {
                CanClose = false;
                CanEdit = false;
                CanTake = false;
                IsTaken = false;

                if (!IsClose && ActsList != null && ActsList.Count > 0)
                {
                    CanClose = true;
                }

                if (!IsClose)
                {
                    CanEdit = IsCanEditWO;
                }

                if (_currentWO._TakePersonsList != null)
                {
                    if (!_currentWO._TakePersonsList.Contains(MainObjects.Instance.CurrentUser!.PersonID))
                    {
                        if (!IsClose)
                        {
                            CanTake = true;
                        }
                    }
                    else
                    {
                        IsTaken = true;
                    }
                }
            }
        }

        #endregion

        #region METHOD SetAddDictList

        private void SetAddDictList()
        {
            if (MainObjects.Instance.CurrentDevice != null)
            {
                PersonList = DictionaryResources.Instance.PersonList;

                var woDictionary = DictionaryResources.Instance.WODictionaryForAdd.ForDevice(CurrentDevice!);

                WOStateList = woDictionary.WOStateList;
                WOCatList = woDictionary.WOCategoryList;
                WOLvlList = woDictionary.WOLevelList;
                WOReasList = woDictionary.WOReasonList;
                DepList = woDictionary.WODepartmentList;

                Location = MainObjects.Instance.CurrentDevice.Location;

            }
        }

        #endregion

        #region METHOD SetEditDictList

        private async Task SetEditDictList(bool isChangeView)
        {
            IsBusy = true;

            if (MainObjects.Instance.CurrentDevice != null)
            {
                PersonList = DictionaryResources.Instance.PersonList;

                var woDictionary = DictionaryResources.Instance.WODictionaryForEdit.ForDevice(CurrentDevice!);

                WOStateList = woDictionary.WOStateList;
                WOCatList = woDictionary.WOCategoryList;
                WOLvlList = woDictionary.WOLevelList;
                WOReasList = woDictionary.WOReasonList;
                DepList = woDictionary.WODepartmentList;

                var woResponse = await workOrderBLL.Get(new GetByWorkOrderIDRequest
                {
                    WorkOrderID = _currentWO!.WorkOrderID
                });

                var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                {
                    MachineID = CurrentWO!.MachineID
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                if (deviceResponse.IsResponseWithData(this))
                {
                    CurrentDevice = MainObjects.Instance.CurrentDevice = deviceResponse.Data!;
                }

                Location = MainObjects.Instance.CurrentDevice.Location;


                if (woResponse.IsResponseWithData(this))
                {
                    var TempWO = woResponse.Data!;

                    if (PersonList != null)
                    {
                        if (IsAssignPerson)
                        {
                            AssignedPerson = PersonList.FirstOrDefault(tt => tt.PersonID == TempWO.AssignedPersonID);
                        }
                    }

                    if (TempWO != null)
                    {
                        WOCat = WOCatList.FirstOrDefault(tt => tt.ID == TempWO.CategoryID);
                        WOState = WOStateList.FirstOrDefault(tt => tt.ID == TempWO.StateID);
                        WOLvl = WOLvlList.FirstOrDefault(tt => tt.ID == TempWO.LevelID);
                        WOReas = WOReasList.FirstOrDefault(tt => tt.ID == TempWO.ReasonID);
                        Dep = DepList.FirstOrDefault(tt => tt.ID == TempWO.DepartmentID);
                    }
                }
                //  }

                if (isChangeView)
                {
                    eventWeakManager.RaiseEvent(this, true, nameof(OnWOViewChange));
                }
            }

            //    ScanManager.FillWithScanType(this);

            IsBusy = false;
        }

        #endregion

        #region METHOD Save

        private void Save()
        {
            if (IsAddNewWO())
            {
                SaveAddWO();
            }
            else
            {
                SaveEditWO();
            }
        }

        #endregion

        #region METHOD IsAddNewWO

        private bool IsAddNewWO() =>
            CurrentWO == null || CurrentWO.WorkOrderID == 0;

        #endregion

        #region METHOD SaveAddWO

        private async void SaveAddWO()
        {
            CreateWORequest createWO = new CreateWORequest();

            if (string.IsNullOrEmpty(Description))
            {
                return;
            }

            if (WOCatList!.Count > 0)
            {
                if (WOCat == null)
                {
                    return;
                }

                createWO.CategoryID = WOCat.ID;
            }

            if (WOLvl == null)
            {
                return;
            }

            if (AssignedPerson != null)
            {
                createWO.AssignedPersonID = AssignedPerson.PersonID;
            }
            else
            {
                createWO.AssignedPersonID = null;
            }

            if (CurrentDevice!.LocationRequired.HasValue
                && CurrentDevice.LocationRequired.Value)
            {
                if (string.IsNullOrEmpty(Location))
                {
                    return;
                }
            }

            createWO.Location = Location ?? string.Empty;

            createWO.LevelID = WOLvl.ID;

            if (WOReasList!.Count > 0)
            {
                if (WOReas == null)
                {
                    return;
                }

                createWO.ReasonID = WOReas.ID;
            }

            createWO.Start_Date = StartDate;
            createWO.End_Date = EndDate;
            createWO.Description = Description;

            createWO.PersonID = MainObjects.Instance.CurrentUser!.PersonID;
            createWO.MachineID = MainObjects.Instance.CurrentDevice!.MachineID;

            if (Dep != null)
            {
                createWO.DepartmentID = Dep.ID;
            }

            var createRespone = await workOrderBLL.Create(createWO);

            if (createRespone.IsResponseWithData(this))
            {
                var woResponse = await workOrderBLL.GetList(new GetWOsRequest()
                {
                    WorkOrderID = createRespone.Data!.WorkOrderID
                    ,
                    DeviceID = createWO.MachineID
                    ,
                    IsPlan = IsPlanList
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                if (woResponse.IsResponseWithData(this))
                {
                    IsEdit = false;
                    CurrentWO = woResponse.Data!.FirstOrDefault();

                    if (!IsDeviceInfo)
                    {
                        SConsts.GetGlobalAction(SConsts.SET_DEVICE)?.Invoke();
                        SConsts.GetGlobalAction(SConsts.DEV_WO_ADD)?.Invoke();
                    }

                    eventWeakManager.RaiseEvent(this, false, nameof(OnWOViewChange));

                    SetMainData();
                }
            }
        }

        #endregion

        #region METHOD SaveEditWO

        private async void SaveEditWO()
        {
            UpdateWORequest updateWO = new UpdateWORequest();
            updateWO.WorkOrderID = CurrentWO!.WorkOrderID;

            if (string.IsNullOrEmpty(Description))
            {
                return;
            }

            if (WOCat != null)
            {
                updateWO.CategoryID = WOCat.ID;
            }
            else
            {
                updateWO.CategoryID = CurrentWO.CategoryID;
            }

            if (WOState != null)
            {
                updateWO.StateID = WOState.ID;
            }

            if (WOReas != null)
            {
                updateWO.ReasonID = WOReas.ID;
            }
            else
            {
                updateWO.ReasonID = CurrentWO.ReasonID;
            }

            updateWO.PersonID = MainObjects.Instance.CurrentUser!.PersonID;
            updateWO.LevelID = WOLvl!.ID;


            if (AssignedPerson != null)
            {
                updateWO.AssignedPersonID = AssignedPerson.PersonID;
            }
            else
            {
                updateWO.AssignedPersonID = null;
            }

            if (Dep != null)
            {
                updateWO.DepartmentID = Dep.ID;
            }

            updateWO.Description = Description;

            updateWO.Start_Date = StartDate;
            updateWO.End_Date = EndDate;
            updateWO.Location = Location ?? string.Empty;

            var updateResponse = await workOrderBLL.Update(updateWO);

            if (updateResponse.IsResponseWithData(this))
            {
                var woResponse = await workOrderBLL.GetList(new GetWOsRequest()
                {
                    WorkOrderID = updateWO.WorkOrderID
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                if (woResponse.IsResponseWithData(this))
                {
                    IsEdit = false;

                    CurrentWO = woResponse.Data!.FirstOrDefault();
                    //  MessagingCenter.Send<string>(string.Empty, COMMON.SConsts.DEV_WO_ADD);
                    SConsts.GetGlobalAction(SConsts.DEV_WO_ADD)?.Invoke();
                    SConsts.GetGlobalAction(SConsts.SET_DEVICE)?.Invoke();

                    //OnWOViewChange?.Invoke(this, false);
                    eventWeakManager.RaiseEvent(this, false, nameof(OnWOViewChange));
                }
            }
        }

        #endregion

        #region METHOD SetMainData

        public async void SetMainData()
        {
            IsBusy = true;

            if (CurrentWO == null)
                return;

            if (CurrentWO.WorkOrderID > 0)
            {
                var deviceTask = deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                {
                    MachineID = CurrentWO.MachineID
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                var actsTask = woActivityBLL.GetList(new GetWOActsRequest
                {
                    WoID = CurrentWO.WorkOrderID
                    ,
                    Lang = MainObjects.Instance.Lang
                });

                await Task.WhenAll(deviceTask, actsTask);

                var deviceResponse = deviceTask.Result;
                var actsResponse = actsTask.Result;

                if (deviceResponse.IsResponseWithData(this))
                {
                    CurrentDevice = MainObjects.Instance.CurrentDevice = deviceResponse.Data!;
                }
                else
                {
                    return;
                }

                Location = CurrentDevice!.Location;
                Description = CurrentWO.WO_Desc;
                StartDate = CurrentWO.Start_Date;
                EndDate = CurrentWO.End_Date;

                if (actsResponse.IsResponseWithData(this))
                {

                        ActsList = new ObservableCollection<GetWOActsResponse>(actsResponse.Data!);

                }

                if (CurrentWO.PlanID.HasValue)
                {
                    var plansResponse = await planController.GetActs(new API.Contracts.v1.Requests.Plan.GetWOPlanActsRequest
                    {
                        WoID = this.CurrentWO.WorkOrderID
                        ,
                        Lang = MainObjects.Instance.Lang
                    });

                    if (plansResponse.IsResponseWithData(this))
                    {
                        IsPlanList = true;
                        SetMainWOPlansList(plansResponse.Data!.ToList());
                    }
                }

                SetToobarView();
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD SetMainWOPlansList

        private void SetMainWOPlansList(IEnumerable<GetWOPlanActsResponse> list)
        {
            var groups = list.Where(tt => tt.GroupIndexID.HasValue).Select(tt => new { gi = tt.GroupIndexID, gn = tt.GroupName }).Distinct();

            WOPlanList = new ObservableCollection<GetWOPlanActsResponse>();

            if (groups.Count() > 0)
            {
                foreach (var gr in groups)
                {
                    GetWOPlanActsResponse plGr = new GetWOPlanActsResponse();
                    plGr.GroupIndexID = gr.gi;
                    plGr.GroupName = gr.gn;
                    plGr.WorkOrderActivityID = 0;
                    WOPlanList.Add(plGr);

                    foreach (var item in list.Where(tt => tt.GroupIndexID == gr.gi))
                    {
                        WOPlanList.Add(item);
                    }
                }
            }

            foreach (var item in list.Where(tt => !tt.GroupIndexID.HasValue))
            {
                WOPlanList.Add(item);
            }

            OnPropertyChanged(nameof(WOPlanList));
        }

        #endregion

        #region METOD ClearData

        public void ClearData()
        {
            WOCat = null;
            WOCatList = null;
            WOLvl = null;
            WOLvlList = null;
            WOReas = null;
            WOReasList = null;
            WOState = null;
            WOStateList = null;
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            if (IsEdit)
            {
                WOUserScan userScan = new WOUserScan(PersonList!);
                userScan.UIMethod = (obj) =>
                {
                    if (obj is GetAllUsersResponse person)
                    {
                        AssignedPerson = person;
                    }
                };

                scanTypes.Add(userScan);

                WOItemScan wocScan = new WOItemScan("woc:", WOCatList!);
                wocScan.UIMethod = (obj) =>
                {
                    if (obj is DictBase woItem)
                    {
                        WOCat = woItem;
                    }
                };

                scanTypes.Add(wocScan);

                WOItemScan wolScan = new WOItemScan("wol:", WOLvlList!);
                wolScan.UIMethod = (obj) =>
                {
                    if (obj is DictBase woItem)
                    {
                        WOLvl = woItem;
                    }
                };

                scanTypes.Add(wolScan);

                WOItemScan wosScan = new WOItemScan("wos:", WOStateList!);
                wosScan.UIMethod = (obj) =>
                {
                    if (obj is DictBase woItem)
                    {
                        WOState = woItem;
                    }
                };

                scanTypes.Add(wosScan);

                WOItemScan worScan = new WOItemScan("wor:", WOReasList!);
                worScan.UIMethod = (obj) =>
                {
                    if (obj is DictBase woItem)
                    {
                        WOReas = woItem;
                    }
                };

                scanTypes.Add(worScan);

                WOItemScan wodScan = new WOItemScan("wod:", DepList!);
                wodScan.UIMethod = (obj) =>
                {
                    if (obj is DictBase woItem)
                    {
                        Dep = woItem;
                    }
                };

                scanTypes.Add(wodScan);
            }
            else
            {
                if (IsActivityVisible && IsActivityAdd)
                {
                    WOItemScan woacScan = new WOItemScan("woac:", CatActList!);
                    woacScan.UIMethod = (obj) =>
                    {
                        if (obj is DictBase woItem)
                        {
                            CatAct = woItem;
                        }
                    };

                    scanTypes.Add(woacScan);
                }
                else if (IsCloseWOOpen)
                {
                    WOItemScan wocScan = new WOItemScan("woc:", WOCatList!);
                    wocScan.UIMethod = (obj) =>
                    {
                        if (obj is DictBase woItem)
                        {
                            WOCat = woItem;
                        }
                    };

                    scanTypes.Add(wocScan);

                    WOItemScan worScan = new WOItemScan("wor:", WOReasList!);
                    worScan.UIMethod = (obj) =>
                    {
                        if (obj is DictBase woItem)
                        {
                            WOReas = woItem;
                        }
                    };

                    scanTypes.Add(worScan);
                }
            }

            return scanTypes;
        }

        public override string GetVisualScanPresentation() => "construction";

        #endregion
    }
}
