using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Requests.Part;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.API.Model;

namespace CMMMobileMaui.API
{
    public class DictionaryResources
    {
        private string currentDictLanguage;

        #region FIELDS 

        private IPartController partController;
        private IDeviceController deviceController;
        private IUserController userController;
        private IOtherController otherController;
        private IActController actController;
        private IWOController woController;

        private static readonly Lazy<DictionaryResources> lazyInstance = new Lazy<DictionaryResources>(() => new DictionaryResources());

        public static DictionaryResources Instance =>
            lazyInstance.Value;

        #endregion


        #region PROPERTY WarehouseList

        public List<PartDictResponse> WarehouseList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PartCategoryList

        public List<GetPartCatResponse> PartCategoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PartGeneralCategoryList

        public List<GetPartCatResponse> PartGeneralCategoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY MainPartCategoryList

        public List<GetPartCatResponse> MainPartCategoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ProducentList

        public List<PartDictResponse> ProducentList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY UnitList

        public List<PartDictResponse> PartUnitList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrencyList

        public List<PartDictResponse> CurrencyList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PersonList

        public List<GetAllUsersResponse> PersonList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceCategoryList

        public List<DeviceDictResponse> DeviceCategoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceProducentList

        public List<DeviceDictResponse> DeviceProducentList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WODictionaryForAdd

        public WODictionaryBase WODictionaryForAdd
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WODictionaryForEdit

        public WODictionaryBase WODictionaryForEdit
        {
            get;
            set;
        }

        #endregion

        //#region PROPERTY WOCategoryList

        //public List<WODictResponse> WOCategoryList
        //{
        //    get;
        //    set;
        //}

        //#endregion

        //#region PROPERTY WOLevelList

        //public List<WODictResponse> WOLevelList
        //{
        //    get;
        //    set;
        //}

        //#endregion

        //#region PROPERTY WOReasonList

        //public List<WODictResponse> WOReasonList
        //{
        //    get;
        //    set;
        //}

        //#endregion

        //#region PROPERTY WOStateList

        //public List<WODictResponse> WOStateList
        //{
        //    get;
        //    set;
        //}

        //#endregion

        //#region PORPERTY WODepartmentList

        //public List<WODictResponse> WODepartmentList
        //{
        //    get;
        //    set;
        //}

        //#endregion

        #region PROPERTY ActCategoryList

        public List<WOActDictResponse> ActCategoryList
        {
            get;
            set;
        }

        #endregion

        #region Cstr

        private DictionaryResources()
        {
            InitControllers();
            InitLocalLists();
        }

        #endregion

        #region METHOD GetWOActDictListFor

        public List<WOActDictResponse> GetWOActDictListFor(int deviceCategoryID)
        {
            var temp = new List<WOActDictResponse>();

            temp.AddRange(ActCategoryList.Where(tt => !tt.MachineCategoryID.HasValue));
            temp.AddRange(ActCategoryList.Where(tt => tt.MachineCategoryID.HasValue && tt.MachineCategoryID == deviceCategoryID));
            
            return temp.OrderBy(tt => tt.Name)
                .ToList();
        }

        #endregion

        #region METHOD InitControllers

        private void InitControllers()
        {
            partController = (IPartController)MainObjects.Instance.ServiceProvider.GetService(typeof(IPartController));
            deviceController = (IDeviceController)MainObjects.Instance.ServiceProvider.GetService(typeof(IDeviceController));
            userController = (IUserController)MainObjects.Instance.ServiceProvider.GetService(typeof(IUserController));
            otherController = (IOtherController)MainObjects.Instance.ServiceProvider.GetService(typeof(IOtherController));
            actController = (IActController)MainObjects.Instance.ServiceProvider.GetService(typeof(IActController));
            woController = (IWOController)MainObjects.Instance.ServiceProvider.GetService(typeof(IWOController));
        }

        #endregion

        #region METHOD InitLocalLists

        private void InitLocalLists()
        {
            WarehouseList = new List<PartDictResponse>();
            PartCategoryList = new List<GetPartCatResponse>();
            PartGeneralCategoryList = new List<GetPartCatResponse>();
            MainPartCategoryList = new List<GetPartCatResponse>();
            ProducentList = new List<PartDictResponse>();
            PartUnitList = new List<PartDictResponse>();
            CurrencyList = new List<PartDictResponse>();
            PersonList = new List<GetAllUsersResponse>();
            DeviceCategoryList = new List<DeviceDictResponse>();
            DeviceProducentList = new List<DeviceDictResponse>();
            ActCategoryList = new List<WOActDictResponse>();
        }

        #endregion

        #region METHOD InitDictionariesAfterLogin

        public async Task InitDataAfterLogin()
        {
            if (!WasDictLanguageChanged())
            {
                return;
            }

            currentDictLanguage = MainObjects.Instance.Lang;
            ClearDictionaries();

            List<Task> initTask = new List<Task>();

            initTask.AddRange(GetPartDictTasks());
            initTask.AddRange(GetDeviceDictTasks());
            initTask.AddRange(GetWorkOrderDictTasks());
            initTask.AddRange(GetActivityDictTask());

            if (PersonList == null
                || PersonList.Count == 0)
            {
                initTask.Add(GetUserListTask());
            }

            await Task.WhenAll(initTask.ToArray());
        }

        #endregion 

        #region METHOD WasDictLanguageChanged

        private bool WasDictLanguageChanged()
        {
            return currentDictLanguage != MainObjects.Instance.Lang;
        }

        #endregion

        #region METHOD GetFilteredAndSortedList

        private List<T> GetFilteredAndSortedList<T>(IEnumerable<T> list, int listType) where T : DictBase =>
            list.Where(tt => tt.ListType == listType)
                        .OrderBy(tt => tt.Name)
                        .ToList();

        #endregion

        #region METHOD GetActivityDictTask

        private Task[] GetActivityDictTask()
        {
            Task[] tasks = new Task[1];

            var dictTask = actController.GetDict(new Contracts.v1.Requests.Act.GetActDictRequest
            {
                Lang = MainObjects.Instance.Lang
                ,
                PersonID = MainObjects.Instance.CurrentUser.PersonID
            }).ContinueWith(task =>
            {
                if (task.Result.IsValid)
                {
                    var temp = task.Result.Data;

                    if (temp != null)
                    {
                        ActCategoryList = GetFilteredAndSortedList(temp, (int)Common.Enums.ActivityDictListType.category);
                    }
                }
            });

            tasks[0] = dictTask;

            return tasks;
        }

        #endregion

        #region METHOD GetWorkOrderDictTasks

        private Task[] GetWorkOrderDictTasks()
        {
            Task[] tasks = new Task[1];

            var dictTask = woController.GetDict(new Contracts.v1.Requests.WO.GetWODictRequest
            {
                Lang = MainObjects.Instance.Lang
                ,
                PersonID = MainObjects.Instance.CurrentUser.PersonID
            }).ContinueWith(task =>
            {
                if (task.Result.IsValid)
                {
                    var temp = task.Result.Data;

                    if (temp != null)
                    {
                        var WOCategoryList = GetFilteredAndSortedList(temp, (int)Common.Enums.WorkOrderDictListType.category);
                        var WODepartmentList = GetFilteredAndSortedList(temp, (int)Common.Enums.WorkOrderDictListType.department);
                        var WOLevelList = GetFilteredAndSortedList(temp, (int)Common.Enums.WorkOrderDictListType.level);
                        var WOReasonList = GetFilteredAndSortedList(temp, (int)Common.Enums.WorkOrderDictListType.reason);
                        var WOStateList = GetFilteredAndSortedList(temp, (int)Common.Enums.WorkOrderDictListType.state);

                        WODictionaryForAdd = new WODictionaryForAdd(WOCategoryList, WOLevelList, WOReasonList, WOStateList, WODepartmentList);
                        WODictionaryForEdit = new WODictionaryForEdit(WOCategoryList, WOLevelList, WOReasonList, WOStateList, WODepartmentList);
                    }
                }
            });

            tasks[0] = dictTask;

            return tasks;
        }

        #endregion

        #region METHOD GetDeviceDictTasks

        private Task[] GetDeviceDictTasks()
        {
            Task[] tasks = new Task[1];

            var dictTask = deviceController.GetDict(new Contracts.v1.Requests.GetDictRequest
            {
                PersonID = MainObjects.Instance.CurrentUser.PersonID,
                Lang = MainObjects.Instance.Lang
            }).ContinueWith(task =>
            {
                if (task.Result.IsValid)
                {
                    var temp = task.Result.Data;

                    if (temp != null)
                    {
                        DeviceCategoryList = GetFilteredAndSortedList(temp, (int)Common.Enums.DeviceDictListType.category);
                        DeviceProducentList = GetFilteredAndSortedList(temp, (int)Common.Enums.DeviceDictListType.supplier);
                    }
                }
            });

            tasks[0] = dictTask;

            return tasks;
        }

        #endregion

        #region METHOD GetPartDictTasks

        private Task[] GetPartDictTasks()
        {
            Task[] tasks = new Task[2];

            var dictTask = partController.GetDict(new Contracts.v1.Requests.GetDictRequest
            {
                PersonID = MainObjects.Instance.CurrentUser.PersonID,
                Lang = MainObjects.Instance.Lang
            }).ContinueWith(task =>
            {
                if (task.Result.IsValid)
                {
                    var temp = task.Result.Data;

                    if (temp != null)
                    {
                        WarehouseList = GetFilteredAndSortedList(temp, (int)Common.Enums.PartDictListType.warehouse);
                        ProducentList = GetFilteredAndSortedList(temp, (int)Common.Enums.PartDictListType.producent);
                        PartUnitList = GetFilteredAndSortedList(temp, (int)Common.Enums.PartDictListType.unit);
                        CurrencyList = GetFilteredAndSortedList(temp, (int)Common.Enums.PartDictListType.currency);
                    }
                }
            });

            var catsTask = partController.GetCat(new GetPartCatRequest
            {
                Lang = MainObjects.Instance.Lang
            }).ContinueWith(task =>
            {
                if (task.Result.IsValid)
                {
                    MainPartCategoryList = task.Result.Data.ToList();

                    PartGeneralCategoryList = MainPartCategoryList.GroupBy(tt => (ID: tt.PartGeneralCategoryID, Name: tt.GeneralName, Prefix: tt.GeneralPrefix))
                            .Select(tt => new GetPartCatResponse() { PartGeneralCategoryID = tt.Key.ID, GeneralName = tt.Key.Name, GeneralPrefix = tt.Key.Prefix })
                            .ToList();
                }
            });

            tasks[0] = dictTask;
            tasks[1] = catsTask;

            return tasks;
        }

        #endregion

        #region METHOD InitDataBeforeLogin

        public async Task InitDataBeforeLogin() =>
            await GetUserListTask();

        private Task GetUserListTask()
        {
            var userTask = otherController.GetUsersList()
                .ContinueWith(task =>
                {
                    if (task.Result.IsValid)
                    {
                        var temp = task.Result.Data;

                        if (temp != null)
                        {
                            PersonList = task.Result
                                .Data
                                .OrderBy(tt => tt.Name)
                                .ToList();
                        }
                    }
                });


            return userTask;
        }

        #endregion

        #region METHOD ClearDictionaries

        private void ClearDictionaries()
        {
            WarehouseList.Clear();
            ProducentList.Clear();
            PartUnitList.Clear();
            CurrencyList.Clear();
            PersonList.Clear();
            MainPartCategoryList.Clear();
            PartGeneralCategoryList.Clear();
            DeviceCategoryList.Clear();
            DeviceProducentList.Clear();

        }

        #endregion
    }
}
