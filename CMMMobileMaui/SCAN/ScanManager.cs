using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.SCAN
{
    public class ScanManager : IZebraScanPage
    {
        #region FIELDS

        private Timer? _subTimer = null;
        private bool isBusy = false;

        public IScanItems? ScanItems
        {
            get;
            private set;
        }

        private IScannerService scaner;

        // private IEnumerable<IScanType> scanTypesOnPage;

        private IScanType? currentSubScan;

        private readonly WeakEventManager eventWeakManager = new WeakEventManager();

        public event EventHandler<ScanMode> OnScanSuccess
        {
            add => eventWeakManager.AddEventHandler(value);
            remove => eventWeakManager.RemoveEventHandler(value);
        }

        #endregion

        #region Cstr

        public ScanManager(IScannerService scaner)
        {
            this.scaner = scaner;
            this.currentSubScan = null;
            this.scaner.ItemScanned = ScanerOnItemScanned;
        }

        #endregion

        #region METHOD InitScanIcon

        public void Init(IScanItems scanItems)
        {
            ScanItems = scanItems;
        }

        #endregion

        #region METHOD CanProcessScan

        private bool CanProcessScan() =>
            ScanItems is not null
            && ScanItems.GetScanItems().Any()
            && isBusy == false;

        #endregion

        #region METHOD AfterScanMethod

        public async void AfterScanMethod(string code)
        {
            if (!CanProcessScan())
                return;

            isBusy = true;

            ShowScanResult(ScanMode.Start);

            if (currentSubScan != null)
            {
                if (currentSubScan.IsValid(code))
                {
                    var subScan = currentSubScan.GetSubScan();

                    await currentSubScan.ScanMethod();

                    if (subScan != null)
                    {
                        InitSubTimer();
                    }

                    currentSubScan = subScan;
                    isBusy = false;
                    return;
                }
            }

            currentSubScan = null;

            bool wasGoodScan = false;

            var scanTypes = ScanItems!.GetScanItems();

            foreach (var scanType in scanTypes)
            {
                if (scanType.IsValid(code))
                {
                    currentSubScan = scanType.GetSubScan();

                    await scanType.ScanMethod();

                    if (currentSubScan != null)
                    {
                        InitSubTimer();
                    }

                    wasGoodScan = true;

                    break;
                }
            }

            if (!wasGoodScan)
                ShowScanResult(ScanMode.Fail);
            else
                ShowScanResult(ScanMode.Empty);

            isBusy = false;
        }

        #endregion

        #region METHOD InitSubTimer

        private void InitSubTimer()
        {
            if (_subTimer != null)
            {
                _subTimer.Dispose();
                _subTimer = null;
            }

            _subTimer = new Timer((item) =>
            {
                if (currentSubScan != null)
                {
                    currentSubScan = null;
                }

                eventWeakManager.HandleEvent(this, ScanMode.Empty, nameof(OnScanSuccess));

                try
                {
                    isBusy = false;

                    if (_subTimer != null)
                    {
                        _subTimer.Dispose();
                        _subTimer = null;
                    }
                }
                catch (Exception) { }

            }, null, 5000, 0);
        }

        #endregion

        #region METHOD ShowScanResult
        public void ShowScanResult(ScanMode value)
        {
            ScanItems!.ScanIcon?.SetScanColorForMode(value);         
            eventWeakManager.HandleEvent(this, value, nameof(OnScanSuccess));
        }

        #endregion

        #region METHOD DisableScan

        public void DisableScan()
        {
            if(ScanItems is null)
                return;

           // ScanItems!.ScanIcon?.StopScanAnimation();

         //   this.ScanItems = null;
            this.scaner.ItemScanned = null;
        }

        #endregion

        #region METHOD EnableScan

        public void EnableScan()
        {
            if (ScanItems is null)
                return;

         //   ScanItems!.ScanIcon?.StartScanAnimation();
            this.scaner.ItemScanned = ScanerOnItemScanned;
        }

        #endregion

        #region METHOD ScanerOnItemScanned

        private void ScanerOnItemScanned(string code)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AfterScanMethod(code);
            });
        }

        #endregion
    }
}
