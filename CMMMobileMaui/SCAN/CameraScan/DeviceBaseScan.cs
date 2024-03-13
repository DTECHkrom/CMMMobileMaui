using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal abstract class DeviceBaseScan : IScanBaseType
    {
        private readonly string prefix = "d:";
        private int? deviceId;

        public abstract ScanType GetScanType();

        public bool IsValid(string code)
        {
            if (code.ToLower().StartsWith(prefix))
            {
                var parsedCode = code.Replace(prefix, string.Empty);

                if (int.TryParse(parsedCode, out int id))
                {
                    deviceId = id;
                    return true;
                }
            }

            return false;
        }

        public abstract void GoToDevice(GetDeviceListResponse device);

        public async Task<bool> ScanMethod()
        {
            var deviceCMMBLL = (IDeviceController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IDeviceController));

            var devResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
            {
                MachineID = deviceId!.Value
                                    ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (devResponse.IsResponseWithData())
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    GoToDevice(devResponse.Data!);
                });

                return true;
            }

            return false;
        }
    }
}
