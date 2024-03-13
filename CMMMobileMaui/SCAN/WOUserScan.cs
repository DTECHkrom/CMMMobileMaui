using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;

namespace CMMMobileMaui.SCAN
{
    public class WOUserScan : BaseScanType, IScanType
    {
        private readonly string prefix = "u:";
        private readonly List<GetAllUsersResponse> itemsList;
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public WOUserScan(List<GetAllUsersResponse> itemsList)
        {
            this.itemsList = itemsList;
        }

        public string GetParsedCode()
        {
            return code.Replace(prefix, string.Empty);
        }

        public IScanType GetSubScan()
        {
            return null;
        }

        public bool IsValid(string code)
        {
            if (code.ToLower().StartsWith(prefix))
            {
                this.code = code;
                return true;
            }

            return false;
        }

        public async Task ScanMethod()
        {
            if (itemsList != null)
            {
                if (int.TryParse(GetParsedCode(), out int personID))
                {
                    var woItem = itemsList.FirstOrDefault(tt => tt.PersonID == personID);

                    if (woItem != null)
                    {
                        UIMethod?.Invoke(woItem);
                        App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
                    }
                    else
                    {
                        App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
                    }
                }
                else
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
                }
            }
            else
            {
                App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
            }

        }
    }
}
