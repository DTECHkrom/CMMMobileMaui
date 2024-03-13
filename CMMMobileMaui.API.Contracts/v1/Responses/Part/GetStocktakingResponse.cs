using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetStocktakingResponse
    {
        public int SparePartStocktakingID { get; set; }
        public string Name { get; set; }
        public short AddPersonID { get; set; }
        public short ModPersonID { get; set; }
        public int WarehouseID { get; set; }
    }
}
