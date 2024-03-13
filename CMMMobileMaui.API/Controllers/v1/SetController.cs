using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.Set;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class SetController : BaseBLL, ISetController
    {
        public SetController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "set";
        }

        public Task<InvokeReturn<IEnumerable<GetSetsResponse>>> GetSets() =>
            GetAsyncResult<IEnumerable<GetSetsResponse>>(new QueryMethod());

        public Task<InvokeReturn<IEnumerable<GetSubSetsResponse>>> GetSubSets() =>
            GetAsyncResult<IEnumerable<GetSubSetsResponse>>(new QueryMethod());
    }
}
