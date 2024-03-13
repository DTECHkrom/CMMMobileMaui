using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;

namespace CMMMobileMaui.API
{
    public static class Extensions
    {
        public static List<DictBase> ToDictBaseList(this List<WODictResponse> list) =>
            list.Cast<DictBase>()
            .OrderBy(tt => tt.Name)
            .ToList();
    }
}
