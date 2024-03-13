using CMMMobileMaui.COMMON;
using CMMMobileMaui.CUST;

namespace CMMMobileMaui.VIEW
{
    public class ViewContentBase : ContentView
    {
        public ViewContentBase()
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0f));
            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(COMMON.SConsts.COLOR_5), 1f));
            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);

            this.Background = linearGradientBrush;

            this.Unloaded += Lc_Unloaded;
            this.Loaded += Lc_Loaded;
        }

        private void Lc_Loaded(object? sender, EventArgs e)
        {
            App.CurrentScanManager?.DisableScan();

            if (this.BindingContext is ScannerViewModelBase scanItems)
            {
                if (scanItems.ScanIcon is null)
                {
                    var zebraIcon = this.Content.GetVisualTreeDescendants()
                    .Where(tt => tt.GetType() == typeof(ZebraScanIcon))
                    .FirstOrDefault();

                    if (zebraIcon != null)
                    {
                        scanItems.InitScannerOnPage((ZebraScanIcon)zebraIcon);
                    }

                }

                App.CurrentScanManager?.Init(scanItems);
                App.CurrentScanManager?.EnableScan();
            }
        }

        private void Lc_Unloaded(object? sender, EventArgs e)
        {
            if (this.BindingContext is COMMON.ViewModelBase view)
            {
                //view.ScanManager?.DisableScan();
            }
        }
    }

    public class ViewContentBase<T> : ViewContentBase where T : COMMON.ViewModelBase
    {
        public T ViewModel
        {
            get;
            set;
        }

        public ViewContentBase()
        {
            this.BindingContext = ViewModel = (T)(API.MainObjects.Instance.ServiceProvider
                ?? throw new ArgumentNullException("ServiceProvider can not be null"))
                .GetRequiredService(typeof(T));
        }
    }
}
