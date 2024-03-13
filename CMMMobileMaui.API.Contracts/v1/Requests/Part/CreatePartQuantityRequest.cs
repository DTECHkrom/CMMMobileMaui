using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class CreatePartQuantityRequest
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

        public int CurrencyID
        {
            get;
            set;
        }

        public decimal Quantity
        {
            get;
            set;
        }

        public decimal? UnitPrice
        {
            get;
            set;
        }
    }
}
