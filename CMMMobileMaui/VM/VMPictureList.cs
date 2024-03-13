using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.COMMON.PictureOperation;

namespace CMMMobileMaui.VM
{
    public class VMPictureList : COMMON.ViewModelBase
    {
        #region PROPERTY Operation

        private IPictureOperation? operation;
        public IPictureOperation? Operation
        {
            get => operation;
            set
            {
                SetProperty(ref operation, value);
            }
        }

        #endregion

        #region PROPERTY PictureManager

        public PictureManager PictureManager
        {
            get;
            set;
        } = new PictureManager();

        #endregion

        #region Cstr

        public VMPictureList()
        {

        }

        #endregion

        #region METHOD Init

        public async void Init()
        {
            if (!CanUseOperation())
                return;

            IsBusy = true;

            await Operation!.Select();

            PictureManager.OnDeletePicture -= PictureManager_OnDeletePicture;
            PictureManager.OnSelectPicture -= PictureManager_OnSelectPicture;
            PictureManager.OnTakePicture -= PictureManager_OnTakePicture;
            PictureManager.OnEditPicture -= PictureManager_OnEditPicture;

            PictureManager.OnDeletePicture += PictureManager_OnDeletePicture;
            PictureManager.OnSelectPicture += PictureManager_OnSelectPicture;
            PictureManager.OnTakePicture += PictureManager_OnTakePicture;
            PictureManager.OnEditPicture += PictureManager_OnEditPicture;

            OnPropertyChanged(nameof(Operation));

            IsBusy = false;
        }

        #endregion

        #region METHOD CanUseOperation

        private bool CanUseOperation() => Operation is not null;

        #endregion

        #region EVENT PictureManager_OnEditPicture

        private async void PictureManager_OnEditPicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (!CanUseOperation())
                return;

            IsBusy = true;

            await Operation!.Replace(e.info);

            IsBusy = false;
        }

        #endregion

        #region EVENT PictureManager_OnTakePicture

        private async void PictureManager_OnTakePicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (!CanUseOperation())
                return;

            IsBusy = true;

            await Operation!.Insert(e.info);

            IsBusy = false;
        }

        #endregion

        #region EVENT PictureManager_OnSelectPicture

        private async void PictureManager_OnSelectPicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (!CanUseOperation())
                return;

            IsBusy = true;

            await Operation!.Insert(e.info);

            IsBusy = false;
        }

        #endregion

        #region EVENT PictureManager_OnDeletePicture

        private async void PictureManager_OnDeletePicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (!CanUseOperation())
                return;

            IsBusy = true;

            await Operation!.Delete(e.info);

             IsBusy = false;
        }

        #endregion
    }
}
