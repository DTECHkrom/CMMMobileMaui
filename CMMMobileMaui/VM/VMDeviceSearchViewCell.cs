using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VM
{
    public class VMDeviceSearchViewCell: COMMON.ViewModelBase
    {
        #region PROPERTY CurrentDevice
        public MODEL.DeviceModel CurrentDevice
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DisplayIndex

        public int DisplayIndex
        {
            get;
            set;
        }

        #endregion

        #region COMMAND TakeDeviceCommand

        public ICommand TakeDeviceCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDeviceSearchViewCell()
        {
            TakeDeviceCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj != null)
                    {
                        //if (VM.VMDeviceSearch.IsPartGive)
                        //{
                        //  //  MessagingCenter.Send(new object(), COMMON.SConsts.DEV_FIND_GIVE, CurrentDevice);
                        //    COMMON.SConsts.GetGlobalAction<MODEL.DeviceModel>(COMMON.SConsts.DEV_FIND_GIVE).Invoke(CurrentDevice);

                        //    CMMMobileMaui.API.MainObjects.Instance.CurrentDevice = CurrentDevice.BaseItem;
                        //}
                        //else
                        //{
                        //    CMMMobileMaui.API.MainObjects.Instance.CurrentDevice = CurrentDevice.BaseItem;
                        //    COMMON.SConsts.GetGlobalAction<VMDeviceSearchViewCell>(COMMON.SConsts.DEV_OPEN_SEARCH).Invoke(this);
                        //  //  MessagingCenter.Send(this, COMMON.SConsts.DEV_OPEN_SEARCH);
                        //    // GoToDevice();
                        //}
                    }
                }
            });
        }

        #endregion
    }
}
