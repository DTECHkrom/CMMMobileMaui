
using CMMMobileMaui.COMMON;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    public partial class WebViewPageCS : ContentPage
	{
		public WebViewPageCS (string fileName)
		{
            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Children = {
                    new CustomWebView {
                        Uri = fileName,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };
        }
	}
}