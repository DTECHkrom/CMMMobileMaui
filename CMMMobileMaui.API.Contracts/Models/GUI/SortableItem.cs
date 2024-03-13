using Newtonsoft.Json;
using System.Collections.Generic;
using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    public class SortableItem
    {
        [FilterIgnore, JsonIgnore]
        public List<SortItem> SortingPropertyList { get; set; }

        [FilterIgnore, JsonIgnore]
        public SortItem DefaultSortItem { get; set; }
    }
}
