using System.IO;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageView : ContentPage
	{
		public ImageView (byte[] data)
		{
			InitializeComponent ();

            imageEditor.Source = ImageSource.FromStream(() => new MemoryStream(data));
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			imageEditor.Source = null;
		}
	}
}