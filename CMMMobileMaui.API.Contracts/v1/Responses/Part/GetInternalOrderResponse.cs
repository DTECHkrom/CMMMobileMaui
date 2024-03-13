using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Part
{
    public class GetInternalOrderResponse
    {
        public int InternalOrderID { get; set; }
        public int? WorkOrderID { get; set; }
        public short PersonID { get; set; }
        public int PartID { get; set; }
        public decimal Quantity { get; set; }
        public int? PartSupplierID { get; set; }
        public int InternalOrderStateID { get; set; }

    }
}
