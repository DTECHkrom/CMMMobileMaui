using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamPartInvScan : PartBaseScan
    {
        public override ScanType GetScanType() => ScanType.PartInv;

        public override void GoToPart(GetPartDetailShortResponse part) =>
            COMMON.SConsts
                .GetGlobalAction<GetPartDetailShortResponse>(COMMON.SConsts.PART_SCAN_INV)
                .Invoke(part);
    }
}
