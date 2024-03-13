using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Act;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IActController
    {
        //  [Get("/" + ApiRoutes.Act.GetDict)]
        Task<InvokeReturn<IEnumerable<WOActDictResponse>>> GetDict(GetActDictRequest getDict);

        //  [Get("/" + ApiRoutes.Act.GetList)]
        Task<InvokeReturn<IEnumerable<GetWOActsResponse>>> GetList(GetWOActsRequest getWOActs);

        //  [Post("/" + ApiRoutes.Act.Create)]
        Task<InvokeReturn<CreateWOActivityResponse>> Create(CreateWOActivityRequest createWOActivity);

        //  [Get("/" + ApiRoutes.Act.Get)]
        Task<InvokeReturn<CreateWOActivityResponse>> Get(GetByWOActivityIDRequest getByWOActivityID);

        //   [Delete("/" + ApiRoutes.Act.Delete)]
        Task<InvokeReturn<bool>> Delete(DeleteWOActivityRequest deleteWOActivity);
    }
}
