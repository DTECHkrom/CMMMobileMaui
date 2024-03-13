using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Other;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class OtherController : BaseBLL, IOtherController
    {
        public OtherController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "other";
            this.Authentication = AuthenticationType.Basic;
        }
        public Task<InvokeReturn<bool>> GetIsSetUsed() =>
            GetAsyncResult<bool>(new QueryMethod());

        public Task<InvokeReturn<IEnumerable<GetAllUsersResponse>>> GetUsersList() =>
            GetAsyncResult<IEnumerable<GetAllUsersResponse>>(new QueryMethod());

        public Task<InvokeReturn<GetAppSettResponse>> GetAppSett() =>
            GetAsyncResult<GetAppSettResponse>(new QueryMethod());

        public Task<InvokeReturn<IEnumerable<GetAppDictResponse>>> GetDict(GetAppDictRequest getDict) =>
            GetAsyncResult<IEnumerable<GetAppDictResponse>>(new QueryMethod().AddParams(getDict));

        public Task<InvokeReturn<GetLastVersionResponse>> GetLastVersion() =>
            GetAsyncResult<GetLastVersionResponse>(new QueryMethod());

        public Task<InvokeReturn<bool>> IsHost() =>
            GetAsyncResult<bool>(new QueryMethod());
    }
}
