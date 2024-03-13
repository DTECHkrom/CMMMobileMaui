using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.Act
{
    public class DeleteWOActivityRequest
    {
        public int ActivityID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }
    }
}
