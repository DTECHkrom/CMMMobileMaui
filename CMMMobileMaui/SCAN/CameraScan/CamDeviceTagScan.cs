using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamDeviceTagScan : TextBaseScan
    {
        public override ScanType GetScanType() => 
            ScanType.DevText;
        public override void GoToText(string text) =>
                        COMMON.SConsts.GetGlobalAction<string>(COMMON.SConsts.DEV_SCAN_TAG).Invoke(text);
    }
}
