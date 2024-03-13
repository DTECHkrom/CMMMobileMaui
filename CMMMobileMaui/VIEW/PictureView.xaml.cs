using CMMMobileMaui.API.Contracts;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureView : ContentPage
    {
        public PictureView(WOFile fm)
        {
            InitializeComponent();

            imageEditor.Source = ImageSource.FromStream(() => new MemoryStream(fm.Data));
        }
    }
}