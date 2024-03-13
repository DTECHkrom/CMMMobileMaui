using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.ActPer
{
    public class DeleteWOActivityPersonRequest
    {
        public int ActivityPersonID
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
