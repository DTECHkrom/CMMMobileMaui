using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Identity;
using CMMMobileMaui.API.Contracts.v1.Responses.Identity;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers
{
    public class IdentityController : BaseBLL, IIdentityController
    {
        public IdentityController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "identity";
            this.Authentication = AuthenticationType.Basic;
        }

        public Task<InvokeReturn<LoginResponse>> LoginCode(LoginCodeRequest login) =>
            GetAsyncResult<LoginResponse>(new QueryMethod().AddParams(login));

        public Task<InvokeReturn<LoginResponse>> LoginPass(LoginPassRequest login) =>
            GetAsyncResult<LoginResponse>(new QueryMethod().AddParams(login));
    }
}
