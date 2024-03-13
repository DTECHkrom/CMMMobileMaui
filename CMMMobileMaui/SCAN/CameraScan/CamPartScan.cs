using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamPartScan : PartBaseScan
    {
        public override ScanType GetScanType() => ScanType.Part;

        public override void GoToPart(GetPartDetailShortResponse part) => 
            COMMON.SConsts
            .GetGlobalAction<GetPartDetailShortResponse>(COMMON.SConsts.PART_SCAN)                           
            .Invoke(part);
    }
}
