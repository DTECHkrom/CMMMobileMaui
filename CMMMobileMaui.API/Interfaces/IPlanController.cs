using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Act;
using CMMMobileMaui.API.Contracts.v1.Requests.Plan;
using CMMMobileMaui.API.Contracts.v1.Responses.Act;
using CMMMobileMaui.API.Contracts.v1.Responses.Plan;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IPlanController
    {
        // [Get("/" + ApiRoutes.Plan.GetActs)]
        Task<InvokeReturn<IEnumerable<GetWOPlanActsResponse>>> GetActs(GetWOPlanActsRequest getWOPlanActs);

        //  [Post("/" + ApiRoutes.Plan.CreateAct)]
        Task<InvokeReturn<CreateWOActivityResponse>> CreateAct(CreatePlanActivityRequest createPlanActivity);

        // [Get("/" + ApiRoutes.Plan.GetAct)]
        Task<InvokeReturn<CreateWOActivityResponse>> GetAct(GetByWOActivityIDRequest getByWOActivityID);
    }
}
