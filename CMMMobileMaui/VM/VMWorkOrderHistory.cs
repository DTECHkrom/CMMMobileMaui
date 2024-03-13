using System.Collections.Generic;
using System.Linq;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.VM
{
    public class VMWorkOrderHistory: COMMON.ViewModelBase
    {
        private IWOController workOrderBLL;
        #region PROPERTY WOHistList

        public List<GetWOHistResponse> WOHistList
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

        public VMWorkOrderHistory(IWOController workOrderBLL)
        {
            this.workOrderBLL = workOrderBLL;
        }

        #region METHOD SetMainData

        public async void SetMainData()
        {
            IsBusy = true;

            if (CurrentWO != null)
            {
                var histResponse = await workOrderBLL.GetHist(new API.Contracts.v1.Requests.WO.GetWOHistRequest
                {
                    WorkOrderID = this.CurrentWO.WorkOrderID,
                    Lang = API.MainObjects.Instance.Lang
                });

                if(histResponse.IsValid)
                {
                    WOHistList = histResponse.Data.ToList();
                }

                OnPropertyChanged("WOHistList");
            }

            IsBusy = false;
        }

        #endregion
    }
}
