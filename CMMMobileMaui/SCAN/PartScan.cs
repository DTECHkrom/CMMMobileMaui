using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.SCAN
{
    public class PartScan : BaseScanType, IScanType
    {
        private readonly string prefix = "p:";
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public PartScan()
        {

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
                var partBLL = (IPartController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IPartController));

                var partResponse = await partBLL.GetDetailShort(new API.Contracts.v1.Requests.Part.GetPartDetailShortRequest
                {
                    PartID = id
                    ,
                    Lang = API.MainObjects.Instance.Lang
                    ,
                    Filter = string.Empty
                });

                if (partResponse.IsResponseWithData())
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
                    UIMethod?.Invoke(partResponse.Data!.First());
                }
                else
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
                }
            }
        }
    }
}
