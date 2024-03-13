using CMMMobileMaui.COMMON;
using System.Windows.Input;

namespace CMMMobileMaui.SCAN
{
    public interface IScanIcon
    {
        ICommand TapCommand
        {
            get;
            set;
        }
        void SetScanColorForMode(ScanMode mode);
        //void StartScanAnimation();

        //void StopScanAnimation();
    }
}
