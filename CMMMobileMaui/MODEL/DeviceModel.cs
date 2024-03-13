using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace CMMMobileMaui.MODEL
{
    public class DeviceModel : ObservableObject
    {

        #region PROPETY IsBusy

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        #endregion

        #region PROEPRTY BaseItem

        public GetDeviceListResponse BaseItem
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

        #region COMMAND TakeCommand

        public ICommand TakeDeviceCommand
        {
            get;
        }

        #endregion

        #region cstr

        public DeviceModel(GetDeviceListResponse currentDevice, ICommand takeDeviceCommand)
        {
            BaseItem = currentDevice;
            TakeDeviceCommand = takeDeviceCommand;
        }

        #endregion

    }
}
