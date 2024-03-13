using Mopups.Pages;

namespace CMMMobileMaui.CUST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCustomListPicker : PopupPage
    {
        private CustomListPicker? parent;

        public ListCustomListPicker()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (this.BindingContext != null)
            {
                parent = this.BindingContext as CustomListPicker;

                if (parent != null 
                    && parent.SelectedItem != null)
                {
                    try
                    {
                        cvMain.ScrollTo(parent.SelectedItem, ScrollToPosition.MakeVisible);
                    }
                    catch (Exception) { }
                }
            }

            base.OnAppearing();
        }
    }
}