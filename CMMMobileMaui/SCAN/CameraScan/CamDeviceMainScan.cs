using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal class CamDeviceMainScan : DeviceBaseScan
    {
        public override ScanType GetScanType() =>
            ScanType.DeviceMain;

        public override async void GoToDevice(GetDeviceListResponse device)
        {
            DBMain.Engine en = new DBMain.Engine();

            var histList = await en.GetAllHistory(API.MainObjects.Instance.CurrentUser!.PersonID);

            var hist = histList.FirstOrDefault(tt => tt.ID == Convert.ToInt32(device.MachineID) && tt.Type == "d");

            if (hist != null)
            {
                hist.Mod_Date = DateTime.Now;
                int a = await en.UpdateHist(hist);
            }
            else
            {
                hist = new DBMain.Model.History();
                hist.ID = device.MachineID;
                hist.Type = "d";
                hist.Mod_Date = DateTime.Now;
                hist.Name = device.AssetNo;
                hist.PersonID = API.MainObjects.Instance.CurrentUser.PersonID;
                int b = await en.InsertHist(hist);
            }

            var deviceView = new VIEW.DeviceView(device);

            await ViewModelBase.OpenModalPage(deviceView);
        }
    }
}
