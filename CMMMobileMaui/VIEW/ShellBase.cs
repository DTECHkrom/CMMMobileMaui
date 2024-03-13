using CMMMobileMaui.SCAN;

namespace CMMMobileMaui.VIEW
{
    public class ShellBase : Shell
    {
        private readonly WeakEventManager weakEventManager = new WeakEventManager();
        public event EventHandler OnPageUnload
        {
            add => weakEventManager.AddEventHandler(value);
            remove => weakEventManager.RemoveEventHandler(value);
        }

        public ShellBase()
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

            if (this.BindingContext is IScanItems view)
            {
                App.CurrentScanManager?.Init(view);
                App.CurrentScanManager?.EnableScan();
            }
        }

        private void Lc_Unloaded(object? sender, EventArgs e)
        {
            if (this.BindingContext is COMMON.ViewModelBase view)
            {
                //view.ScanManager?.DisableScan();
            }

            weakEventManager.HandleEvent(this, e, nameof(OnPageUnload));
        }
    }

    public class ShellBase<T> : ShellBase where T : COMMON.ViewModelBase
    {
        public T ViewModel
        {
            get;
            set;
        }

        public ShellBase() : base()
        {
            this.BindingContext = ViewModel = (T)(API.MainObjects.Instance.ServiceProvider
                ?? throw new ArgumentNullException("ServiceProvider can not be null"))
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
