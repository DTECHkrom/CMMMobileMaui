namespace CMMMobileMaui.VM
{
    public class VMWorkOrderFileMain : COMMON.ViewModelBase
    {
        //#region FIELDS

        //private bool hasStartItems = false;

        //#endregion

        //#region PROPERTY WorkOrderID

        //private int _workOrderID;

        //public int WorkOrderID
        //{
        //    get
        //    {
        //        return _workOrderID;
        //    }

        //    set
        //    {
        //        _workOrderID = value;
        //    }
        //}

        //#endregion

        //#region PROPERTY WOFiles

        //public List<WOFile> WOFiles
        //{
        //    get;
        //    set;
        //}

        //#endregion

        //#region COMMAND SelectPictureCommand

        //public ICommand SelectPictureCommand
        //{
        //    get;
        //}

        //private IWOController workOrderBLL;

        //#endregion

        //#region COMMAND TakePictureCommand

        //public ICommand TakePictureCommand
        //{
        //    get;
        //}

        //#endregion

        //#region COMMAND OpenImageCommand

        //public ICommand OpenImageCommand
        //{
        //    get;
        //}

        //#endregion

        //#region COMMAND DeleteImageCommand

        //public ICommand DeleteImageCommand
        //{
        //    get;
        //}

        //#endregion

        //#region Cstr

        //public VMWorkOrderFileMain(IWOController workOrderBLL)
        //{
        //    IsBusy = false;
        //    this.workOrderBLL = workOrderBLL;

        //    TakePictureCommand = new Command(async () =>
        //    {
        //        try
        //        {
        //            await CrossMedia.Current.Initialize();

        //            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //            {
        //                return;
        //            }

        //            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        //            {
        //                SaveToAlbum = true
        //                ,
        //                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
        //            });

        //            if (CrossMedia.Current.IsPickPhotoSupported && file != null)
        //            {
        //                bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

        //                if (dialogResult)
        //                {
        //                    IsBusy = true;

        //                    if (file != null)
        //                    {
        //                        byte[] tab = COMMON.SConsts.GetByteArrayFromStream(file.GetStream());

        //                        WOFile fileWO = new WOFile();
        //                        fileWO.Data = tab;
        //                        fileWO.File_Name = Path.GetFileNameWithoutExtension(file.Path);
        //                        fileWO.PersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID;
        //                        fileWO.WorkOrderID = WorkOrderID;
        //                        fileWO.Extension = Path.GetExtension(file.Path);

        //                        bool isOk = await this.workOrderBLL.AddFile(fileWO);

        //                        if (isOk)
        //                        {
        //                            SetMainData();
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //        catch (Exception) { }

        //        IsBusy = false;

        //    });

        //    SelectPictureCommand = new Command(async (obj) =>
        //    {
        //        try
        //        {
        //            if (CanClick())
        //            {
        //                IsBusy = true;

        //                var cos = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
        //                {
        //                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
        //                });

        //                if (cos != null)
        //                {
        //                    byte[] tab = GetByteArrayFromStream(cos.GetStream());

        //                    WOFile file = new WOFile();
        //                    file.Data = tab;
        //                    file.File_Name = Path.GetFileNameWithoutExtension(cos.Path);
        //                    file.PersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID;
        //                    file.WorkOrderID = WorkOrderID;
        //                    file.Extension = Path.GetExtension(cos.Path);

        //                    bool isOk = await this.workOrderBLL.AddFile(file);

        //                    if (isOk)
        //                    {
        //                        SetMainData();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception) { }

        //        IsBusy = false;

        //    });

        //    OpenImageCommand = new Command(async (obj) =>
        //    {
        //        if (CanClick())
        //        {
        //            if (obj != null)
        //            {
        //                await App.Current.MainPage.Navigation.PushModalAsync(new VIEW.ImageView(((WOFile)obj).Data));
        //            }
        //        }
        //    });

        //    DeleteImageCommand = new Command(async (obj) =>
        //    {
        //        if (obj != null)
        //        {
        //            bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

        //            if(dialogResult)
        //            {
        //                IsBusy = true;

        //                WOFile file = (WOFile)obj;
        //                file.Data = null;
        //                file.PersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID;

        //                var isOk = await this.workOrderBLL.AddFile(file);

        //                if (isOk)
        //                {
        //                    SetMainData();
        //                }

        //                IsBusy = false;
        //            }
        //        }
        //    });
        //}

        //#endregion

        //#region METHOD GetByteArrayFromStream

        //private byte[] GetByteArrayFromStream(Stream s)
        //{         
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //         s.CopyTo(ms);
        //        return ms.ToArray();
        //    }
        //}

        //#endregion

        //#region METHOD SetMainData

        //public async void SetMainData()
        //{
        //    IsBusy = true;

        //    WOFiles = await this.workOrderBLL.GetFilesByWOID(WorkOrderID, true);

        //    OnPropertyChanged("WOFiles");

        //    IsBusy = false;
        //}

        //#endregion
    }
}
