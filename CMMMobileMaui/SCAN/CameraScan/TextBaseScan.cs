using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal abstract class TextBaseScan : IScanBaseType
    {
        private string? code;
        public abstract ScanType GetScanType();

        public bool IsValid(string code)
        {
            this.code = code;
            return true;
        }

        public async Task<bool> ScanMethod()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                GoToText(code!);
            });

            return true;
        }

        public abstract void GoToText(string text);
    }
}
