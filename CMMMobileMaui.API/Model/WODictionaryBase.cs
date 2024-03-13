using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;

namespace CMMMobileMaui.API.Model
{
    public abstract class WODictionaryBase
    {
        protected int currentDeviceCategoryID;
        private IWODictionary currentWODictionary;

        #region PROPERTY WOCategoryList

        protected List<WODictResponse> WOCategoryMainList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOLevelList

        protected List<WODictResponse> WOLevelMainList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOReasonList

        protected List<WODictResponse> WOReasonMainList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOStateList

        protected List<WODictResponse> WOStateMainList
        {
            get;
            set;
        }

        #endregion

        #region PORPERTY WODepartmentList

        protected List<WODictResponse> WODepartmentMainList
        {
            get;
            set;
        }

        protected WODictionaryBase(List<WODictResponse> wOCategoryList
            , List<WODictResponse> wOLevelList
            , List<WODictResponse> wOReasonList
            , List<WODictResponse> wOStateList
            , List<WODictResponse> wODepartmentList)
        {
            WOCategoryMainList = wOCategoryList;
            WOLevelMainList = wOLevelList;
            WOReasonMainList = wOReasonList;
            WOStateMainList = wOStateList;
            WODepartmentMainList = wODepartmentList;
            currentDeviceCategoryID = 0;
        }

        #endregion

        public IWODictionary ForDevice(GetDeviceListResponse device)
        {
            if (ShouldFilterLists(device))
            {
                currentDeviceCategoryID = device.CategoryID;
                currentWODictionary = FilterLists(device);
            }

            return currentWODictionary;
        }

        protected bool ShouldFilterLists(GetDeviceListResponse device) =>
            currentDeviceCategoryID != device.CategoryID;

        protected abstract IWODictionary FilterLists(GetDeviceListResponse device);

        protected bool HasListDefaultValue(List<WODictResponse> list) =>
            list.FirstOrDefault(tt => tt.Is_Default == true) != null;

        protected List<WODictResponse> GetForDeviceCategory(List<WODictResponse> list, GetDeviceListResponse device) =>
            list.Where(tt => tt.MachineCategoryID.HasValue == false || tt.MachineCategoryID.Value == device.CategoryID)
            .ToList();

        protected List<DictBase> GetConvertedListToDictBaseList(List<WODictionaryBase> list) =>
            list.Cast<DictBase>()
            .OrderBy(tt => tt.Name)
            .ToList();

        internal virtual void Clear()
        {
            currentDeviceCategoryID = 0;
            WOCategoryMainList.Clear();
            WODepartmentMainList.Clear();
            WOLevelMainList.Clear();
            WOReasonMainList.Clear();
            WOStateMainList.Clear();
        }
    }
}
