using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class WOController : BaseBLL, IWOController
    {
        public WOController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "wo";
        }

        public Task<InvokeReturn<bool>> Close(UpdateWOCloseRequest updateWOClose) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateWOClose));

        public Task<InvokeReturn<CreateWOResponse>> Create(CreateWORequest createWO) =>
            PostAsyncResult<CreateWOResponse>(new QueryMethod().SetJsonObject(createWO));

        public Task<InvokeReturn<CreateWOFileResponse>> CreateFile(CreateWOFileRequest createWOFile) =>
            PostAsyncResult<CreateWOFileResponse>(new QueryMethod().SetJsonObject(createWOFile));

        public Task<InvokeReturn<bool>> DeleteFile(GetByFileIDRequest updateWOFile) =>
            DeleteAsyncResult<bool>(new QueryMethod().AddParams(updateWOFile));

        public Task<InvokeReturn<CreateWOResponse>> Get(GetByWorkOrderIDRequest getByWorkOrderID) =>
            GetAsyncResult<CreateWOResponse>(new QueryMethod().AddParams(getByWorkOrderID));

        public Task<InvokeReturn<CreateWOFileResponse>> GetFile(GetByFileIDRequest getFile) =>
            GetAsyncResult<CreateWOFileResponse>(new QueryMethod().AddParams(getFile));

        public Task<InvokeReturn<IEnumerable<GetWOFilesResponse>>> GetFiles(GetByWorkOrderIDRequest getByWorkOrderID) =>
            GetAsyncResult<IEnumerable<GetWOFilesResponse>>(new QueryMethod().AddParams(getByWorkOrderID));

        public Task<InvokeReturn<IEnumerable<GetWOHistResponse>>> GetHist(GetWOHistRequest getWOHist) =>
            GetAsyncResult<IEnumerable<GetWOHistResponse>>(new QueryMethod().AddParams(getWOHist));

        public Task<InvokeReturn<IEnumerable<GetWOsResponse>>> GetList(GetWOsRequest getWOs) =>
            GetAsyncResult<IEnumerable<GetWOsResponse>>(new QueryMethod().AddParams(getWOs));

        public Task<InvokeReturn<IEnumerable<GetWOPartsResponse>>> GetParts(GetByWorkOrderIDRequest getByWorkOrderID) =>
            GetAsyncResult<IEnumerable<GetWOPartsResponse>>(new QueryMethod().AddParams(getByWorkOrderID));

        public Task<InvokeReturn<IEnumerable<WODictResponse>>> GetDict(GetWODictRequest getDict) =>
            GetAsyncResult<IEnumerable<WODictResponse>>(new QueryMethod().AddParams(getDict));

        public Task<InvokeReturn<bool>> Take(UpdateWOTakeRequest updateWOTake) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateWOTake));

        public Task<InvokeReturn<bool>> Update(UpdateWORequest updateWO) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateWO));

        public Task<InvokeReturn<bool>> UpdateFile(UpdateWOFileRequest updateWOFile) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateWOFile));

        public Task<InvokeReturn<DateTime?>> GetWOPersonTake(GetWOPersonTakeRequest getWOPersonTake) =>
            GetAsyncResult<DateTime?>(new QueryMethod().AddParams(getWOPersonTake));
    }
}
