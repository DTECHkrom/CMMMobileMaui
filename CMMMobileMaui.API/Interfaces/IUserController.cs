using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.User;
using CMMMobileMaui.API.Contracts.v1.Responses.User;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IUserController
    {
        Task<InvokeReturn<GetUserLoginResponse>> GetLoginByCode(GetUserLoginByCodeRequest getUserLoginByCode);
        Task<InvokeReturn<GetUserLoginResponse>> GetLoginByPass(GetUserLoginByPasswordRequest getUserLoginByPassword);
        Task<InvokeReturn<bool>> UpdateCode(UpdateUserCodeRequest postUserCode);
        Task<InvokeReturn<GetUserLoginResponse>> GetUser(GetByPersonIDRequest personID);
        Task<InvokeReturn<GetUserObservedDeviceResponse>> GetObservedDevices(GetUserObservedDeviceRequest getUserObservedDevice);
    }
}
