using System.Collections.Generic;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;

namespace CMMMobileMaui.API.Model
{
    public class WODictionaryForEdit : WODictionaryBase, IWODictionary
    {
        public List<DictBase> WOCategoryList
        {
            get;
            private set;
        }

        public List<DictBase> WOLevelList
        {
            get;
            private set;
        }

        public List<DictBase> WOReasonList
        {
            get;
            private set;
        }

        public List<DictBase> WOStateList
        {
            get;
            private set;
        }

        public List<DictBase> WODepartmentList
        {
            get;
            private set;
        }

        public WODictionaryForEdit(List<WODictResponse> wOCategoryList
            , List<WODictResponse> wOLevelList
            , List<WODictResponse> wOReasonList
            , List<WODictResponse> wOStateList
            , List<WODictResponse> wODepartmentList) : base(wOCategoryList, wOLevelList, wOReasonList, wOStateList, wODepartmentList)
        {
        }

        protected override IWODictionary FilterLists(GetDeviceListResponse device)
        {
            WOStateList = GetForDeviceCategory(WOStateMainList, device)
                .ToDictBaseList();

            WOReasonList = GetForDeviceCategory(WOReasonMainList, device)
                .ToDictBaseList();

            WOCategoryList = GetForDeviceCategory(WOCategoryMainList, device)
                .ToDictBaseList();

            WODepartmentList = WODepartmentMainList.ToDictBaseList();
            WOLevelList = WOLevelMainList.ToDictBaseList();

            return this;
        }

        internal override void Clear()
        {
            base.Clear();

            WOReasonList.Clear();
            WOStateList.Clear();
            WOReasonList.Clear();
            WOLevelList.Clear();
            WODepartmentList.Clear();
        }
    }
}
