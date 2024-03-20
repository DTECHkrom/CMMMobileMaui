using System.Collections.ObjectModel;
using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.VM
{
    public class VMDevice : COMMON.ViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceController;

        #endregion

        #region PROPERTY CurrentState

        private GetDeviceStateResponse? currentState;

        public GetDeviceStateResponse? CurrentState
        {
            get => currentState;
            set => SetProperty(ref currentState, value);
        }

        #endregion

        #region PROPERTY IsLastStateVisible

        private bool isLastStateVisible;
        public bool IsLastStateVisible
        {
            get => isLastStateVisible;
            set => SetProperty(ref isLastStateVisible, value);
        }

        #endregion

        #region PROPERTY IsAllStateVisible

        private bool isAllStateVisible;
        public bool IsAllStateVisible
        {
            get => isAllStateVisible;
            set => SetProperty(ref isAllStateVisible, value);
        }

        #endregion

        #region PROPERTY IsWOPanel

        public bool IsWOPanel
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceImg

        private GetDeviceImageResposne? deviceImg;

        public GetDeviceImageResposne? DeviceImg
        {
            get => deviceImg;
            set
            {
                SetProperty(ref deviceImg, value);
                IsImagePanelVisible = deviceImg == null;
            }
        }

        #endregion

        #region PROPERTY CurrentDevice

        private GetDeviceListResponse _currentDevice;

        public GetDeviceListResponse CurrentDevice
        {
            get
            {
                return _currentDevice;
            }
            set
            {
                SetProperty(ref _currentDevice, value);

                if (_currentDevice != null)
                {
                    MainObjects.Instance.CurrentDevice = _currentDevice;
                    SetDevice();
                }
            }
        }

        #endregion

        #region PROPERTY StateList

        private ObservableCollection<GetDeviceStateResponse> stateList = new ObservableCollection<GetDeviceStateResponse>();

        public ObservableCollection<GetDeviceStateResponse> StateList
        {
            get => stateList;
            set => SetProperty(ref stateList, value);
        }

        #endregion

        #region PROPERTY PictureManager

        public COMMON.PictureOperation.PictureManager PictureManager
        {
            get;
            set;
        } = new COMMON.PictureOperation.PictureManager();

        #endregion

        #region PROPERTY IsObserved

        private bool isObserved;

        public bool IsObserved
        {
            get => isObserved;
            set => SetProperty(ref isObserved, value);
        }

        #endregion

        #region PROPERTY IsImagePanelVisible

        private bool isImagePanelVisible;

        public bool IsImagePanelVisible
        {
            get => isImagePanelVisible;
            set
            {
                SetProperty(ref isImagePanelVisible, value);
                
            }
        }

        #endregion

        #region COMMAND DeviceInfoCommand

        public ICommand DeviceInfoCommand
        {
            get;
        }

        #endregion

        #region COMMAND DeviceWOListCommand

        public ICommand DeviceWOListCommand
        {
            get;
        }

        #endregion

        #region COMMAND DevicePlanListCommand

        public ICommand DevicePlanListCommand
        {
            get;
        }

        #endregion

        #region PROPERTY IsAnyLocation

        public bool IsAnyLocation
        {
            get
            {
                if (CurrentDevice == null)
                    return false;

                return !string.IsNullOrEmpty(CurrentDevice.Location)
                    || !string.IsNullOrEmpty(CurrentDevice.LocationName)
                    || !string.IsNullOrEmpty(CurrentDevice.Place);
            }
        }

        #endregion

        #region COMMAND AddWOCommand

        public ICommand AddWOCommand
        {
            get;
        }

        #endregion

        #region COMMAND TakePictureCommand

        public ICommand TakePictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectPictureCommand

        public ICommand SelectPictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenImageCommand

        public ICommand OpenImageCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowDocumentListCommand

        public ICommand ShowDocumentListCommand
        {
            get;
        }

        #endregion

        #region COMMAND DeletePictureCommand

        public ICommand DeletePictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowLastStateCommand

        public ICommand ShowLastStateCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowListStateCommand

        public ICommand ShowListStateCommand
        {
            get;
        }

        #endregion

        #region COMMAND ObservedDeviceCommand

        public ICommand ObservedDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowImagePanelCommand

        public ICommand ShowImagePanelCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDevice(IDeviceController deviceController
            , IUserController userController)
        {
            this.deviceController = deviceController;
            IsLastStateVisible = false;
            IsAllStateVisible = true;
            isImagePanelVisible = true;
            IsWOPanel = true;

            PictureManager.OnSelectPicture += PictureManager_OnSelectPicture;
            PictureManager.OnTakePicture += PictureManager_OnTakePicture;

            COMMON.SConsts.SetGlobalAction(COMMON.SConsts.DEV_WO_ADD, async () =>
            {
                var deviceResponse = await deviceController.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                {
                    MachineID = CurrentDevice.MachineID
                        ,
                    Lang = MainObjects.Instance.Lang
                });

                if (deviceResponse.IsResponseWithData(this))
                {
                    CurrentDevice = deviceResponse.Data!;
                    COMMON.SConsts.GetGlobalAction(COMMON.SConsts.DEV_REF_BACK)?.Invoke();
                    SetDevice();
                }
            });

            ShowImagePanelCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    if (IsImagePanelVisible)
                        return;

                    IsImagePanelVisible = true;

                    await Task.Run(async () =>
                    {
                        await Task.Delay(3000);
                        IsImagePanelVisible = false;
                    });
                }
            });

            DeviceInfoCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.DeviceDetailVIew(CurrentDevice.MachineID, CurrentDevice.AssetNo));
                }
            });

            DeviceWOListCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.WorkOrderListAllView(false, canAddNewWO: false));
                }
            });

            DevicePlanListCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.WorkOrderListAllView(false, canAddNewWO: false, isPlan: true));
                }
            });

            AddWOCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    await OpenModalPage(new VIEW.WorkOrderSingleView(null));
                }
            });

            ShowDocumentListCommand = new Command(async () =>
            {
                if (CanClick())
                {
                    await OpenModalPage(COMMON.SConsts.GetBaseNavigationPage(new VIEW.DeviceDocumentationView(CurrentDevice.DocumentationPath) { Title = CurrentDevice.AssetNo }));
                }
            });

            TakePictureCommand = PictureManager.TakePictureCommand;

            SelectPictureCommand = PictureManager.SelectPictureCommand;

            OpenImageCommand = new Command(async (obj) =>
            {
                if (DeviceImg != null)
                {
                    if (CanClick())
                    {
                        await OpenModalPage(new VIEW.ImageView(DeviceImg.Image));
                    }
                }
            });

            DeletePictureCommand = new Command(async (obj) =>
            {
                bool dialogResult = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                if (dialogResult)
                {
                    var updateResponse = await deviceController.UpdateImage(new API.Contracts.v1.Requests.Device.UpdateDeviceImageRequest
                    {
                        DeviceID = CurrentDevice.MachineID,
                        Image = null,
                        PersonID = MainObjects.Instance.CurrentUser!.PersonID
                    });

                    if (updateResponse.IsResponseWithData(this))
                    {
                        DeviceImg = null;
                    }
                }

            });

            ShowLastStateCommand = new Command((obj) =>
            {
                IsLastStateVisible = false;
                IsAllStateVisible = true;
            });

            ShowListStateCommand = new Command((obj) =>
            {
                IsLastStateVisible = true;
                IsAllStateVisible = false;
            });

            ObservedDeviceCommand = new Command(async () =>
            {
                var result = await deviceController.CreateUserObserved(new API.Contracts.v1.Requests.Device.CreateUserObservedDeviceRequest
                {
                    PersonID = MainObjects.Instance.CurrentUser!.PersonID
                    ,
                    MachineID = CurrentDevice.MachineID
                });

                if (result.IsResponseWithData(this))
                {
                    var observedDevicesResult = await userController.GetObservedDevices(new API.Contracts.v1.Requests.User.GetUserObservedDeviceRequest
                    {
                        PersonID = MainObjects.Instance.CurrentUser.PersonID
                    });

                    if (observedDevicesResult.IsResponseWithData(this))
                    {
                        MainObjects.Instance.ObservedDevices = observedDevicesResult.Data;
                        SetViewForObservedDevice();
                    }
                }
            });

        }

        private void PictureManager_OnTakePicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (e.info != null)
            {
                ChangeDeviceImage(e.info);
            }
        }

        private async void ChangeDeviceImage(WOFile woFile)
        {
            IsBusy = true;

            var updateImage = await deviceController.UpdateImage(new API.Contracts.v1.Requests.Device.UpdateDeviceImageRequest
            {
                Image = woFile.Data
                ,
                DeviceID = CurrentDevice.MachineID
                ,
                PersonID = MainObjects.Instance.CurrentUser!.PersonID
            });

            if (updateImage.IsResponseWithData(this))
            {
                DeviceImg = updateImage.Data!;
            }

            IsBusy = false;
        }

        private void PictureManager_OnSelectPicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (e.info != null)
            {
                ChangeDeviceImage(e.info);
            }
        }

        #endregion

        #region METHOD SetViewForObservedDevice

        private void SetViewForObservedDevice()
        {
            if (MainObjects.Instance.ObservedDevices == null
                || !MainObjects.Instance.ObservedDevices.MachineIDs.Contains(CurrentDevice.MachineID))
            {
                IsObserved = false;
                return;
            }

            IsObserved = true;
        }

        #endregion

        #region METHOD SetDevice
        private async void SetDevice()
        {
            IsBusy = true;

            if (CurrentDevice != null)
            {
                SetViewForObservedDevice();

                StateList.Clear();

                var stateTask = deviceController.GetState(new API.Contracts.v1.Requests.Device.GetDeviceStateRequest
                {
                    Lang = MainObjects.Instance.Lang,
                    MachineID = CurrentDevice.MachineID,
                });               

                if(deviceImg == null)
                {
                    var imageTask = deviceController.GetImage(new API.Contracts.v1.Requests.Device.GetByDeviceIDRequest
                    {
                        DeviceID = CurrentDevice.MachineID
                    });

                    await Task.WhenAll(stateTask, imageTask);

                    var imageResult = imageTask.Result;

                    if (imageResult.IsResponseWithData())
                    {
                        DeviceImg = imageTask.Result.Data!;
                    }
                }
                else
                {
                    await stateTask;
                }

                //var imageTask = deviceController.GetImage(new API.Contracts.v1.Requests.Device.GetByDeviceIDRequest
                //{
                //    DeviceID = CurrentDevice.MachineID
                //});

                //await Task.WhenAll(stateTask, imageTask);

                var stateResult = stateTask.Result;
             //   var imageResult = imageTask.Result;

                if (stateResult.IsResponseWithData())
                {
                    stateResult.Data!
                        .OrderByDescending(tt => tt.Change_Time)
                        .ToList()
                        .ForEach(state => StateList.Add(state));

                    CurrentState = StateList.FirstOrDefault();
                }

                //if (imageResult.IsResponseWithData())
                //{
                //    DeviceImg = imageTask.Result.Data!;
                //}

               // OnPropertyChanged(nameof(StateList));
              //  OnPropertyChanged(nameof(DeviceImg));
            }

            IsBusy = false;
        }

        #endregion

    }
}
