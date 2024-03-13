using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CommunityToolkit.Maui.Extensions;
using CMMMobileMaui.COMMON.Resources;

namespace CMMMobileMaui.VM
{
    public class VMDeviceDetail: COMMON.ViewModelBase
    {
        #region FIELDS

        private int devID;
        private IDeviceController deviceCMMBLL;

        #endregion

        #region PROPERTY ItemsList

        public List<GetDeviceDetailsResponse> ItemsList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WarrList

        public List<GetDeviceWarrantyResponse> WarrList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsWarr

        public bool IsWarr
        {
            get;
            set;
        }

        #endregion

        #region Cstr

        public VMDeviceDetail(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;
        }

        #endregion

        #region METHOD SetDeviceID

        public void SetDeviceID(int devID)
        {
            this.devID = devID;
        }

        #endregion

        #region METHOD SetMainData

        public async void SetMainData()
        {
            await Task.Delay(150);

            var detailTask = deviceCMMBLL.GetDetail(new API.Contracts.v1.Requests.Device.GetDeviceDetailsRequest
            {
                DeviceID = devID
                , Lang = API.MainObjects.Instance.Lang
            });

            var warrTask = deviceCMMBLL.GetWarranty(new API.Contracts.v1.Requests.Device.GetByDeviceIDRequest
            {
                DeviceID = devID              
            });

            await Task.WhenAll(detailTask, warrTask);

            if(detailTask.Result.IsValid)
            {
                var tempList = detailTask.Result.Data.ToList();
                
                foreach(var item in tempList)
                {
                    item.SetDisplayName();
                }

                ItemsList = tempList;
            }

            if(warrTask.Result.IsValid)
            {
                if (warrTask.Result.Data.Count() > 0)
                {
                    WarrList = warrTask.Result.Data.ToList();
                    IsWarr = true;
                    OnPropertyChanged(nameof(WarrList));
                    OnPropertyChanged(nameof(IsWarr));
                }
            }

            OnPropertyChanged(nameof(ItemsList));
        }

        #endregion
    }
}
