using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests;
using CMMMobileMaui.API.Contracts.v1.Requests.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IPartController
    {
        //   [Get("/" + ApiRoutes.Part.GetDict)]
        Task<InvokeReturn<IEnumerable<PartDictResponse>>> GetDict(GetDictRequest getDict);

        Task<InvokeReturn<IEnumerable<PartDictResponse>>> GetWars(GetDictRequest getDict);

        // [Get("/" + ApiRoutes.Part.GetCat)]
        Task<InvokeReturn<IEnumerable<GetPartCatResponse>>> GetCat(GetPartCatRequest getPartCat);

        //     [Get("/" + ApiRoutes.Part.GetDetail)]
        Task<InvokeReturn<IEnumerable<GetPartDetailResponse>>> GetDetail(GetPartDetailRequest getPartDetail);

        //      [Get("/" + ApiRoutes.Part.GetDetailShort)]
        Task<InvokeReturn<IEnumerable<GetPartDetailShortResponse>>> GetDetailShort(GetPartDetailShortRequest getPartDetailShort);

        //     [Get("/" + ApiRoutes.Part.GetChange)]
        Task<InvokeReturn<IEnumerable<GetPartChangeResponse>>> GetChange(GetPartChangeRequest getPartChange);

        //      [Get("/" + ApiRoutes.Part.GetLocation)]
        Task<InvokeReturn<IEnumerable<GetPartLocationResponse>>> GetLocation(GetByPartIDRequest getByPartID);

        //     [Get("/" + ApiRoutes.Part.GetWarehouseLocation)]
        Task<InvokeReturn<IEnumerable<GetWarehouseLocationResonse>>> GetWarehouseLocation(GetByWarehouseIDRequest getByWarehouseID);

        //     [Put("/" + ApiRoutes.Part.UpdateTake)]
        Task<InvokeReturn<bool>> Take(PartTakeRequest partTake);

        //    [Put("/" + ApiRoutes.Part.UpdateReturn)]
        Task<InvokeReturn<bool>> Return(PartReturnRequest partReturn);

        //     [Post("/" + ApiRoutes.Part.CreateOrder)]
        Task<InvokeReturn<GetInternalOrderResponse>> CreateOrder(CreatePartOrderRequest createPartOrder);

        //      [Get("/" + ApiRoutes.Part.GetNewPartNo)]
        Task<InvokeReturn<GetNewPartNoResponse>> GetNewPartNo(GetNewPartNoRequest getNewPartNo);

        //      [Get("/" + ApiRoutes.Part.UpdateImage)]
        Task<InvokeReturn<bool>> UpdateImage(UpdatePartImageRequest getNewPartNo);

        //     [Get("/" + ApiRoutes.Part.GetStocktaking)]
        Task<InvokeReturn<IEnumerable<GetStocktakingViewResponse>>> GetStocktaking();

        //      [Get("/" + ApiRoutes.Part.GetPartStocktaking)]
        Task<InvokeReturn<IEnumerable<GetPartStocktakingResponse>>> GetPartStocktaking(GetByStocktakingIDRequest getByStocktakingID);

        //       [Post("/" + ApiRoutes.Part.Create)]
        Task<InvokeReturn<GetSparePartResponse>> Create(CreatePartRequest createPart);

        //      [Post("/" + ApiRoutes.Part.CreateStocktaking)]
        Task<InvokeReturn<GetStocktakingResponse>> CreateStocktaking(CreateStocktakingRequest createStocktaking);

        //      [Post("/" + ApiRoutes.Part.CreateStocktakingPart)]
        Task<InvokeReturn<bool>> CreateStocktakingPart(CreateStocktakingPartRequest createStocktakingPart);

        //      [Delete("/" + ApiRoutes.Part.DeleteStocktakingPart)]
        Task<InvokeReturn<bool>> DeleteStocktakingPart(DeleteStocktakingPartRequest deleteStocktakingPart);

        //      [Delete("/" + ApiRoutes.Part.DeleteStocktaking)]
        Task<InvokeReturn<bool>> DeleteStocktaking(GetByStocktakingIDRequest getByStocktakingID);

        //     [Put("/" + ApiRoutes.Part.UpdateStocktaking)]
        Task<InvokeReturn<bool>> UpdateStocktaking(UpdateStocktakingRequest updateStocktaking);

        //      [Put("/" + ApiRoutes.Part.EndStocktaking)]
        Task<InvokeReturn<bool>> EndStocktaking(UpdateStocktakingEndRequest updateStocktakingEnd);

        //      [Get("/" + ApiRoutes.Part.GetStocktakingByID)]
        Task<InvokeReturn<GetStocktakingResponse>> GetStocktakingByID(GetByStocktakingIDRequest getByStocktakingID);

        //       [Get("/" + ApiRoutes.Part.GetByID)]
        Task<InvokeReturn<GetSparePartResponse>> GetByID(GetByPartIDRequest getByPartID);

        //      [Get("/" + ApiRoutes.Part.GetInternalOrderByID)]
        Task<InvokeReturn<GetInternalOrderResponse>> GetInternalOrderByID(GetByInternalOrderIDRequest getByInternalOrderID);

        //     [Get("/" + ApiRoutes.Part.GetLeftStock)]
        Task<InvokeReturn<IEnumerable<GetPartLeftStockResponse>>> GetLeftStock(GetByPartIDRequest getByPartID);

        //      [Post("/" + ApiRoutes.Part.AddQuantity)]
        Task<InvokeReturn<GetSparePartResponse>> AddQuantity(CreatePartQuantityRequest createPartQuantity);

        Task<InvokeReturn<bool>> UpdateLocation(UpdatePartLocationRequest updatePartLocation);
    }
}
