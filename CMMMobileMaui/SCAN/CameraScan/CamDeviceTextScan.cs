using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamDeviceTextScan : TextBaseScan
    {
        public override ScanType GetScanType() => 
            ScanType.Text;
        public override void GoToText(string text) =>
            COMMON.SConsts.GetGlobalAction<string>(COMMON.SConsts.DEV_SCAN_TEXT).Invoke(text);
    }
}
