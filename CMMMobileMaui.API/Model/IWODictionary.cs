using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;

namespace CMMMobileMaui.API.Model
{
    public interface IWODictionary
    {
        #region PROPERTY WOCategoryList

        List<DictBase> WOCategoryList
        {
            get;
        }

        #endregion

        #region PROPERTY WOLevelList

        List<DictBase> WOLevelList
        {
            get;
        }

        #endregion

        #region PROPERTY WOReasonList

        List<DictBase> WOReasonList
        {
            get;
        }

        #endregion

        #region PROPERTY WOStateList

        List<DictBase> WOStateList
        {
            get;
        }

        #endregion

        #region PORPERTY WODepartmentList

        List<DictBase> WODepartmentList
        {
            get;
        }

        #endregion

        //#region PROPERTY WOCategoryList

        //List<WODictResponse> WOCategoryList
        //{
        //    get;
        //}

        //#endregion

        //#region PROPERTY WOLevelList

        //List<WODictResponse> WOLevelList
        //{
        //    get;
        //}

        //#endregion

        //#region PROPERTY WOReasonList

        //List<WODictResponse> WOReasonList
        //{
        //    get;
        //}

        //#endregion

        //#region PROPERTY WOStateList

        //List<WODictResponse> WOStateList
        //{
        //    get;
        //}

        //#endregion

        //#region PORPERTY WODepartmentList

        //List<WODictResponse> WODepartmentList
        //{
        //    get;
        //}

        //#endregion
    }
}
