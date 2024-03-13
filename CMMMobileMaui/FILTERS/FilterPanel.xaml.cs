using Mopups.Services;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.Models.GUI;
using CMMMobileMaui.COMMON;
using static CMMMobileMaui.API.Contracts.Models.COMMON.Enums;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.FILTERS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPanel : Grid, IFilterViewModel
    {

        #region PROPERTY FilterItemList

        public ObservableCollection<FilterItem> FilterItemList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY UsedFilterItemList

        public ObservableCollection<FilterItem> UsedFilterItemList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY SortItemList

        private ObservableCollection<SortItem> sortItemList;

        public ObservableCollection<SortItem> SortItemList
        {
            get => sortItemList;
            set
            {
                sortItemList = value;
            }
        }

        #endregion

        #region METHOD IsFilterUsed

        private bool IsFilterUsed =>
            FilterItemList != null
            && FilterItemList.FirstOrDefault(tt => tt.IsInUse) != null;

        #endregion

        #region PROPERTY IsSorting

        public bool IsSorting
        {
            get;
            set;
        }

        #endregion

        #region PROEPRTY CurrentSortItem

        public SortItem CurrentSortItem
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentSortDirection

        public SortDirection CurrentSortDirection
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY UseFilterOnStartProperty

        public static BindableProperty UseFilterOnStartProperty = BindableProperty.Create(nameof(UseFilterOnStart), typeof(bool), typeof(FilterPanel));

        public bool UseFilterOnStart
        {
            get
            {
                return (bool)GetValue(UseFilterOnStartProperty);
            }

            set
            {
                SetValue(UseFilterOnStartProperty, value);
            }
        }

        #endregion

        #region COMMAND ConfirmFilterCommand

        public ICommand ConfirmFilterCommand
        {
            get;
            set;
        }

        #endregion

        #region COMMAND OpenFilterCommand

        public ICommand OpenFilterCommand
        {
            get;
        }

        #endregion

        #region COMMAND ClearFilterCommand

        public ICommand ClearFilterCommand
        {
            get;
            set;
        }

        #endregion

        public ImageButton BtnClear =>
            btnFilter;

        public ICommand SortDirectionCommand
        {
            get;
        }

        #region Cstr

        public FilterPanel()
        {
            InitializeComponent();

            this.BindingContext = this;

            OpenFilterCommand = new Command((obj) =>
            {
                if (!SConsts.CanOpenPopupPage(typeof(FilterListPopup)))
                    return;

                //  var baseCanMoveBack = ViewModelBase.CanMoveBack;

                FilterListPopup fiterListPopup = new FilterListPopup();
                fiterListPopup.BindingContext = this;

                fiterListPopup.Disappearing += (s, e) =>
                {
                    //         ViewModelBase.CanMoveBack = baseCanMoveBack;
                };

                //   ViewModelBase.CanMoveBack = false;
                MopupService.Instance.PushAsync(fiterListPopup);
            });

            SortDirectionCommand = new Command(() =>
            {
                if (CurrentSortDirection == SortDirection.Down)
                {
                    CurrentSortDirection = SortDirection.Up;
                }
                else
                {
                    CurrentSortDirection = SortDirection.Down;
                }

                OnPropertyChanged(nameof(CurrentSortDirection));
            });

            btnFilter.Command = OpenFilterCommand;
          //  TouchEffect.SetCommand(btnFilter, OpenFilterCommand);
        }

        #endregion

        #region METHOD SetUsedFilters

        public void SetUsedFilters()
        {
            UsedFilterItemList = new ObservableCollection<FilterItem>(FilterItemList.Where(tt => tt.IsInUse)
                .OrderBy(filter => filter.DisplayName.Length));
            OnPropertyChanged(nameof(UsedFilterItemList));

            if (IsFilterUsed)
            {
                btnFilter.Source = ImageSource.FromFile("filter_red.png");
            }
            else
            {
                btnFilter.Source = ImageSource.FromFile("filter.png");

                //  IconTintColorEffect.SetTintColor(btnFilter, Color.Black);
            }
        }

        #endregion

        #region METHOD ClearDataAfterBack

        public void ClearDataAfterBack()
        {
            ClearFilterCommand.Execute(null);
        }

        #endregion

    }

    public partial class FilterPanel<T> : FilterPanel where T : class//, IFilterViewModel
    {
        #region FIELDS

        private bool wasFirstStart = false;
        public event EventHandler<T> OnFilterChanged;
        private T filterItem;

        #endregion

        #region Cstr
        public FilterPanel() : base()
        {
            var list = FilterItemHelpers.FromAPIFilterObject(out filterItem);
            FilterItemList = new ObservableCollection<FilterItem>(list);
            SetSortingList();

            ClearFilterCommand = new Command((obj) =>
            {
                foreach (var item in FilterItemList)
                {
                    item.ClearValue();
                }

                OnPropertyChanged(nameof(FilterItemList));
                SetUsedFilters();

                OnFilterChanged?.Invoke(this, null);
            });

            ConfirmFilterCommand = new Command((obj) =>
            {
                SetUsedFilters();
                SConsts.CloseLastPopup();

                SortItem sortItem = null;

                if (IsSorting)
                {
                    sortItem = CurrentSortItem.WithSortDirection(CurrentSortDirection);
                }

                OnFilterChanged?.Invoke(this, FilterItemHelpers.FromUsedFilter<T>(FilterItemList.ToList(), sortItem));
            });
            

           // Maui.TouchEffect.TouchEffect.SetLongPressCommand(BtnClear, ClearFilterCommand);
        }

        #endregion

        #region METHOD SetSortingList

        public void SetSortingList()
        {
            if (filterItem is SortableItem sortableItem)
            {
                if (sortableItem == null)
                {
                    return;
                }

                SortItemList = new ObservableCollection<SortItem>(sortableItem.SortingPropertyList);

                IsSorting = SortItemList.Count > 0;

                if (sortableItem.DefaultSortItem != null)
                {
                    CurrentSortItem = sortableItem.DefaultSortItem;
                    CurrentSortDirection = sortableItem.DefaultSortItem.SortDirection;
                }
            }
        }

        #endregion

        #region METHOD FilterOnStart
        public void FilterOnStart()
        {
            if (UseFilterOnStart)
            {
                if (!wasFirstStart)
                {
                    ConfirmFilterCommand.Execute(null);
                    wasFirstStart = true;
                }
            }
        }

        #endregion

        #region METHOD

        public bool WasFirstFilterExecuteOnStart() =>
            UseFilterOnStart ? wasFirstStart : true;

        #endregion

        #region METHOD SetDefaultFilter

        public void SetDefaultFilter(T t)
        {
            filterItem = t;
            var list = FilterItemHelpers.FromAPIFilterObject(t);

            FilterItemList = new ObservableCollection<FilterItem>(list);

            SetSortingList();
            SetUsedFilters();
        }

        #endregion

        #region METHOD SetUsedFilters

        public void SetUsedFilters(T t)
        {
            SetDefaultFilter(t);

            SortItem sortItem = null;

            if (IsSorting)
            {
                sortItem = CurrentSortItem.WithSortDirection(CurrentSortDirection);
            }

            OnFilterChanged?.Invoke(this, FilterItemHelpers.FromUsedFilter<T>(FilterItemList.ToList(), sortItem));
        }

        #endregion

        public void FiltersToRemove(List<string> filterNames)
        {
            if (filterNames.Count > 0)
            {
                foreach (var filter in filterNames)
                {
                    var filterItem = FilterItemList.FirstOrDefault(tt => tt.Name == filter);

                    if (filterItem != null)
                    {
                        FilterItemList.Remove(filterItem);
                    }
                }
            }
        }

        #region METHOD GetUsedFilter

        public T GetUsedFilter() =>
            filterItem;

        #endregion
    }
}