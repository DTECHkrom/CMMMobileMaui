using System;
using System.Linq;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkOrderPlanMainView : PageBase
	{
		private ViewCell _vc;

		public WorkOrderPlanMainView ()
		{
			InitializeComponent ();
		}

		//private void ViewCell_BindingContextChanged(object sender, EventArgs e)
		//{
		//	_vc = sender as ViewCell;

		//	if (_vc != null)
		//	{
		//		var wo = _vc.BindingContext as vWorkOrder;

		//		if (wo == null)
		//			return;

		//		if (wo._TakePersonsList.Contains(CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID) || wo.Close_Date.HasValue)
		//		{
		//			_vc.ContextActions.RemoveAt(1);
		//		}
		//	}
		//}
	}
}