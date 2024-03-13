using CommunityToolkit.Mvvm.ComponentModel;
using Telerik.Maui.Controls.Calendar;

namespace CMMMobileMaui.MODEL
{
    public class CalendarTypeViewSelection : ObservableObject
    {
        #region PROPERTY CurrentViewMode

        private CalendarDisplayMode currentViewMode;

        public CalendarDisplayMode CurrentViewMode
        {
            get => currentViewMode;
            protected set
            {
                SetProperty(ref currentViewMode, value);
            }
        }

        #endregion

        #region PROPERTY IsSelected

        public bool IsSelected
        {
            get;
            protected set;
        }

        #endregion

        #region PROPERTY Name

        public string Name
        {
            get;
            protected set;
        }

        #endregion

        #region Cstr

        public CalendarTypeViewSelection(CalendarDisplayMode viewMode)
        {
            CurrentViewMode = viewMode;
            SetMainName();
        }

        #endregion

        #region METHOD SetMainName

        private void SetMainName()
        {
            Name = CONV.TranslateExtension.GetResourceText("cv_" + CurrentViewMode.ToString().ToLower());
        }

        #endregion

        #region METHOD SelectView

        private static void SelectView(CalendarTypeViewSelection selectedView, IEnumerable<CalendarTypeViewSelection> viewList)
        {
            foreach (var view in viewList)
            {
                view.IsSelected = false;
            }

            selectedView.IsSelected = true;
        }

        #endregion

        #region METHOD SetCurrentSelection

        public static CalendarTypeViewSelection SetCurrentSelection(CalendarDisplayMode viewMode, IEnumerable<CalendarTypeViewSelection> viewList)
        {
            var view = viewList.FirstOrDefault(tt => tt.CurrentViewMode == viewMode);

            if (view != null)
            {
                SelectView(view, viewList);
                return view;
            }

            return null;
        }

        #endregion
    }
}
