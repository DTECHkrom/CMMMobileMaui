using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.MODEL;
using Telerik.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderUserSchedule : PageBase<VM.VMWorkOrderUserScheduler>
    {
        #region Cstr

        public WorkOrderUserSchedule()
        {
            TelerikLocalizationManager.Manager = new COMMON.CustomTelerikLocalizationManager();

            InitializeComponent();
            calendar.ForceLayout();

            ViewModel.OnShowCurrentDate += VmMain_OnShowCurrentDate;
        }

        private void VmMain_OnShowCurrentDate(object? sender, EventArgs e)
        {
            calendar.CurrentDate = DateTime.Now.Date;
            calendar.ScrollIntoView(TimeOnly.FromDateTime(DateTime.Now));
        }

        #endregion

        #region METHOD OnAppearing

        protected override void OnAppearing()
        {
            ViewModel.SetMainSource();

            base.OnAppearing();
        }

        #endregion
        private void calendar_SlotTapped(object sender, Telerik.Maui.Controls.Scheduler.TappedEventArgs<Telerik.Maui.Controls.Scheduler.Slot> e)
        {
        }


        private async void calendar_DialogOpening(object sender, Telerik.Maui.Controls.Scheduler.SchedulerDialogOpeningEventArgs e)
        {
            e.Cancel = true;           
        }

        private async void calendar_AppointmentTapped(object sender, Telerik.Maui.Controls.Scheduler.TappedEventArgs<Telerik.Maui.Controls.Scheduler.Occurrence> e)
        {
            if (Shell.Current.Navigation.ModalStack.Count != 0
               || ViewModel.IsBusy)
                return;

            ViewModel.IsBusy = true;

            if(e.Data != null)
            {
                if(e.Data.Appointment is WOAppointment woApp)
                {

                    var baseWO = woApp.GetBaseWO();
                    var deviceCMMBLL = (IDeviceController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IDeviceController));

                    var devResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                    {
                        MachineID = baseWO.MachineID
                        ,
                        Lang = API.MainObjects.Instance.Lang
                    });

                    if (devResponse.IsValid)
                    {
                        API.MainObjects.Instance.CurrentDevice = devResponse.Data;

                        var woView = new WorkOrderSingleView(baseWO);
                       // woView.OnPageUnload += WoView_OnPageUnload;
                        await ViewModelBase.OpenModalPage(woView);
                    }
                }
            }

            ViewModel.IsBusy = false;
        }

        private void WoView_OnPageUnload(object? sender, EventArgs e)
        {
            ViewModel.SetMainSource();
        }
    }
}