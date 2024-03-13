
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartTakerView : PageBase<VM.VMPartGiver2>
    {
        #region FIELDS

        private bool lastState = false;

        #endregion

        #region Cstr

        public PartTakerView()
        {
            InitializeComponent();
            ViewModel.OnCurrentPartChange += _vmMain_OnCurrentPartChange;

            if (!App.IsZebraScanIconVisible)
            {
                //zebraIcon.DisableAnimation();
                var parent = zebraIcon.Parent as Grid;

                if (parent != null)
                {
                    parent.Children.Remove(zebraIcon);
                }
            }
            else
            {
                ViewModel.InitScannerOnPage(zebraIcon);
            }
        }

        #endregion

        #region EVENT _vmMain_OnCurrentPartChange

        private void _vmMain_OnCurrentPartChange(object sender, bool e)
        {
            if (e != lastState)
            {
                lastState = e;

                Animation animation = new Animation();

                double gHeight = gMenuMain.HeightRequest;

                if (e)
                {
                    //height 80
                    //     fCurrentPart.TranslateTo(0, 0, 500);
                    //   fCurrentPart.IsVisible = true;
                    gCurrentPart.IsVisible = true;
                    btnCancelPart.IsVisible = false;
                    btnSavePart.IsVisible = false; ;

                    //animation.Add(0, 1, new Animation((value) =>
                    //{
                    //    gMenuMain.HeightRequest = gHeight + (value * 44);
                    //}, 0, 1, easing: Easing.SinInOut));
                }
                else
                {
                    //height 36
                    //  fCurrentPart.TranslateTo(0, -200, 500);

                    //animation.Add(0, 1, new Animation((value) =>
                    //{
                    //    gMenuMain.HeightRequest = gHeight - (value * 44);

                    //}, 0, 1, easing: Easing.SinInOut, () =>
                    //{
                    //    gMenuMain.HeightRequest = 36;

                    //}));

                    //   fCurrentPart.IsVisible = false;

                    gCurrentPart.IsVisible = false;
                    btnCancelPart.IsVisible = true;
                    btnSavePart.IsVisible = true;
                }

              //  animation.Commit(gMenuMain, "mm", length: 500);
            }
        }

        #endregion
    }
}