using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.ActPer;
using CMMMobileMaui.API.Contracts.v1.Responses.ActPer;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IActPerController
    {
        // [Get("/" + ApiRoutes.ActPer.Get)]
        Task<InvokeReturn<CreateWOActivityPersonResponse>> Get(GetByWOActivityPersonIDRequest getByWOActivityPersonID);

        //   [Post("/" + ApiRoutes.ActPer.Create)]
        Task<InvokeReturn<CreateWOActivityPersonResponse>> Create(CreateWOActivityPersonRequest createWOActivityPerson);

        //  [Delete("/" + ApiRoutes.ActPer.Delete)]
        Task<InvokeReturn<bool>> Delete(DeleteWOActivityPersonRequest deleteWOActivityPerson);
    }
}
