using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.MODEL
{
    internal class WOIconsSetter
    {
        private readonly GetWOsResponse woItem;

        public WOIconsSetter(GetWOsResponse woItem)
        {
            this.woItem = woItem;
        }

        #region METHOD SetCurrentWOItems

        internal List<DisplayImage> GetIcons()
        {
            var woIcons = new List<DisplayImage>();

            if (woItem.Take_Date.HasValue)
            {
                if (woItem.Person_Take_Date.HasValue)
                {
                    woIcons.Add(new DisplayImage("pan_tool", Colors.DarkGreen));
                }

                if (IsOtherUserTakeWO())
                {
                    woIcons.Add(new DisplayImage("how_to_reg", Colors.DarkGreen));
                }
            }
            
            if (woItem.Part_Count > 0)
            {
                woIcons.Add(new DisplayImage("extension", Colors.DarkGreen));
            }

            if (woItem.File_Count > 0)
            {
                woIcons.Add(new DisplayImage("collections", Colors.DarkGreen));
            }

            if (woItem.Is_Scheduled_Planned)
            {
                woIcons.Add(new DisplayImage("event", Colors.DarkGreen));
            }

            if (woItem.PlanID.HasValue)
            {
                woIcons.Add(new DisplayImage("schedule", Colors.DarkGreen));
            }

            return woIcons;
        }

        private bool IsOtherUserTakeWO() =>
            woItem._TakePersonsList.Any(x => x != API.MainObjects.Instance.CurrentUser!.PersonID);

        #endregion

    }
}
