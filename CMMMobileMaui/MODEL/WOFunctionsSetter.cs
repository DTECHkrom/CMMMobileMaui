using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using CMMMobileMaui.COMMON;

namespace CMMMobileMaui.MODEL
{
    internal class WOFunctionsSetter
    {
        private readonly GetWOsResponse woItem;
        private readonly VM.VMWorkOrderListAll vmWO;

        public WOFunctionsSetter(GetWOsResponse woItem
            , VM.VMWorkOrderListAll vmWO)
        {
            this.woItem = woItem;
            this.vmWO = vmWO;
        }

        #region METHOD GetFunctions

        internal List<FunctionItem> GetFunctions()
        {
            var woFunctions = new List<FunctionItem>();

            woFunctions.Add(new FunctionItem() { ImageInfo = new DisplayImage("more_horiz", Colors.Black), Command = vmWO.ShowItemCommand });

            if (CanTakeWO())
            {
                woFunctions.Add(new FunctionItem() { ImageInfo = new DisplayImage("pan_tool", Colors.Black), Command = vmWO.TakeItemCommand });
            }

            if (CanCloseWO())
            {
                woFunctions.Add(new FunctionItem() { ImageInfo = new DisplayImage("check_circle_outline", Colors.Black), Command = vmWO.CloseWOCommand });
            }

            return woFunctions;
        }

        #endregion

        #region METHOD CanTakeWO

        private bool CanTakeWO() => 
            woItem.Person_Take_Date is null && woItem.Close_Date is null;

        #endregion

        #region METHOD CanCloseWO

        private bool CanCloseWO()
        {
            if (!woItem.Close_Date.HasValue)
            {
                if (woItem.PlanID.HasValue)
                {
                    if (woItem.Plan_Act_Count == 0
                        && woItem.Act_Count > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    if (woItem.Act_Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
