using CMMMobileMaui.SCAN;
using CMMMobileMaui.VIEW;
using System.Windows.Input;

namespace CMMMobileMaui.COMMON
{
    public abstract class ScannerViewModelBase : ViewModelBase, IScanItems
    {
        public IScanIcon? ScanIcon
        {
            get;
            private set;
        }

        public ScannerViewModelBase()
        {
            TapCommand = new Command(async () =>
            {
                if(CanClick())
                {
                    ScanView scanView = new ScanView(GetScanItems());
                    await OpenModalPage(scanView);
                }
            });
        }

        public abstract IEnumerable<IScanType> GetScanItems();

        public void InitScannerOnPage(IScanIcon? scanIcon)
        {
            ScanIcon = scanIcon;

            if(ScanIcon != null)
            {
                ScanIcon.TapCommand = TapCommand;
            }

            App.CurrentScanManager?.Init(this);
        }

        public ICommand TapCommand
        {
            get;
        }
    }
}
