using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class GetNewPartNoRequest
    {
        public int WarehouseID
        {
            get;
            set;
        }

        public int GeneralCategoryID
        {
            get;
            set;
        }

        public int CategoryID
        {
            get;
            set;
        }
    }
}
