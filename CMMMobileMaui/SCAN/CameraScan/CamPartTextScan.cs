using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamPartTextScan : TextBaseScan
    {
        public override ScanType GetScanType() => ScanType.PartText;

        public override void GoToText(string text) =>
            COMMON.SConsts.GetGlobalAction<string>(COMMON.SConsts.PART_SCAN_TEXT).Invoke(text);
    }
}