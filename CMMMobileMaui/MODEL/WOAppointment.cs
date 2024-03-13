using Microsoft.Maui.Platform;
using Telerik.Maui;
using Telerik.Maui.Controls.Scheduler;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.COMMON;
using System.Security.Cryptography.Pkcs;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.MODEL
{
    public class WOAppointment : Appointment
    {
        private GetWOsResponse baseWO;

        public string WO_State
        {
            get;
            set;
        }

        public string WO_Category
        {
            get;
            set;
        }

        public string WO_Reason
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        #region PROPERTY WOItems

        private ObservableCollection<DisplayImage> wOItems;

        public ObservableCollection<DisplayImage> WOItems
        {
            get => wOItems;
            set => wOItems = value;
        }

        #endregion

        public WOAppointment() { }

        public WOAppointment(GetWOsResponse baseWO)
        {
            this.baseWO = baseWO;
            this.Start = baseWO.Start_Date!.Value;
            this.End = baseWO.End_Date!.Value;
            this.Subject = baseWO.Asset_No;
            this.Body = baseWO.WO_Desc;
            this.WO_State = baseWO.WO_State;
            this.WO_Reason = baseWO.WO_Reason;
            this.WO_Category = baseWO.WO_Category;
            this.Color = COMMON.SConsts.StringToColor(baseWO.State_Color);
            this.IsAllDay = this.End.Subtract(this.Start).TotalHours > 24;
            //this.RecurrenceRule.Pattern.CO

            SetCurrentWOItems();
        }

        #region METHOD SetCurrentWOItems

        private void SetCurrentWOItems()
        {
            WOItems = new ObservableCollection<DisplayImage>();

            if (baseWO.Person_Take_Date is not null)
            {
                WOItems.Add(new DisplayImage("front_hand", Colors.Green));
            }

            if (baseWO.Part_Count > 0)
            {
                WOItems.Add(new DisplayImage("extension", Colors.Green));
            }

            if (baseWO.File_Count > 0)
            {
                WOItems.Add(new DisplayImage("collections", Colors.Green));
            }

            if (baseWO.Is_Scheduled_Planned)
            {
                WOItems.Add(new DisplayImage("insert_invitation", Colors.Green));
            }
        }

        #endregion

        public GetWOsResponse GetBaseWO() =>
            baseWO;

    }
}
