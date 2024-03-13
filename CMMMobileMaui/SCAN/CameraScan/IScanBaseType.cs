using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    public interface IScanBaseType
    {
        bool IsValid(string code);
        Task<bool> ScanMethod();
        ScanType GetScanType();
    }
}
