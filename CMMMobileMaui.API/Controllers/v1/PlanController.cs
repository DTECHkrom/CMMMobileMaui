using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Act;
using CMMMobileMaui.API.Contracts.v1.Requests.Plan;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;
using CMMMobileMaui.API.Contracts.v1.Responses.Plan;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class PlanController : BaseBLL, IPlanController
    {
        public PlanController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "plan";
        }

        public Task<InvokeReturn<CreateWOActivityResponse>> CreateAct(CreatePlanActivityRequest createPlanActivity) =>
            PostAsyncResult<CreateWOActivityResponse>(new QueryMethod().SetJsonObject(createPlanActivity));

        public Task<InvokeReturn<CreateWOActivityResponse>> GetAct(GetByWOActivityIDRequest getByWOActivityID) =>
            GetAsyncResult<CreateWOActivityResponse>(new QueryMethod().AddParams(getByWOActivityID));

        public Task<InvokeReturn<IEnumerable<GetWOPlanActsResponse>>> GetActs(GetWOPlanActsRequest getWOPlanActs) =>
            GetAsyncResult<IEnumerable<GetWOPlanActsResponse>>(new QueryMethod().AddParams(getWOPlanActs));
    }
}
