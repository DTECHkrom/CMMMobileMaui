using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IWOController
    {
        // [Get("/" + ApiRoutes.WO.GetDict)]
        Task<InvokeReturn<IEnumerable<WODictResponse>>> GetDict(GetWODictRequest getDict);

        //   [Get("/" + ApiRoutes.WO.GetList)]
        Task<InvokeReturn<IEnumerable<GetWOsResponse>>> GetList(GetWOsRequest getWOs);

        // [Get("/" + ApiRoutes.WO.GetHist)]
        Task<InvokeReturn<IEnumerable<GetWOHistResponse>>> GetHist(GetWOHistRequest getWOHist);

        //  [Get("/" + ApiRoutes.WO.GetFiles)]
        Task<InvokeReturn<IEnumerable<GetWOFilesResponse>>> GetFiles(GetByWorkOrderIDRequest getByWorkOrderID);

        //  [Get("/" + ApiRoutes.WO.GetParts)]
        Task<InvokeReturn<IEnumerable<GetWOPartsResponse>>> GetParts(GetByWorkOrderIDRequest getByWorkOrderID);

        //    [Get("/" + ApiRoutes.WO.GetFile)]
        Task<InvokeReturn<CreateWOFileResponse>> GetFile(GetByFileIDRequest getFile);

        //   [Post("/" + ApiRoutes.WO.CreateFile)]
        Task<InvokeReturn<CreateWOFileResponse>> CreateFile(CreateWOFileRequest createWOFile);

        //  [Put("/" + ApiRoutes.WO.UpdateFile)]
        Task<InvokeReturn<bool>> UpdateFile(UpdateWOFileRequest updateWOFile);

        //  [Delete("/" + ApiRoutes.WO.DeleteFile)]
        Task<InvokeReturn<bool>> DeleteFile(GetByFileIDRequest updateWOFile);

        //   [Put("/" + ApiRoutes.WO.Take)]
        Task<InvokeReturn<bool>> Take(UpdateWOTakeRequest updateWOTake);

        //   [Put("/" + ApiRoutes.WO.Close)]
        Task<InvokeReturn<bool>> Close(UpdateWOCloseRequest updateWOClose);

        //    [Get("/" + ApiRoutes.WO.Get)]
        Task<InvokeReturn<CreateWOResponse>> Get(GetByWorkOrderIDRequest getByWorkOrderID);

        //   [Post("/" + ApiRoutes.WO.Create)]
        Task<InvokeReturn<CreateWOResponse>> Create(CreateWORequest createWO);

        //     [Put("/" + ApiRoutes.WO.Update)]
        Task<InvokeReturn<bool>> Update(UpdateWORequest updateWO);
        Task<InvokeReturn<DateTime?>> GetWOPersonTake(GetWOPersonTakeRequest getWOPersonTake);
    }
}
