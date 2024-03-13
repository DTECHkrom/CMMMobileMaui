using System.Windows.Input;
using CMMMobileMaui.API.Contracts;
using Camera.MAUI;

namespace CMMMobileMaui.COMMON.PictureOperation
{
    public class PictureManager : ViewModelBase
    {
        #region FIELDS

        private WeakEventManager weakEventManager = new();

        public event EventHandler<(WOFile info, object? commandParameter)> OnTakePicture
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }

        public event EventHandler<(WOFile info, object? commandParameter)> OnSelectPicture
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }

        public event EventHandler<(WOFile info, object? commandParameter)> OnDeletePicture
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }

        public event EventHandler<(WOFile info, object? commandParameter)> OnEditPicture
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }

        #endregion

        #region COMMAND TakePictureCommand

        public ICommand TakePictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectPictureCommand

        public ICommand SelectPictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND DeletePictureCommand

        public ICommand DeletePictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND EditPictureCommand

        public ICommand EditPictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenPictureCommand

        public ICommand OpenPictureCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public PictureManager()
        {
            this.TakePictureCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    var picture = await TakePicture();

                    if (picture != null)
                    {
                        weakEventManager.HandleEvent(this, (picture, obj), nameof(OnTakePicture));
                    }
                }
            });

            this.SelectPictureCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    var picture = await SelectPicture();

                    if (picture != null)
                    {
                        weakEventManager.HandleEvent(this, (picture, obj), nameof(OnSelectPicture));
                    }
                }
            });

            this.OpenPictureCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is WOFile picture)
                    {
                        await Shell.Current.Navigation.PushModalAsync(new VIEW.PictureView(picture));
                    }
                }
            });

            this.DeletePictureCommand = new Command(async (obj) =>
            {
                await BaseStepActionMethod<WOFile>((picture) =>
                {
                    weakEventManager.HandleEvent(this, (picture, obj), nameof(OnDeletePicture));
                }, obj);

            });

            this.EditPictureCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null
                    && obj is WOFile picture)
                    {
                        var view = new VIEW.PictureEditView(picture);
                        view.OnFileSave += View_OnFileSave;
                        await Shell.Current.Navigation.PushModalAsync(view); 
                    }
                }
            });
        }

        private void View_OnFileSave(object? sender, WOFile e)
        {
            weakEventManager.HandleEvent(this, (e, (object?)null), nameof(OnEditPicture));
        }

        #endregion

        #region METHOD TakePicture

        public async Task<WOFile?> TakePicture()
        {
            try
            {
              //  Camera.MAUI.CameraView.Current.TakePhotoAsync()
                
              //  await CrossMedia.Current.Initialize();

                //MediaPicker.Default.Is

                //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                //{
                //    return null;
                //}

                var file = await MediaPicker.CapturePhotoAsync();

                if (file != null)
                {
                    return await GetFromMediaFile(file);
                }
                

                return null;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "cancel");


                return null;
            }
        }

        #endregion

        #region METHOD GetFromMediaFile

        private async Task<WOFile> GetFromMediaFile(FileResult file) =>
            new WOFile()
            {
                Data = SConsts.GetByteArrayFromStream(await file.OpenReadAsync())
                ,
                File_Name = Path.GetFileNameWithoutExtension(file.FileName)
                ,
                Extension = Path.GetExtension(file.FullPath)
            };


        #endregion

        #region METHOD SelectPicture

        public async Task<WOFile?> SelectPicture()
        {
            try
            {
                // await CrossMedia.Current.Initialize();

                var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "test"
                });

                if (file != null)
                {
                    return await GetFromMediaFile(file);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "cancel");
            }
            return null;
        }

        #endregion

        #region OpenPictureList

        public async void OpenPictureList(IPictureOperation operation)
        {
            await Shell.Current.Navigation.PushModalAsync(new VIEW.PictureListView(operation));
        }

        #endregion

    }
}
