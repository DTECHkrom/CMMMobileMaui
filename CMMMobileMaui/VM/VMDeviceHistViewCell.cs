using System;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VM
{
    public class VMDeviceHistViewCell : COMMON.ViewModelBase
    {
        #region PROEPRTY DisplayIndex

        public int DisplayIndex
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ZebraViewModel

        public COMMON.IZebraScanPage ZebraViewModel
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceHist

        private DBMain.Model.History deviceHist;

        public DBMain.Model.History DeviceHist
        {
            get => deviceHist;
            set
            {
                deviceHist = value;

                if(value != null)
                {
                    CurrentDevice = new GetDeviceListResponse();
                    CurrentDevice.MachineID = DeviceHist.ID;
                    CurrentDevice.AssetNo = DeviceHist.Name;
                    OnPropertyChanged("CurrentDevice");
                    SetDataForDevice();
                }
            }
        }

        #endregion

        #region PROPERTY CurrentDevice

        private GetDeviceListResponse currentDevice;

        public GetDeviceListResponse CurrentDevice
        {
            get => currentDevice;
            set => currentDevice = value;
        }

        private IDeviceController deviceCMMBLL;

        #endregion

        #region COMMAND ShowItemCommand

        public ICommand ShowItemCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDeviceHistViewCell(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;

            ShowItemCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    DeviceHist.Mod_Date = DateTime.Now;
                    DBMain.Engine en = new DBMain.Engine();

                    int a = await en.UpdateHist(DeviceHist);

                    if (DeviceHist.Type == "d")
                    {
                        ShowItem();
                        COMMON.SConsts.GetGlobalAction(COMMON.SConsts.DEV_HIST_REF)?.Invoke();
                    }

                    en.CloseConnection();
                }
            });
        }

        #endregion

        #region METHOD SetDataForDevice

        private async void SetDataForDevice()
        {
            IsBusy = true;

            var devResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
            {
                MachineID = DeviceHist.ID
                        ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (devResponse.IsValid)
            {
                CurrentDevice = devResponse.Data;
                OnPropertyChanged("CurrentDevice");
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD ShowItem

        private async void ShowItem()
        {
            var devResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
            {
                MachineID = DeviceHist.ID
                        ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (devResponse.IsValid)
            {
                await OpenModalPage(new VIEW.DeviceView(devResponse.Data));
            }
        }

        #endregion
    }
}
