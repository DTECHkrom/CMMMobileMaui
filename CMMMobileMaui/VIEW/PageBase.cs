using CMMMobileMaui.COMMON;
using CMMMobileMaui.CUST;
using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VIEW
{
    public class PageBase : ContentPage
    {
        private readonly WeakEventManager weakEventManager = new WeakEventManager();

        public event EventHandler OnPageUnload
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }

        public PageBase()
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0f));
            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(COMMON.SConsts.COLOR_5), 1f));
            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);

            this.Background = linearGradientBrush;

            this.Loaded += PageBase_Loaded;
            this.Unloaded += PageBase_Unloaded;
        }

        private void PageBase_Unloaded(object? sender, EventArgs e)
        {
            //if (this.BindingContext is COMMON.ViewModelBase view)
            //{
            //    // view.ScanManager?.DisableScan();
            //}

            weakEventManager.HandleEvent(this, e, nameof(OnPageUnload));
        }

        private void PageBase_Loaded(object? sender, EventArgs e)
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

        protected override void OnAppearing()
        {
            if (this.BindingContext is IScanItems scanItems)
            {
                App.CurrentScanManager?.Init(scanItems);
                App.CurrentScanManager?.EnableScan();
            }
            else
            {
                App.CurrentScanManager?.DisableScan();
            }

            base.OnAppearing();
        }
    }



    public class PageBase<T> : PageBase where T : COMMON.ViewModelBase
    {
        public T ViewModel
        {
            get;
            set;
        }

        public PageBase() : base()
        {
            this.BindingContext = ViewModel = (T)(API.MainObjects.Instance.ServiceProvider ??
                throw new ArgumentNullException("ServiceProvider is null"))
                .GetRequiredService(typeof(T));
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.IsBusy)
            {
                return ViewModel.IsBusy;
            }

            if (!ViewModel.CanMoveBack)
            {
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
