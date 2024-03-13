using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Requests.WO
{
    public class CreateWOFileRequest
    {
        public int WorkOrderID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public string FileNameWithExtension
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
