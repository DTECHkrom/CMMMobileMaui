using CMMMobileMaui.API.Contracts.Models.Handlers;
using CMMMobileMaui.API.Contracts.v1.Requests.WO;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderListAllView : PageBase<VM.VMWorkOrderListAll>
    {
        #region Cstr

        public WorkOrderListAllView()
        {
            InitializeComponent();
        }

        public WorkOrderListAllView(bool isPerson
            , bool isPartGive = false
            , bool canAddNewWO = false
            , bool isActive = false
            , bool isPlan = false
            , int deviceId = 0) : this()
        {
            ViewModel.IsPartGive = isPartGive;
            ViewModel.CanAddNewWO = canAddNewWO;
            ViewModel.IsPlan = isPlan;

            if (isPartGive)
            {
                if(deviceId == 0)
                {
                    throw new ArgumentNullException(nameof(deviceId));
                }

                ViewModel.DeviceID = deviceId;
                ViewModel.CanAddNewWO = false;
                ViewModel.InitRequestType(new WOPartTakeListRequestHandler(filterPanel.GetUsedFilter(), deviceId));
            }
            else if (API.MainObjects.Instance.CurrentDevice != null)
            {
                ViewModel.DeviceID = API.MainObjects.Instance.CurrentDevice.MachineID;

                if (isPlan)
                {
                    ViewModel.InitRequestType(new DevicePlanListRequestHandler(filterPanel.GetUsedFilter()));
                }
                else
                {
                    ViewModel.InitRequestType(new DeviceWOListRequestHandler(filterPanel.GetUsedFilter()));
                }
            }
            else if (isPerson)
            {
                ViewModel.InitRequestType(new PersonWOListRequestHandler(filterPanel.GetUsedFilter()));
            }
            else
            {
                ViewModel.InitRequestType(new WOListRequestHandler(filterPanel.GetUsedFilter()));
            }

            ViewModel.IsActiveWO = isActive;
            //this.isPerson = isPerson;

            InitLocalEvents();
        }

        #endregion

        #region METHOD SetFilterOnStart

        private void SetFilterOnStart()
        {
            //  if (wasFirstLoad)
            //      return;

            filterPanel.SetDefaultFilter(ViewModel.GetListRequestHandler().GetWOsRequest());
            filterPanel.FiltersToRemove(ViewModel.GetListRequestHandler().GetFiltersToRemove());
            //    wasFirstLoad = true;
        }

        #endregion

        private void InitLocalEvents()
        {
            filterPanel.OnFilterChanged += FilterPanel_OnFilterChanged;
        }

        private void FilterPanel_OnFilterChanged(object? sender, GetWOsRequest e)
        {
            this.ViewModel.SetListByFilter(e);
        }

        #region EVENT OnAppearing

        protected override void OnAppearing()
        {
            SetFilterOnStart();

            if (API.MainObjects.Instance.CurrentUser != null)
            {

                if (!filterPanel.WasFirstFilterExecuteOnStart())
                {
                    filterPanel.FilterOnStart();
                }
                else
                {
                    ViewModel.IsBusy = true;
                }

                base.OnAppearing();
            }
        }

        #endregion

    }
}