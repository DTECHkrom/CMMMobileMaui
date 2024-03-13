using System;
using System.Threading.Tasks;

namespace CMMMobileMaui.SCAN
{
    public class UserSaveScan : BaseScanType, IScanType
    {
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public UserSaveScan()
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
            bool isOK = await App.CompanyData.SaveUserLogin(code);

            if (isOK)
            {
                App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
            }
            else
            {
                App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
            }
        }
    }
}
