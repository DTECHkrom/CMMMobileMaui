using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.WO
{
    public class UpdateWOFileRequest
    {
        public int WorkOrderDataID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public byte[] Data
        {
            get;
            set;
        }
    }
}
