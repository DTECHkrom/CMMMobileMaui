using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.Models.COMMON
{
    public static class Enums
    {
        public enum SortDirection : int
        {
            Up = 0,
            Down = 1
        }

        public enum ObservedDeviceListType: int
        {
            Active = 1,
            History = 2
        }
    }
}
