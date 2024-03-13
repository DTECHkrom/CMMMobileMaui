using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.MODEL
{
    public class WOPicModel : ObservableObject, COMMON.PictureOperation.IPictureModel
    {
        #region PROPERTY CurrentWO

        public GetWOsResponse CurrentWO
        {
            get;
        }

        #endregion

        #region PROPERTY CanEdit

        public bool CanEdit
        {
            get;
        }

        #endregion

        #region PROPERTY PictureList

        private ObservableCollection<WOFile> pictureList = new ObservableCollection<WOFile>();

        public ObservableCollection<WOFile> PictureList
        {
            get => pictureList;
            set => SetProperty(ref pictureList, value);
        }
        #endregion

        #region PROPERTY IsPicture

        private bool isPicture;

        public bool IsPicture
        {
            get => isPicture;
            set
            {
                SetProperty(ref isPicture, value);
            }
        }

        #endregion

        #region Cstr

        public WOPicModel(GetWOsResponse wo, bool canEdit = true)
        {
            this.CurrentWO = wo;
            this.CanEdit = canEdit;
            IsPicture = false;
        }

        #endregion
    }
}
