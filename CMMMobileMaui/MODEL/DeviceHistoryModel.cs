using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CMMMobileMaui.MODEL
{
    public class DeviceHistoryModel : ObservableObject
    {
        #region FIELDS

        private Task workerTask;

        #endregion

        #region PROPETY IsBusy

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        #endregion

        #region PROEPRTY BaseItem

        public DBMain.Model.History BaseItem
        {
            get;
            private set;
        }


        #endregion

        #region PROPERTY DisplayIndex

        public int DisplayIndex
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentDevice

        private GetDeviceListResponse currentDevice;

        public GetDeviceListResponse CurrentDevice
        {
            get => currentDevice;
            set => SetProperty(ref currentDevice, value);
        }

        #endregion

        #region COMMAND ShowItemCommand

        public ICommand ShowItemCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public DeviceHistoryModel(DBMain.Model.History baseItem, ICommand showItemCommand)
        {
            BaseItem = baseItem;
            ShowItemCommand = showItemCommand;
            workerTask = SetDataForDevice();
            Task.Run(async () => await workerTask);
        }

        #endregion

        #region METHOD SetDataForDevice

        public async Task SetDataForDevice()
        {
            var deviceCMMBLL = (IDeviceController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IDeviceController));

            var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
            {
                MachineID = BaseItem.ID
                        ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (deviceResponse.IsResponseWithData())
            {
                CurrentDevice = deviceResponse.Data!;
            }
            else
            {
                CurrentDevice = GetDeviceFromHistory();
            }
        }

        #endregion

        #region METHOD GetDeviceFromHistory

        private GetDeviceListResponse GetDeviceFromHistory()
        {
            var device = new GetDeviceListResponse
            {
                MachineID = BaseItem.ID
                ,
                AssetNo = BaseItem.Name
                ,
                AssetNoShort = BaseItem.Description
                , 
                StateID = 0
            };

            return device;
        }

        #endregion

    }
}
