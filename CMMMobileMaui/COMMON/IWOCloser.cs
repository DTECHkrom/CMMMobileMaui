using CMMMobileMaui.API.Contracts.v1.Responses;

namespace CMMMobileMaui.COMMON
{
    public interface IWOCloser
    {
        #region PROPERTY WOCat

        DictBase WOCat
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY WOReas

        DictBase WOReas
        {
            get;
            set;
        }


        #endregion
    }
}
