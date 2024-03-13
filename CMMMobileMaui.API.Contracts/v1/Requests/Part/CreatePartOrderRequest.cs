using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class CreatePartOrderRequest
    {
        public int PartID { get; set; }
        public short PersonID { get; set; }
        public int? WorkOrderID { get; set; }
        public decimal Quantity { get; set; }
        //TODO add to app
        public string Description { get; set; }
        //TODO add to app
        public DateTime? ExpectedDate { get; set; }   
    }
}
