using System;
using System.Linq;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderSingleStaticView : ContentView
    {
        private VM.VMWorkOrderSingle vmMain;
        public WorkOrderSingleStaticView()
        {
            InitializeComponent();
        }

        //private void ViewCell_BindingContextChanged(object sender, EventArgs e)
        //{
        //    if (sender != null)
        //    {
        //        var view = sender as ViewCell;

        //        if (view != null)
        //        {
        //            var item = view.BindingContext as vWOActivity;

        //            if (item != null)
        //            {
        //                if (item._ActPersonList.Contains(CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID))
        //                {
        //                    view.ContextActions.RemoveAt(0);
        //                }
        //            }
        //        }
        //    }
        //}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            vmMain = this.BindingContext as VM.VMWorkOrderSingle;
        }
    }
}