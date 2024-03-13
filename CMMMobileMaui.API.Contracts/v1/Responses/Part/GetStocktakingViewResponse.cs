using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetStocktakingViewResponse
    {
        public int SparePartStocktakingID { get; set; }
        public string StocktakingName { get; set; }
        public DateTime ModDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AddPersonName { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseID { get; set; }
    }
}
