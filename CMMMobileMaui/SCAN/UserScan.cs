namespace CMMMobileMaui.SCAN
{
    public class UserScan : BaseScanType, IScanType
    {
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public UserScan()
        {
        }

        public string GetParsedCode()
        {
            return code;
        }

        public IScanType GetSubScan()
        {
            return null;
        }

        public bool IsValid(string code)
        {
            this.code = code;
            return true;
        }

        public async Task ScanMethod()
        {
            bool isOk = await App.CompanyData.ScanUserLogin(code);

            if (isOk)
            {
                App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);

                UIMethod?.Invoke(null);
            }
            else
            {
                App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
            }
        }
    }
}
