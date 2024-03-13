using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamPartGiverScan : PartBaseScan
    {
        public override ScanType GetScanType() => ScanType.PartGiver;

        public override void GoToPart(GetPartDetailShortResponse part) =>
            COMMON.SConsts
            .GetGlobalAction<GetPartDetailShortResponse>(COMMON.SConsts.PART_SCAN_GIVER)
            .Invoke(part);
    }
}
