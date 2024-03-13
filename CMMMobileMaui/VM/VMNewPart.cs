using System.Windows.Input;
using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.VM
{
    public class VMNewPart : COMMON.ViewModelBase
    {
        #region FIELDS

        private Entry _entryScan;

        private readonly WeakEventManager clearNewDeviceWeakManager = new WeakEventManager();

        public event EventHandler OnClearNewDevice
        {
            add => clearNewDeviceWeakManager.AddEventHandler(value);
            remove => clearNewDeviceWeakManager.RemoveEventHandler(value);
        }

        private IPartController partBLL;

        #endregion

        #region PROEPRTY CurrentPart

        public CreatePartRequest CurrentPart
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceImg

        private byte[]? deviceImg;

        public byte[]? PartImage
        {
            get => deviceImg;
            set => SetProperty(ref deviceImg, value);
        }

        #endregion

        #region PROPERTY WarehouseList

        public List<PartDictResponse> WarehouseList =>
            DictionaryResources.Instance.WarehouseList;

        #endregion

        #region PROPERTY PictureManager

        public COMMON.PictureOperation.PictureManager PictureManager
        {
            get;
            set;
        } = new COMMON.PictureOperation.PictureManager();

        #endregion

        //TODO dodać liste lokalizacji części po zmianie magazynu
        #region PROPERTY CurrentWarehouse

        private PartDictResponse _currentWarehouse;

        public PartDictResponse CurrentWarehouse
        {
            get => _currentWarehouse;
            set
            {
                _currentWarehouse = value;

                if (value != null)
                {
                    CurrentPart.WarehouseID = _currentWarehouse.ID;
                    ((Command)SaveCommand).ChangeCanExecute();
                    GeneratePartName();
                }
            }
        }

        #endregion

        #region PROPERTY GeneralCategoryList

        public List<GetPartCatResponse> GeneralCategoryList =>
            DictionaryResources.Instance.PartGeneralCategoryList;

        #endregion

        #region PROPERTY CurrentGeneralCategory

        private GetPartCatResponse _currentGeneralCategory;

        public GetPartCatResponse CurrentGeneralCategory
        {
            get => _currentGeneralCategory;
            set
            {
                _currentGeneralCategory = value;

                if (_currentGeneralCategory != null)
                {
                    CategoryList = DictionaryResources.Instance.MainPartCategoryList.Where(tt => tt.PartGeneralCategoryID == _currentGeneralCategory.PartGeneralCategoryID).ToList();
                    CurrentPart.GeneralCategoryID = _currentGeneralCategory.PartGeneralCategoryID;
                }
                else
                {
                    CategoryList = new List<GetPartCatResponse>();
                }

                ((Command)SaveCommand).ChangeCanExecute();
                GeneratePartName();
            }
        }

        #endregion

        #region PROPERTY CategoryList

        public List<GetPartCatResponse> CategoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentCategory

        private GetPartCatResponse _currentCategory;

        public GetPartCatResponse CurrentCategory
        {
            get => _currentCategory;
            set
            {
                _currentCategory = value;

                if (value != null)
                {
                    CurrentPart.CategoryID = _currentCategory.PartCategoryID;
                }

                ((Command)SaveCommand).ChangeCanExecute();
                GeneratePartName();
            }
        }

        #endregion

        #region PROPERTY ProducentList

        public List<PartDictResponse> ProducentList =>
            DictionaryResources.Instance.ProducentList;

        #endregion

        #region PROPERTY CurrentProducent

        public PartDictResponse CurrentProducent
        {
            get => currentProducent;
            set
            {
                currentProducent = value;

                if (value != null)
                {
                    CurrentPart.ProducentID = value.ID;
                }
            }
        }

        #endregion

        #region PROPERTY UnitList

        public List<PartDictResponse> UnitList =>
            DictionaryResources.Instance.PartUnitList;

        #endregion

        #region PROPERTY CurrentWarehouse

        private PartDictResponse _currentUnit;

        public PartDictResponse CurrentUnit
        {
            get => _currentUnit;
            set
            {
                _currentUnit = value;

                if (_currentUnit != null)
                {
                    CurrentPart.UnitID = _currentUnit.ID;
                }

                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        #endregion

        #region PROPERTY CurrencyList

        public List<PartDictResponse> CurrencyList =>
            DictionaryResources.Instance.CurrencyList;

        #endregion

        #region PROPERTY CurrentWarehouse

        private PartDictResponse _currentCurrency;
        private PartDictResponse currentProducent;

        public PartDictResponse CurrentCurrency
        {
            get => _currentCurrency;
            set
            {
                _currentCurrency = value;

                if (value != null)
                {
                    CurrentPart.CurrencyID = _currentCurrency.ID;
                }
            }
        }

        #endregion

        #region PROEPRTY PartNo

        public string PartNo
        {
            get;
            set;
        }

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

        //TODO : add scan code
        public ICommand ScanCodeCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowPictureCommand

        //TODO : add show picture
        public ICommand ShowPictureCommand
        {
            get;
        }

        #endregion

        #region COMMAND DeletePictureCommand

        //TODO : add delete picture
        public ICommand DeletePictureCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMNewPart(IPartController partBLL)
        {
            this.partBLL = partBLL;
            CurrentPart = new CreatePartRequest();

            PictureManager.OnSelectPicture += PictureManager_OnSelectPicture;
            PictureManager.OnTakePicture += PictureManager_OnTakePicture;

            CancelCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    ClearPart();
                }
            });

            SaveCommand = new Command(async (obj) =>
            {
                if (IsControlFill())
                {
                    if (CanClick())
                    {
                        var createResponse = await partBLL.Create(new CreatePartRequest
                        {
                            CategoryID = CurrentPart.CategoryID
                            ,
                            CurrencyID = CurrentPart.CurrencyID
                            ,
                            Description = CurrentPart.Description
                            ,
                            GeneralCategoryID = CurrentPart.GeneralCategoryID
                            ,
                            Image = PartImage
                            ,
                            PartNo = PartNo!
                            ,
                            PersonID = MainObjects.Instance.CurrentUser!.PersonID
                            ,
                            ProducentCode = CurrentPart.ProducentCode
                            ,
                            ProducentID = CurrentPart.ProducentID
                            ,
                            StockLevel = CurrentPart.StockLevel
                            ,
                            UnitID = CurrentPart.UnitID
                            ,
                            UnitPrice = CurrentPart.UnitPrice
                            ,
                            StockLevelTarget = CurrentPart.StockLevelTarget
                            ,
                            WarehouseID = CurrentPart.WarehouseID
                        });

                        if (createResponse.IsValid)
                        {
                            var result = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Info, "OK");

                            ClearPart();
                        }
                    }

                }
            });

            COMMON.SConsts.SetGlobalAction<string>(COMMON.SConsts.PART_SCAN_TEXT, async (item) =>
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
                        _entryScan = obj as Entry;
                    }
                    await Shell.Current.Navigation.PushModalAsync(new VIEW.ScanView(ScanType.PartText));
                }
            });

            TakePictureCommand = PictureManager.TakePictureCommand;

            SelectPictureCommand = PictureManager.SelectPictureCommand;

            ShowPictureCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (PartImage != null)
                    {
                        await Shell.Current.Navigation.PushModalAsync(new VIEW.ImageView(PartImage));
                    }
                }
            });

            DeletePictureCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    PartImage = null;
                }
            });

            SetStartData();
        }

        private void PictureManager_OnTakePicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (e.info != null)
            {
                PartImage = e.info.Data;
            }
        }

        private void PictureManager_OnSelectPicture(object? sender, (WOFile info, object? commandParameter) e)
        {
            if (e.info != null)
            {
                PartImage = e.info.Data;
            }
        }

        #endregion

        #region METHOD IsControlFill

        private bool IsControlFill()
        {
            if (CurrentPart.CategoryID == 0
                || CurrentPart.WarehouseID == 0
                || string.IsNullOrEmpty(PartNo)
                || !CurrentPart.UnitPrice.HasValue
                || CurrentCurrency == null
                || CurrentPart.GeneralCategoryID == 0
                || CurrentPart.UnitID == 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region METHOD ClearPart

        private void ClearPart()
        {
            CurrentCategory = null;
            CurrentCurrency = null;
            CurrentGeneralCategory = null;
            CurrentProducent = null;
            CurrentUnit = null;
            CurrentWarehouse = null;
            PartImage = null;
            CurrentPart = new CreatePartRequest();
            ((Command)SaveCommand).ChangeCanExecute();
            clearNewDeviceWeakManager.HandleEvent(this, null, nameof(OnClearNewDevice));
        }

        #endregion

        #region EVENT VMNewDevice_PropertyChanged

        private void VMNewDevice_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ((Command)SaveCommand).ChangeCanExecute();
        }

        #endregion

        #region METHOD SetStartData

        private void SetStartData()
        {
            if (GeneralCategoryList.Count == 1)
            {
                CurrentGeneralCategory = GeneralCategoryList.FirstOrDefault();
            }
        }

        #endregion

        #region METHOD GeneratePartName

        private async void GeneratePartName()
        {
            if (CurrentWarehouse != null
                && CurrentGeneralCategory != null
                && CurrentCategory != null)
            {
                var newNoResponse = await partBLL.GetNewPartNo(new GetNewPartNoRequest()
                {
                    WarehouseID = CurrentWarehouse.ID,
                    CategoryID = CurrentCategory.PartCategoryID,
                    GeneralCategoryID = CurrentGeneralCategory.PartGeneralCategoryID
                });

                if (newNoResponse.IsValid)
                {
                    PartNo = newNoResponse.Data.NewPartNo;
                }
            }
            else
            {
                PartNo = string.Empty;
            }
        }

        #endregion
    }
}
