using System.Collections.ObjectModel;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VM
{
    public class VMBaseList : ScannerViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceCMMBLL;
        private int id;
        public Action<string>? ScanMethod;
        private readonly int displayStep = 20;

        private readonly WeakEventManager selectedItemWeakManager = new WeakEventManager();

        private List<GetDeviceSubLocationResponse>? orgTempList;
        private List<IScanType> scanTypes = new List<IScanType>();

        public event EventHandler<GetDeviceSubLocationResponse> OnSelectedItem
        {
            add => selectedItemWeakManager.AddEventHandler(value);
            remove => selectedItemWeakManager.RemoveEventHandler(value);
        }

        #endregion

        #region PROPERTY MainList

        private ObservableCollection<GetDeviceSubLocationResponse> mainList = new ObservableCollection<GetDeviceSubLocationResponse>();

        public ObservableCollection<GetDeviceSubLocationResponse> MainList
        {
            get => mainList;
            set => SetProperty(ref mainList, value);
        }

        #endregion

        #region PROPERTY FilterText

        private string? filterText;

        public string? FilterText
        {
            get => filterText;
            set => SetProperty(ref filterText, value);
        }

        #endregion

        #region PROPERTY LocationName

        private string? locationName;

        public string? LocationName
        {
            get => locationName;
            set => SetProperty(ref locationName, value);
        }

        #endregion

        #region PROPERTY IsFilterModeVisible

        private bool isFilterModeVisible;

        public bool IsFilterModeVisible
        {
            get => isFilterModeVisible;
            set => SetProperty(ref isFilterModeVisible, value);
        }

        #endregion

        #region PROPERTY TakeItemCommand

        public ICommand TakeItemCommand
        {
            get;
        }

        #endregion

        #region PROPERTY FilterItemCommand

        public ICommand FilterItemCommand
        {
            get;
        }

        #endregion

        #region COMMAND RefrehListCommand

        public ICommand RefrehListCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMBaseList(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;
            FilterText = string.Empty;

            TakeItemCommand = new Command((obj) =>
            {
                if (obj is GetDeviceSubLocationResponse location)
                {
                    selectedItemWeakManager.HandleEvent(this, location, nameof(OnSelectedItem));              
                }
            });

            FilterItemCommand = new Command(() =>
            {
                if (CanClick())
                {
                    RefreshList(true);
                }
            });

            RefrehListCommand = new Command((obj) =>
            {
                RefreshList(false);
            });
        }

        #endregion

        #region METHOD SetScanTypes

        public void SetScanTypes(List<IScanType> scanTypes)
        {
            this.scanTypes.AddRange(scanTypes);
        }

        #endregion

        #region METHOD RefreshList

        private void RefreshList(bool isFromFilter)
        {
            IsBusy = true;

            if (orgTempList is not null)
            {
                if (isFromFilter)
                {
                    MainList.Clear();
                }

                if(MainList.Count != orgTempList.Count)
                {
                    var items = orgTempList.OrderBy(tt => tt.Name)
                        .Skip(MainList.Count)
                        .Take(displayStep)
                        .ToList();

                    foreach(var item in items)
                    {
                        MainList.Add(item);
                    }
                }
            }

            IsBusy = false;
        }

        #endregion

        #region METHOD SetMainData

        public async Task SetMainData(int locationID, string locationName)
        {
            IsBusy = true;

            this.id = locationID;

            var subLocationResponse = await deviceCMMBLL.GetSubLoc(new API.Contracts.v1.Requests.Device.GetByDeviceLocationIDRequest
            {
                LocationID = locationID
            });

            MainList.Clear();
            IsFilterModeVisible = false;

            if (subLocationResponse.IsResponseWithData(this))
            {
                orgTempList = subLocationResponse.Data!.ToList();

                IsFilterModeVisible = ShouldShowFilterMode();

                if (!IsFilterModeVisible)
                {
                    RefreshList(false);
                }
            }
            else
            {
                IsBusy = false;
            }
        }

        #endregion

        #region METHOD ShouldShowFilterMode

        private bool ShouldShowFilterMode() =>
            orgTempList is not null && orgTempList.Count > displayStep * 5;

        #endregion

        #region METHOD GetID

        public int GetID() => this.id;

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems() => scanTypes;

        #endregion
    }
}
