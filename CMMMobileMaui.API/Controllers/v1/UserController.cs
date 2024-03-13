using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.User;
using CMMMobileMaui.API.Contracts.v1.Responses.User;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class UserController : BaseBLL, IUserController
    {
        public UserController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "user";
        }

        public Task<InvokeReturn<GetUserLoginResponse>> GetLoginByCode(GetUserLoginByCodeRequest getUserLoginByCode) =>
            GetAsyncResult<GetUserLoginResponse>(new QueryMethod().AddParams(getUserLoginByCode));

        public Task<InvokeReturn<GetUserLoginResponse>> GetLoginByPass(GetUserLoginByPasswordRequest getUserLoginByPassword) =>
            GetAsyncResult<GetUserLoginResponse>(new QueryMethod().AddParams(getUserLoginByPassword));

        public Task<InvokeReturn<GetUserObservedDeviceResponse>> GetObservedDevices(GetUserObservedDeviceRequest getUserObservedDevice) =>
            GetAsyncResult<GetUserObservedDeviceResponse>(new QueryMethod().AddParams(getUserObservedDevice));

        public Task<InvokeReturn<GetUserLoginResponse>> GetUser(GetByPersonIDRequest personID) =>
            GetAsyncResult<GetUserLoginResponse>(new QueryMethod().AddParams(personID));

        public Task<InvokeReturn<bool>> UpdateCode(UpdateUserCodeRequest postUserCode) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(postUserCode));
    }
}
