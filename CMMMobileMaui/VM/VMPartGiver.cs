using CMMMobileMaui.API;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.API.Interfaces;
using System.Windows.Input;

namespace CMMMobileMaui.VM
{
    public class VMPartGiver : COMMON.ViewModelBase
    {
        #region PROPERTY TakeAmount

        public decimal TakeAmount
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentPart

        public GetPartDetailResponse CurrentPart
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY PersonList

        public List<GetAllUsersResponse> PersonList =>
            DictionaryResources.Instance.PersonList;

        #endregion

        #region PROPERTY CurrentPerson

        public GetAllUsersResponse CurrentPerson
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY DeviceName

        public string DeviceName
        {
            get;
            set;
        }

        #endregion

        #region PROPETRY WOName

        public string WOName
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsDevice

        public bool IsDevice
        {
            get;
            set;
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
                _currentDevice = value;

                if (_currentDevice != null)
                {
                    IsDevice = true;
                    OnPropertyChanged("IsDevice");
                    DeviceName = _currentDevice.AssetNo;
                    OnPropertyChanged("DeviceName");
                }
            }
        }

        #endregion

        #region PROEPRTY CurrentWO

        private GetWOsResponse _currentWO;

        public GetWOsResponse CurrentWO
        {
            get
            {
                return _currentWO;
            }
            set
            {
                _currentWO = value;
                WOName = string.Empty;

                if (value != null)
                {
                    WOName = $"[{_currentWO.Add_Date.ToString("yyyy-MM-dd HH:mm")}]\n[{_currentWO.Mod_Person}]";
                }

                OnPropertyChanged("WOName");

            }
        }

        #endregion

        #region COMMAND SelectDeviceCommand

        public ICommand SelectDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND SelectWOCommand

        public ICommand SelectWOCommand
        {
            get;
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

        #region Cstr

        public VMPartGiver(IPartController partBLL, IUserController personBLL)
        {
            TakeAmount = 1;
            DeviceName = string.Empty;
            WOName = string.Empty;

            COMMON.SConsts.SetGlobalAction<GetWOsResponse>(COMMON.SConsts.WO_GIVE, (item) =>
            {
                if (item != null)
                {
                    CurrentWO = item as GetWOsResponse;
                    OnPropertyChanged("CurrentWO");

                    Shell.Current.Navigation.PopModalAsync();
                }
            });

            SelectDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    VIEW.DeviceSearchView vm = new VIEW.DeviceSearchView(true);
                    vm.OnDeviceSelected += Vm_OnDeviceSelected;
                    await OpenModalPage(vm);
                }
            });

            SelectWOCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (CurrentDevice != null)
                    {
                        VIEW.WorkOrderListAllView vm = new VIEW.WorkOrderListAllView(false, true, deviceId: CurrentDevice.MachineID);

                        await OpenModalPage(vm);
                    }
                }
            });

            SaveCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (IsDataSet())
                    {
                        var result = await CUST.CustomMessageDialog.Show(CUST.CustomMessageType.Dialog, CONV.TranslateExtension.GetResourceText("q_sure"));

                        if (!result)
                            return;

                        var part = new PartTake();
                        part.DeviceID = CurrentDevice.MachineID;
                        part.Amount = TakeAmount;

                        if (CurrentWO != null)
                        {
                            part.WorkOrderID = CurrentWO.WorkOrderID;
                        }

                        part.PartID = CurrentPart!.PartID;
                        part.PersonID = MainObjects.Instance.CurrentUser!.PersonID;
                        part.OtherPersonID = CurrentPerson!.PersonID;

                        var takeResponse = await partBLL.Take(new API.Contracts.v1.Requests.Part.PartTakeRequest
                        {
                            PartTakeList = new List<API.Contracts.v1.Requests.Part.SinglePartTake>
                            {
                                new API.Contracts.v1.Requests.Part.SinglePartTake
                                {
                                    MachineID = CurrentDevice.MachineID
                                    , Quantity = TakeAmount
                                    , WorkOrderID = (CurrentWO == null)? null: (int?)CurrentWO.WorkOrderID
                                    , OtherPersonID = CurrentPerson.PersonID
                                    , PersonID = MainObjects.Instance.CurrentUser.PersonID
                                    , PartID = CurrentPart.PartID
                                }
                            }
                        });

                        if (takeResponse.IsValid)
                        {
                            //   MessagingCenter.Send<string>(string.Empty, COMMON.SConsts.PART_MSG_GIVE);
                            COMMON.SConsts.GetGlobalAction(COMMON.SConsts.PART_MSG_GIVE)?.Invoke();
                            await Shell.Current.Navigation.PopModalAsync();
                        }
                    }
                }
            });

            CancelCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    Shell.Current.Navigation.PopModalAsync();
                }
            });
        }

        private bool IsDataSet() =>
            CurrentPerson != null
            && CurrentDevice != null
            && ((CurrentPart.StockLevel.HasValue
            && !CurrentPart.IsExchangeable && TakeAmount > 0
            && TakeAmount <= CurrentPart.StockLevel)
            || CurrentPart.IsExchangeable);

        private void Vm_OnDeviceSelected(object? sender, GetDeviceListResponse e)
        {
            CurrentDevice = e;
            CurrentWO = null;

            OnPropertyChanged("CurrentDevice");
            // App.Current.MainPage.Navigation.PopModalAsync();
        }

        #endregion

        #region METHOD SetMainData

        public void SetMainData()
        {
            if (CurrentPart.IsExchangeable)
            {
                TakeAmount = 0;
            }
        }

        #endregion
    }
}
