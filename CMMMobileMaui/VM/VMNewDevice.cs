using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using System.Windows.Input;

namespace CMMMobileMaui.VM
{
    public class VMNewDevice : COMMON.ViewModelBase
    {
        #region FIELDS

        private CUST.CustomFrameEntry? _entryScan;
        private List<GetBranchLocationsResponse>? _branchTempList;

        private readonly WeakEventManager clearNewDeviceWeakManager = new WeakEventManager();

        public event EventHandler OnClearNewDevice
        {
            add => clearNewDeviceWeakManager.AddEventHandler(value);
            remove => clearNewDeviceWeakManager.RemoveEventHandler(value);
        }

        private IDeviceController deviceCMMBLL;

        #endregion

        #region PROPERTY CurrentDevice

        public CreateDeviceRequest? CurrentDevice { get; set; }

        #endregion

        #region PROPERTY CategoryList

        private List<DeviceDictResponse>? categoryList;
        public List<DeviceDictResponse>? CategoryList
        {
            get => categoryList;
            set => SetProperty(ref categoryList, value);
        }

        #endregion

        #region PROPERTY ProducentList

        private List<DeviceDictResponse>? producentList;

        public List<DeviceDictResponse>? ProducentList
        {
            get => producentList;
            set => SetProperty(ref producentList, value);
        }

        #endregion

        #region PROPERTY BranchList

        private List<GetBranchLocationsResponse>? branchList;

        public List<GetBranchLocationsResponse>? BranchList
        {
            get => branchList;
            set => SetProperty(ref branchList, value);
        }

        #endregion

        #region PROPERTY BranchList

        private List<GetBranchLocationsResponse>? locationList;

        public List<GetBranchLocationsResponse>? LocationList
        {
            get => locationList;
            set => SetProperty(ref locationList, value);
        }

        #endregion

        #region PROEPRTY CurrentBranch

        private GetBranchLocationsResponse? currentBranch;

        public GetBranchLocationsResponse? CurrentBranch
        {
            get
            {
                return currentBranch;
            }

            set
            {
                SetProperty(ref currentBranch, value);

                if (currentBranch is not null)
                {
                    CurrentLocation = null;
                    SetLocationForCurrentBranch();
                }
            }
        }

        #endregion

        #region PROPERTY CurrentLocation

        private GetBranchLocationsResponse? _currentLocation;

        public GetBranchLocationsResponse? CurrentLocation
        {
            get => _currentLocation;
            set
            {
                SetProperty(ref _currentLocation, value);

                if (CurrentDevice is null)
                {
                    return;
                }

                CurrentDevice.MainLocationID = null;

                if (_currentLocation != null)
                {
                    CurrentDevice.MainLocationID = _currentLocation.MainLocationID;
                }
            }
        }

        #endregion

        #region PROPERTY CurrentCategory

        private DeviceDictResponse? _currentCategory;

        public DeviceDictResponse? CurrentCategory
        {
            get => _currentCategory;
            set
            {
                SetProperty(ref _currentCategory, value);

                if(CurrentDevice is null)
                {
                    return;
                }

                if (_currentCategory == null)
                {
                    CurrentDevice.CategoryID = 0;
                }
                else
                {
                    CurrentDevice.CategoryID = _currentCategory.ID;
                }
            }
        }

        #endregion

        #region PROPERTY CurrentProducent

        private DeviceDictResponse? _currentProducent;

        public DeviceDictResponse? CurrentProducent
        {
            get => _currentProducent;
            set
            {
                SetProperty(ref _currentProducent, value);

                if (CurrentDevice is null)
                {
                    return;
                }

                CurrentDevice.ManufacturerID = null;

                if (_currentProducent is not null)
                {
                    CurrentDevice.ManufacturerID = _currentProducent.ID;
                }
            }
        }

        #endregion

        #region PROPERTY DeviceImg

        private GetDeviceImageResposne? deviceImg;

        public GetDeviceImageResposne? DeviceImg
        {
            get => deviceImg;
            set => SetProperty(ref deviceImg, value);
        }

        #endregion

        #region PROPERTY PictureManager

        public COMMON.PictureOperation.PictureManager PictureManager
        {
            get;
            set;
        } = new COMMON.PictureOperation.PictureManager();

        #endregion

        #region COMMAND SaveCommand

        public ICommand SaveCommand
        {
            get;
        }

        #endregion

        #region COMMAND CancelCommand

        public ICommand CancelCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectPictureCommand

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

        #region COMMAND ScanCodeCommand

        //TODO : Add ScanCodeCommand
        public ICommand ScanCodeCommand
        {
            get;
        }

        #endregion

        #region COMMAND OpenImageCommand

        //TODO : Add OpenImageCommand
        public ICommand OpenImageCommand
        {
            get;
        }

        #endregion

        #region COMMAND DeleteImageCommand

        //TODO : Add DeleteImageCommand
        public ICommand DeleteImageCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMNewDevice(IDeviceController deviceCMMBLL, IOtherController otherController)
        {
            this.deviceCMMBLL = deviceCMMBLL;
            CurrentDevice = new CreateDeviceRequest();

            PictureManager.OnSelectPicture += PictureManager_OnSelectPicture;
            PictureManager.OnTakePicture += PictureManager_OnTakePicture;

            CancelCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    CurrentDevice = null;
                    CurrentCategory = null;
                    CurrentProducent = null;
                    CurrentLocation = null;
                    CurrentBranch = null;
                    DeviceImg = null;

                    CurrentDevice = new CreateDeviceRequest();

                    clearNewDeviceWeakManager.HandleEvent(this, null, nameof(OnClearNewDevice));
                }
            });

            SaveCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    CurrentDevice.AddPersonID = MainObjects.Instance.CurrentUser!.PersonID;

                    if (DeviceImg != null)
                    {
                        CurrentDevice.Image = DeviceImg.Image;
                    }

                    var createResponse = await deviceCMMBLL.Create(CurrentDevice);

                    if (createResponse.IsValid)
                    {
                        var result = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");

                        if (result)
                        {
                            CancelCommand.Execute(null);
                        }
                    }
                }

            }, (obj) =>
            {
                if (CurrentDevice.CategoryID == 0
                || string.IsNullOrEmpty(CurrentDevice.AssetNo)
                || !CurrentDevice.MainLocationID.HasValue)
                {
                    return false;
                }

                return true;
            });

            COMMON.SConsts.SetGlobalAction<string>(COMMON.SConsts.DEV_SCAN_TAG, async (item) =>
                {
                    await Shell.Current.Navigation.PopModalAsync();

                    if (_entryScan != null)
                    {
                        _entryScan.Text = item;
                        _entryScan = null;
                    }
                });

            ScanCodeCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is Entry)
                    {
                        _entryScan = obj as CUST.CustomFrameEntry;
                    }

                    await OpenModalPage(new VIEW.ScanView(ScanType.DevText));
                }
            });

            TakePictureCommand = PictureManager.TakePictureCommand;

            SelectPictureCommand = PictureManager.SelectPictureCommand;

            OpenImageCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentDevice != null
                    && DeviceImg != null)
                    {
                        await OpenModalPage(new VIEW.ImageView(DeviceImg.Image));
                    }
                }
            });

            DeleteImageCommand = new Command((obj) =>
            {
                DeviceImg = null;
            }, (obj) =>
            {
                if (DeviceImg != null)
                {
                    return false;
                }

                return true;
            });

            SetStartData();
        }

        #endregion

        #region EVENT PictureManager_OnTakePicture

        private void PictureManager_OnTakePicture(object? sender, (WOFile info, object commandParameter) e)
        {
            if (e.info != null)
            {
                var img = new GetDeviceImageResposne();
                img.Image = e.info.Data;
                DeviceImg = img;
            }
        }

        #endregion

        #region EVENT PictureManager_OnSelectPicture

        private void PictureManager_OnSelectPicture(object? sender, (WOFile info, object commandParameter) e)
        {
            if (e.info != null)
            {
                var img = new GetDeviceImageResposne();
                img.Image = e.info.Data;
                DeviceImg = img;
            }
        }

        #endregion

        #region METHOD SetLocationForCurrentBranch

        private void SetLocationForCurrentBranch()
        {
            LocationList = _branchTempList?.Where(tt => tt.BranchID == currentBranch!.BranchID)
                .OrderBy(tt => tt.Location_Name)
                .ToList();
        }

        #endregion

        #region EVENT VMNewDevice_PropertyChanged

        private void VMNewDevice_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ((Command)SaveCommand).ChangeCanExecute();
        }

        #endregion

        #region METHOD SetStartData

        private async void SetStartData()
        {
            var branchResult = await deviceCMMBLL.GetBranchLocations();

            if (branchResult.IsResponseWithData(this))
            {
                CategoryList = DictionaryResources.Instance.DeviceCategoryList;
                ProducentList = DictionaryResources.Instance.DeviceProducentList;

                _branchTempList = branchResult.Data!.ToList();

                var items = _branchTempList.GroupBy(tt => tt.BranchID, tt => tt.Branch_Name, (id, name) => new { ID = id, Name = name });

                BranchList = items.Select(tt => new GetBranchLocationsResponse { BranchID = tt.ID, Branch_Name = tt.Name.First() }).ToList();
            }
        }

        #endregion
    }
}
