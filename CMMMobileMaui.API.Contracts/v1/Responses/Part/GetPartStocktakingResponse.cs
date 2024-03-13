using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetPartStocktakingResponse
    {
        public int PartID { get; set; }
        public string PartNo { get; set; }
        public decimal OldValue { get; set; }
        public decimal NewValue { get; set; }
        public decimal? CurrentValue { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsSet { get; set; }
        public string ModPersonName { get; set; }
        public DateTime ModDate { get; set; }
        public decimal? CurrencyValue { get; set; }
        public string CurrencyName { get; set; }
        public string UnitName { get; set; }
    }
}
