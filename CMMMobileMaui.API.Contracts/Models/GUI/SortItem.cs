using static CMMMobileMaui.API.Contracts.Models.COMMON.Enums;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    public class SortItem
    {
        public string PropertyName { get; private set; }

        public string DisplayName { get; private set; }

        public SortDirection SortDirection { get; private set; } = SortDirection.Up;

        public SortItem(string propertyName, SortDirection sortDirection)
        {
            PropertyName = propertyName;
            SortDirection = sortDirection;
            DisplayName = propertyName;

            if(COMMON.Container.TextDictionaryResource != null) 
                DisplayName = COMMON.Container.TextDictionaryResource.GetText(propertyName);
        }

        public SortItem WithSortDirection(SortDirection sortDirection)
        {
            SortDirection = sortDirection;
            return this;
        }
    }
}
