using System;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.v1.Requests.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.SCAN
{
    public class DeviceLocationScan : BaseScanType, IScanType
    {
        private readonly string prefix = "dl:";
        private GetMainLocationResponse selectedLocation;
        public Action<object> UIMethod
        {
            get;
            set;
        }

        public DeviceLocationScan(GetMainLocationResponse selectedLoc, ScanManager scanManager)
        {
            this.selectedLocation = selectedLoc;
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
            if(code.StartsWith(prefix))
            {
                this.code = code;
                return true;
            }

            return false;
        }

        public async Task ScanMethod()
        {
            var tab = code.Replace("dl:", "").Split(':');

            if (tab.Length > 0)
            {
                var loc = new GetScannedLocationRequest();

                loc.LocationID = int.Parse(tab[0]);

                if ((selectedLocation != null && loc.LocationID == selectedLocation.LocationID)
                    || (loc.LocationID > 0 && selectedLocation == null))
                {
                    if (tab.Length == 2)
                    {
                        loc.SubLocationID = int.Parse(tab[1]);
                    }

                    var deviceCMMBLL = (IDeviceController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IDeviceController));

                    var scanedLocation = await deviceCMMBLL.GetScanLoc(loc);

                    if (scanedLocation.IsResponseWithData())
                    {

                        App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Good);
                        UIMethod?.Invoke(scanedLocation.Data!);
                    }
                    else
                    {
                        App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
                    }
                }
                else
                {
                    App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
                    await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Error, $"Kod nie tej lokalizacji!");
                }
            }
            else
            {
                App.CurrentScanManager?.ShowScanResult(COMMON.ScanMode.Fail);
            }
        }
    }
}
