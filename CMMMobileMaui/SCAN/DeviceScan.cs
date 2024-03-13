using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.SCAN
{
    public class DeviceScan : BaseScanType, IScanType
    {
        private readonly string prefix = "d:";
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public DeviceScan()
        { }

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
            if (int.TryParse(GetParsedCode(), out int id))
            {
                var deviceCMMBLL = (IDeviceController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IDeviceController));

                var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                {
                    MachineID = id
                        ,
                    Lang = API.MainObjects.Instance.Lang
                });

                if (deviceResponse.IsResponseWithData())
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
                    UIMethod?.Invoke(deviceResponse.Data!);
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
