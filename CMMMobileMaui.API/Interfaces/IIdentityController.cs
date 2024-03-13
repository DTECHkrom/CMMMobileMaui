using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Identity;
using CMMMobileMaui.API.Contracts.v1.Responses.Identity;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IIdentityController
    {
        Task<InvokeReturn<LoginResponse>> LoginPass(LoginPassRequest login);
        Task<InvokeReturn<LoginResponse>> LoginCode(LoginCodeRequest login);
    }
}
