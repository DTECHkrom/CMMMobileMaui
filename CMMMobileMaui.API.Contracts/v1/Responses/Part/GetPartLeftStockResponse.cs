using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetPartLeftStockResponse
    {
        public int StockLevelChangeID { get; set; }
        public decimal OriginalValue { get; set; }
        public decimal LeftValue { get; set; }
        public decimal UnitPrice { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyValue { get; set; }
    }
}
