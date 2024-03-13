using Telerik.Maui.Controls.Scheduler;

namespace CMMMobileMaui.COMMON
{
    public class CustomDayAppointmentWOView : DataTemplateSelector
    {
        public DataTemplate AllDay { get; set; }
        public DataTemplate NotAllDay { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if(item is AppointmentNode appNode)
            {
                if(appNode.Occurrence.Appointment.IsAllDay)
                {
                    return AllDay;
                }
            }

            return this.NotAllDay;
        }
    }
}
