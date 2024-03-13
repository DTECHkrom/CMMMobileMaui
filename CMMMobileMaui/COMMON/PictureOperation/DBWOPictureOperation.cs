using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.COMMON.PictureOperation
{
    public class DBWOPictureOperation : IPictureOperation
    {
        private GetWOsResponse wo;
        private IWOController workOrderBLL;
        public IPictureModel PictureModel
        {
            get;
        }

        public DBWOPictureOperation(IPictureModel model, GetWOsResponse wo)
        {
            workOrderBLL = (IWOController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IWOController));
            PictureModel = model;
            this.wo = wo;
        }


        public async Task<bool> Delete(WOFile fileModel)
        {
            var deleteResponse = await workOrderBLL.DeleteFile(new API.Contracts.v1.Requests.GetByFileIDRequest
            {
                FileID = fileModel.WorkOrderDataID
            });

            await Select();

            return deleteResponse.IsValid;
        }

        public async Task<bool> Insert(WOFile fileModel)
        {
            if (fileModel.Data != null)
            {
                fileModel.WorkOrderID = wo.WorkOrderID;
                fileModel.PersonID = API.MainObjects.Instance.CurrentUser!.PersonID;

                var createResponse = await workOrderBLL.CreateFile(new API.Contracts.v1.Requests.WO.CreateWOFileRequest
                {
                    WorkOrderID = wo.WorkOrderID,
                    Data = fileModel.Data,
                    FileNameWithExtension = fileModel.File_Name + fileModel.Extension,
                    PersonID = API.MainObjects.Instance.CurrentUser.PersonID
                });

                await Select();

                return createResponse.IsValid;
            }

            return false;
        }

        public async Task Select()
        {
            var filesResponse = await workOrderBLL.GetFiles(new API.Contracts.v1.Requests.WO.GetByWorkOrderIDRequest
            {
                WorkOrderID = wo.WorkOrderID
            });

            if (filesResponse.IsResponseWithData())
            {
                var files = filesResponse.Data!.Select(tt => new WOFile
                {
                    Data = tt.Data
                    ,
                    Extension = tt.Extension
                    ,
                    File_Name = tt.File_Name
                    ,
                    Person_Name = tt.Person_Name
                    ,
                    WorkOrderDataID = tt.WorkOrderDataID
                    ,
                    WorkOrderID = wo.WorkOrderID
                }).ToList();

                DownloadedPictureManager.Instance.AddPictures("wo" + wo.WorkOrderID, files);
                PictureModel.PictureList = new ObservableCollection<WOFile>(files);
            }
        }

        public async Task<bool> Replace(WOFile fileModel)
        {
            if (fileModel.Data != null)
            {
                //  fileModel.WorkOrderID = wo.WorkOrderID;
                //  fileModel.PersonID = CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID;

                var updateResponse = await workOrderBLL.UpdateFile(new API.Contracts.v1.Requests.WO.UpdateWOFileRequest
                {
                    Data = fileModel.Data,
                    WorkOrderDataID = fileModel.WorkOrderDataID,
                    PersonID = API.MainObjects.Instance.CurrentUser!.PersonID
                });

                await Select();

                return updateResponse.IsValid;
            }

            return false;
        }
    }
}
