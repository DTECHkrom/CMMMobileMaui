using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.Behevior
{
    public class ListViewBehavior: Behavior<ListView>
    {
        private ListView _listView;

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            _listView = bindable;

            _listView.ItemSelected += _listView_ItemSelected;
            _listView.ItemTapped += _listView_ItemTapped;
        }

        private void _listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item != null)
            {

            }
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

            }
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);

            _listView.ItemSelected -= _listView_ItemSelected;
            _listView.ItemTapped -= _listView_ItemTapped;


        }
    }
}
