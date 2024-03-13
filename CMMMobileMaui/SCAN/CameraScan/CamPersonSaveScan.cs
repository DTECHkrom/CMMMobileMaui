using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamPersonSaveScan : IScanBaseType
    {
        private string? code;
        public ScanType GetScanType() => ScanType.Person;

        public bool IsValid(string code)
        {
            this.code = code;
            return true;
        }

        public async Task<bool> ScanMethod()
        {
            if (App.CompanyData != null)
            {
                bool isOk = await App.CompanyData.SaveUserLogin(code!);

                if (isOk)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        COMMON.SConsts.GetGlobalAction(COMMON.SConsts.SUB_SCAN_PERSON_SAVE)?.Invoke();
                    });

                    return isOk;
                }
            }

            return false;
        }
    }
}
