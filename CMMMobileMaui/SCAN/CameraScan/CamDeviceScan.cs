using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamDeviceScan : DeviceBaseScan
    {
        public override ScanType GetScanType() => ScanType.Device;

        public override void GoToDevice(GetDeviceListResponse device)
        {
            COMMON.SConsts.GetGlobalAction<GetDeviceListResponse>(COMMON.SConsts.DEV_SCAN_CODE)
                                            .Invoke(device);
        }
    }
}
