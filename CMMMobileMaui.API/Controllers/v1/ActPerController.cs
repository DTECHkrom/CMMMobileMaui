using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.ActPer;
using CMMMobileMaui.API.Contracts.v1.Responses.ActPer;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class ActPerController : BaseBLL, IActPerController
    {
        public ActPerController(IAPIManage apiManage) : base(apiManage)
        {
            Name = "actper";
        }

        public Task<InvokeReturn<CreateWOActivityPersonResponse>> Create(CreateWOActivityPersonRequest createWOActivityPerson) =>
            PostAsyncResult<CreateWOActivityPersonResponse>(new QueryMethod().SetJsonObject(createWOActivityPerson));

        public Task<InvokeReturn<bool>> Delete(DeleteWOActivityPersonRequest deleteWOActivityPerson) =>
            DeleteAsyncResult<bool>(new QueryMethod().AddParams(deleteWOActivityPerson));

        public Task<InvokeReturn<CreateWOActivityPersonResponse>> Get(GetByWOActivityPersonIDRequest getByWOActivityPersonID) =>
            GetAsyncResult<CreateWOActivityPersonResponse>(new QueryMethod().AddParams(getByWOActivityPersonID));
    }
}
