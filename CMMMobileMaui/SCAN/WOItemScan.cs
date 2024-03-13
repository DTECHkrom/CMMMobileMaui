using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Responses;

namespace CMMMobileMaui.SCAN
{
    public class WOItemScan : BaseScanType, IScanType
    {
        private readonly string prefix;
        private List<DictBase> itemsList;
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public WOItemScan(string prefix, List<DictBase> itemsList)
        {
            this.prefix = prefix;
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
            if(code.ToLower().StartsWith(prefix))
            {
                this.code = code;
                return true;
            }

            return false;
        }

        public async Task ScanMethod()
        {
            Regex reg = new Regex(@"\:(\d+)\:");
            var result = reg.Match(code);

            if(result != null 
                && itemsList != null)
            {
                var id = result.Groups[1].Value;

                if (int.TryParse(id, out int wocID))
                {
                    var woItem = itemsList.FirstOrDefault(tt => tt.ID == wocID);

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
