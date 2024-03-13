using System.Collections.ObjectModel;
using System.Windows.Input;
using static CMMMobileMaui.API.Contracts.Models.COMMON.Enums;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    public interface IFilterViewModel
    {
        ObservableCollection<FilterItem> FilterItemList
        {
            get; set;
        }

        ObservableCollection<FilterItem> UsedFilterItemList
        {
            get; set;
        }

        ObservableCollection<SortItem> SortItemList
        {
            get;
            set;
        }

        bool IsSorting { get; set; }

        SortItem CurrentSortItem
        {
            get;
            set;
        }

        SortDirection CurrentSortDirection
        {
            get; set;
        }

        ICommand ConfirmFilterCommand
        {
            get;
        }

        ICommand SortDirectionCommand
        {
            get;
        }
    }
}
