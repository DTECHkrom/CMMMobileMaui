using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class PartReturnRequest
    {
        public int PartID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public int StockLevelChangeID
        {
            get;
            set;
        }

        public decimal Quantity
        {
            get;
            set;
        }
    }
}
