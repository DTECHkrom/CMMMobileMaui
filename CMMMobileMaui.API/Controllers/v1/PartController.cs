using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests;
using CMMMobileMaui.API.Contracts.v1.Requests.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class PartController : BaseBLL, IPartController
    {
        public PartController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "part";
        }

        public Task<InvokeReturn<GetSparePartResponse>> AddQuantity(CreatePartQuantityRequest createPartQuantity) =>
            PostAsyncResult<GetSparePartResponse>(new QueryMethod().SetJsonObject(createPartQuantity));

        public Task<InvokeReturn<GetInternalOrderResponse>> CreateOrder(CreatePartOrderRequest createPartOrder) =>
            PostAsyncResult<GetInternalOrderResponse>(new QueryMethod().SetJsonObject(createPartOrder));

        public Task<InvokeReturn<GetSparePartResponse>> Create(CreatePartRequest createPart) =>
            PostAsyncResult<GetSparePartResponse>(new QueryMethod().SetJsonObject(createPart));

        public Task<InvokeReturn<GetStocktakingResponse>> CreateStocktaking(CreateStocktakingRequest createStocktaking) =>
            PostAsyncResult<GetStocktakingResponse>(new QueryMethod().SetJsonObject(createStocktaking));

        public Task<InvokeReturn<bool>> CreateStocktakingPart(CreateStocktakingPartRequest createStocktakingPart) =>
            PostAsyncResult<bool>(new QueryMethod().SetJsonObject(createStocktakingPart));

        public Task<InvokeReturn<bool>> DeleteStocktaking(GetByStocktakingIDRequest getByStocktakingID) =>
            DeleteAsyncResult<bool>(new QueryMethod().AddParams(getByStocktakingID));

        public Task<InvokeReturn<bool>> DeleteStocktakingPart(DeleteStocktakingPartRequest deleteStocktakingPart) =>
            DeleteAsyncResult<bool>(new QueryMethod().AddParams(deleteStocktakingPart));

        public Task<InvokeReturn<bool>> EndStocktaking(UpdateStocktakingEndRequest updateStocktakingEnd) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateStocktakingEnd));

        public Task<InvokeReturn<GetSparePartResponse>> GetByID(GetByPartIDRequest getByPartID) =>
            GetAsyncResult<GetSparePartResponse>(new QueryMethod().AddParams(getByPartID));

        public Task<InvokeReturn<IEnumerable<GetPartCatResponse>>> GetCat(GetPartCatRequest getPartCat) =>
            GetAsyncResult<IEnumerable<GetPartCatResponse>>(new QueryMethod().AddParams(getPartCat));

        public Task<InvokeReturn<IEnumerable<GetPartChangeResponse>>> GetChange(GetPartChangeRequest getPartChange) =>
            GetAsyncResult<IEnumerable<GetPartChangeResponse>>(new QueryMethod().AddParams(getPartChange));

        public Task<InvokeReturn<IEnumerable<GetPartDetailResponse>>> GetDetail(GetPartDetailRequest getPartDetail) =>
            GetAsyncResult<IEnumerable<GetPartDetailResponse>>(new QueryMethod().AddParams(getPartDetail));

        public Task<InvokeReturn<IEnumerable<GetPartDetailShortResponse>>> GetDetailShort(GetPartDetailShortRequest getPartDetailShort) =>
            GetAsyncResult<IEnumerable<GetPartDetailShortResponse>>(new QueryMethod().AddParams(getPartDetailShort));

        public Task<InvokeReturn<IEnumerable<PartDictResponse>>> GetDict(GetDictRequest getDict) =>
            GetAsyncResult<IEnumerable<PartDictResponse>>(new QueryMethod().AddParams(getDict));
        public Task<InvokeReturn<IEnumerable<PartDictResponse>>> GetWars(GetDictRequest getDict) =>
            GetAsyncResult<IEnumerable<PartDictResponse>>(new QueryMethod().AddParams(getDict));

        public Task<InvokeReturn<GetInternalOrderResponse>> GetInternalOrderByID(GetByInternalOrderIDRequest getByInternalOrderID) =>
            GetAsyncResult<GetInternalOrderResponse>(new QueryMethod().AddParams(getByInternalOrderID));

        public Task<InvokeReturn<IEnumerable<GetPartLeftStockResponse>>> GetLeftStock(GetByPartIDRequest getByPartID) =>
            GetAsyncResult<IEnumerable<GetPartLeftStockResponse>>(new QueryMethod().AddParams(getByPartID));

        public Task<InvokeReturn<IEnumerable<GetPartLocationResponse>>> GetLocation(GetByPartIDRequest getByPartID) =>
            GetAsyncResult<IEnumerable<GetPartLocationResponse>>(new QueryMethod().AddParams(getByPartID));

        public Task<InvokeReturn<GetNewPartNoResponse>> GetNewPartNo(GetNewPartNoRequest getNewPartNo) =>
            GetAsyncResult<GetNewPartNoResponse>(new QueryMethod().AddParams(getNewPartNo));

        public Task<InvokeReturn<IEnumerable<GetPartStocktakingResponse>>> GetPartStocktaking(GetByStocktakingIDRequest getByStocktakingID) =>
            GetAsyncResult<IEnumerable<GetPartStocktakingResponse>>(new QueryMethod().AddParams(getByStocktakingID));

        public Task<InvokeReturn<IEnumerable<GetStocktakingViewResponse>>> GetStocktaking() =>
            GetAsyncResult<IEnumerable<GetStocktakingViewResponse>>(new QueryMethod());

        public Task<InvokeReturn<GetStocktakingResponse>> GetStocktakingByID(GetByStocktakingIDRequest getByStocktakingID) =>
            GetAsyncResult<GetStocktakingResponse>(new QueryMethod().AddParams(getByStocktakingID));

        public Task<InvokeReturn<IEnumerable<GetWarehouseLocationResonse>>> GetWarehouseLocation(GetByWarehouseIDRequest getByWarehouseID) =>
            GetAsyncResult<IEnumerable<GetWarehouseLocationResonse>>(new QueryMethod().AddParams(getByWarehouseID));

        public Task<InvokeReturn<bool>> Return(PartReturnRequest partReturn) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(partReturn));

        public Task<InvokeReturn<bool>> Take(PartTakeRequest partTake) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(partTake));

        public Task<InvokeReturn<bool>> UpdateImage(UpdatePartImageRequest getNewPartNo) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(getNewPartNo));

        public Task<InvokeReturn<bool>> UpdateLocation(UpdatePartLocationRequest updatePartLocation) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updatePartLocation));

        public Task<InvokeReturn<bool>> UpdateStocktaking(UpdateStocktakingRequest updateStocktaking) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateStocktaking));
    }
}
