using System.Collections.Generic;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class PartTakeRequest
    {
        public List<SinglePartTake> PartTakeList { get; set; }
    }

    public class SinglePartTake
    {
        public int PartID { get; set; }
        public int? WorkOrderID { get; set; }
        public short PersonID { get; set; }
        public int? MachineID { get; set; }
        public decimal Quantity { get; set; }
        public short? OtherPersonID { get; set; }
        public bool IsExchangeable { get; set; }

    }
}
