using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CameraScanManager
    {
        private static Lazy<CameraScanManager> _instance = new(() => new CameraScanManager());
        public static CameraScanManager Instance => _instance.Value;

        private List<IScanBaseType> scanTypes = new List<IScanBaseType>
        {
            new CamDeviceScan(),
            new CamDeviceMainScan(),
            new CamPartScan(),
            new CamPartInvScan(),
            new CamPartGiverScan(),
            new CamPersonLoginScan(),
            new CamPersonSaveScan(),
            new CamDeviceTextScan(),
            new CamPartTextScan(),
        };

        private CameraScanManager()
        {
        }

        public async void Scan(ScanType scanType, string code)
        {
            var scanMethod = GetScanMethod(scanType);

            if (scanMethod is not null)
            {
                if (scanMethod.IsValid(code))
                {
                    await scanMethod.ScanMethod();
                }
            }
        }

        public IScanBaseType? GetScanMethod(ScanType scanType) =>
            scanTypes.FirstOrDefault(x => x.GetScanType() == scanType);
    }
}