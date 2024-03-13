using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.MODEL;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.VM
{
    public class VMWorkOrderUserScheduler : COMMON.ViewModelBase
    {
        #region FIELDS

        private WeakEventManager weakEventHandlerManager = new WeakEventManager();

        public event EventHandler OnShowCurrentDate
        {
            add => weakEventHandlerManager.AddEventHandler(value);
            remove => weakEventHandlerManager.RemoveEventHandler(value);
        }

        private IWOController workOrderBLL;

        #endregion

        #region PROPERTY WOList

        private ObservableCollection<WOAppointment> wOList = new ObservableCollection<WOAppointment>();

        public ObservableCollection<WOAppointment> WOList
        {
            get => wOList;
            set => SetProperty(ref wOList, value);
        }

        #endregion

        #region Cstr

        public VMWorkOrderUserScheduler(IWOController workOrderBLL)
        {
            this.workOrderBLL = workOrderBLL;
        }

        #endregion

        #region METHOD SetMainSource

        public async void SetMainSource()
        {
            IsBusy = true;

            WOList.Clear();

            var wosResponse = await workOrderBLL.GetList(new API.Contracts.v1.Requests.WO.GetWOsRequest()
            {
                PersonID = API.MainObjects.Instance.CurrentUser!.PersonID
                ,
                IsScheduled = false
                ,
                IsAssignedPerson = null
                ,
                Active = true
                ,
                IsWithPerson = true
                ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (wosResponse.IsResponseWithData(this))
            {
                var userWOItems = wosResponse.Data!.Where(IsWOStartEndDate).ToList();

                foreach (var item in userWOItems)
                {
                    WOList.Add(new WOAppointment(item));
                }
            }

            IsBusy = false;
        }

        #endregion

        //#region METHOD IsWOScheduledForCurrentPerson

        //private bool IsWOScheduledForCurrentPerson(GetWOsResponse woItem) =>
        //    string.IsNullOrEmpty(woItem.Assigned_Person) ? false
        //    : !woItem.Start_Date.HasValue || !woItem.End_Date.HasValue ? false
        //    : woItem.Assigned_Person.ToLowerInvariant() == API.MainObjects.Instance.CurrentUser!.Name.ToLowerInvariant() ? true : false;

        //#endregion

        #region METHOD IsWOScheduledForCurrentPerson

        private bool IsWOStartEndDate(GetWOsResponse woItem) =>
             !woItem.Start_Date.HasValue || !woItem.End_Date.HasValue ? false : true;

        #endregion
    }
}
