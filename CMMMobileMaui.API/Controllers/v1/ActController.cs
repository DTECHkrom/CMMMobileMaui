using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Act;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class ActController : BaseBLL, IActController
    {
        public ActController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "act";
        }

        public Task<InvokeReturn<CreateWOActivityResponse>> Create(CreateWOActivityRequest createWOActivity) =>
            PostAsyncResult<CreateWOActivityResponse>(new QueryMethod().SetJsonObject(createWOActivity));

        public Task<InvokeReturn<bool>> Delete(DeleteWOActivityRequest deleteWOActivity) =>
            DeleteAsyncResult<bool>(new QueryMethod().AddParams(deleteWOActivity));

        public Task<InvokeReturn<CreateWOActivityResponse>> Get(GetByWOActivityIDRequest getByWOActivityID) =>
            GetAsyncResult<CreateWOActivityResponse>(new QueryMethod().AddParams(getByWOActivityID));

        public Task<InvokeReturn<IEnumerable<WOActDictResponse>>> GetDict(GetActDictRequest getDict) =>
            GetAsyncResult<IEnumerable<WOActDictResponse>>(new QueryMethod().AddParams(getDict));

        public Task<InvokeReturn<IEnumerable<GetWOActsResponse>>> GetList(GetWOActsRequest getWOActs) =>
            GetAsyncResult<IEnumerable<GetWOActsResponse>>(new QueryMethod().AddParams(getWOActs));
    }
}
