using System.Collections.Generic;

namespace CMMMobileMaui.SCAN
{
    public interface IScanItems
    {
        IScanIcon? ScanIcon
        {
            get;
        }
        IEnumerable<IScanType> GetScanItems();
    }
}
