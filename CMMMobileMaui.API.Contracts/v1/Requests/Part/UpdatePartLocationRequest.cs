using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class UpdatePartLocationRequest
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

        public SinglePartLocation OldLocation
        {
            get;
            set;
        }

        public SinglePartLocation NewLocation
        {
            get;
            set;
        }
    }

    public class SinglePartLocation 
    { 
        public int RackID
        {
            get;
            set;
        }
    }
}
