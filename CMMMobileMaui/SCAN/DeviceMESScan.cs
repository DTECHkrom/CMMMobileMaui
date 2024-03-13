using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.SCAN
{
    public class DeviceMESScan : BaseScanType, IScanType
    {
        public DeviceMESScan()
        {
        }

        public Action<object> UIMethod
        {
            get;
            set;
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
            var deviceCMMBLL = (IDeviceController)API.MainObjects.Instance.ServiceProvider.GetRequiredService(typeof(IDeviceController));

            var deviceResponse = await deviceCMMBLL.GetByName(new API.Contracts.v1.Requests.Device.GetDeviceByNameRequest
            {
                Name = code
                ,
                Lang = API.MainObjects.Instance.Lang
            }); ;

            if (deviceResponse.IsResponseWithData())
            {
                if (deviceResponse.Data!.Count() == 1)
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
                    UIMethod?.Invoke(deviceResponse.Data!.First());
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
