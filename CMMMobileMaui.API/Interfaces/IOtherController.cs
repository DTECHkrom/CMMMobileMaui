using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Other;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IOtherController
    {
        // [Get("/" + ApiRoutes.Other.GetAppSett)]
        Task<InvokeReturn<GetAppSettResponse>> GetAppSett();
        //   [Get("/" + ApiRoutes.Other.GetBranchLocations)]
        Task<InvokeReturn<bool>> GetIsSetUsed();
        Task<InvokeReturn<IEnumerable<GetAllUsersResponse>>> GetUsersList();

        Task<InvokeReturn<GetLastVersionResponse>> GetLastVersion();
        Task<InvokeReturn<bool>> IsHost();
        Task<InvokeReturn<IEnumerable<GetAppDictResponse>>> GetDict(GetAppDictRequest getDict);
    }
}
