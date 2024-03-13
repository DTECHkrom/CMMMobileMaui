using System;

namespace CMMMobileMaui.COMMON
{
    public enum ScanMode
    {
        Start,
        Good,
        Fail,
        Empty
    }

    public interface IZebraScanPage
    {
        void AfterScanMethod(string code);

        event EventHandler<ScanMode> OnScanSuccess;

        void EnableScan();
        void DisableScan();
    }
}
