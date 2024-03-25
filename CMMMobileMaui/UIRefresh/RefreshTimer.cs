using System.Collections.Concurrent;

namespace CMMMobileMaui.UIRefresh
{
    internal class RefreshTimer
    {
        private PeriodicTimer? timer;
        private List<IUIRefresh> itemsToRefresh = new List<IUIRefresh>();
        private TimeSpan period;
        public RefreshTimer(TimeSpan period)
        {
            this.period = period;
        }

        public void AddItemsToRefresh(IEnumerable<IUIRefresh> items)
        {
            foreach (var item in items)
            {
                itemsToRefresh.Add(item);
            }
        }

        public void AddItemToRefresh(IUIRefresh item)
        {
            itemsToRefresh.Add(item);
        }

        public void ClearItemsToRefresh()
        {
            Stop();

            itemsToRefresh.Clear();
        }

        public void Start()
        {
            InitializeTimer();
            ProcessItemsToRefresh();
        }

        private void InitializeTimer()
        {
            if (!IsTimerRunning())
            {
                timer = new PeriodicTimer(period);
            }
        }

        private bool IsTimerRunning() => timer != null;

        private async void ProcessItemsToRefresh()
        {
            if (!IsTimerRunning())
                return;

            while (await timer!.WaitForNextTickAsync())
            {
                var tempItems = itemsToRefresh.ToList();

                foreach (var item in tempItems)
                {
                    item.UIRefresh();
                }
            }
        }

        public void Stop()
        {
            if (IsTimerRunning())
            {
                timer!.Dispose();
                timer = null;
            }
        }
    }
}
