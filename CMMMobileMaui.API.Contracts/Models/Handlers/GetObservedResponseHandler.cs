using System;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using static CMMMobileMaui.API.Contracts.Models.COMMON.Enums;

namespace CMMMobileMaui.API.Contracts.Models.Handlers
{
    public class GetObservedResponseHandler
    {
        private static readonly int diffHours = 12;

        public static bool IsAfterTime(GetObservedResponse getObservedResponse)
        {
            if (getObservedResponse.ListType == (int)ObservedDeviceListType.History)
                return false;

            getObservedResponse._IsOverTime = TimeSpan.FromTicks(DateTime.UtcNow.Ticks)
                        .Subtract(TimeSpan.FromTicks(getObservedResponse.Select_Date.Value.Ticks))
                        .TotalHours >= diffHours;

            return getObservedResponse._IsOverTime;
        }


    }
}
