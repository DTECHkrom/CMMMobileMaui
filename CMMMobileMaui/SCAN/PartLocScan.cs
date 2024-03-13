using CMMMobileMaui.API.Contracts.v1.Responses.Part;

namespace CMMMobileMaui.SCAN
{
    public class PartLocScan : BaseScanType, IScanType
    {
        private readonly string prefix = "pl:";
        private List<GetWarehouseLocationResonse> itemsList;
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public PartLocScan(List<GetWarehouseLocationResonse> itemsList)
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
            if (code.StartsWith(prefix))
            {
                this.code = code;
                return true;
            }

            return false;
        }

        public async Task ScanMethod()
        {
            if (int.TryParse(GetParsedCode(), out int id))
            {
                var loc = itemsList.FirstOrDefault(tt => tt.RackID == id);

                if (loc != null)
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
                    UIMethod?.Invoke(loc);
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
