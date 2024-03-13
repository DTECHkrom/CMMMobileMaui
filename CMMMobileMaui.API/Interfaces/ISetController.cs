using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.Set;

namespace CMMMobileMaui.API.Interfaces
{
    public interface ISetController
    {
        //  [Get("/" + ApiRoutes.Set.GetIsSetUsed)]
        

        //  [Get("/" + ApiRoutes.Set.GetSets)]
        Task<InvokeReturn<IEnumerable<GetSetsResponse>>> GetSets();

        //   [Get("/" + ApiRoutes.Set.GetSubSets)]
        Task<InvokeReturn<IEnumerable<GetSubSetsResponse>>> GetSubSets();
    }
}
