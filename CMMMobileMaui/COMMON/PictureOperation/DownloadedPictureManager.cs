using CMMMobileMaui.API.Contracts;

namespace CMMMobileMaui.COMMON.PictureOperation
{
    public class DownloadedPictureManager
    {
        #region FIELDS

        private Timer _clearTimer = null;
        private Dictionary<string, List<WOFile>> Pictures = new Dictionary<string, List<WOFile>>();
        private Dictionary<string, DateTime> PicturesDate = new Dictionary<string, DateTime>();
        private object lockObj = new object();

        private static Lazy<DownloadedPictureManager> instance = new Lazy<DownloadedPictureManager>(() => new DownloadedPictureManager());

        #endregion

        #region Cstr

        private DownloadedPictureManager()
        {

        }

        #endregion

        #region PROPERTY Instance

        public static DownloadedPictureManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        #endregion

        #region METHOD ClearPictures

        public void ClearPictures()
        {
            if (_clearTimer != null)
            {
                _clearTimer.Dispose();
                _clearTimer = null;
            }

            Pictures.Clear();
            PicturesDate.Clear();
        }

        #endregion

        #region METHOD CreateClearTimer

        private void CreateClearTimer()
        {
            if (_clearTimer == null)
            {
                _clearTimer = new Timer(new TimerCallback((obj) =>
                {
                    lock (lockObj)
                    {
                        var items = PicturesDate.Where(tt => tt.Value.Subtract(DateTime.Now).TotalMinutes <= -10)
                        .Select(tt => tt.Key)
                        .ToList();

                        if (items.Count > 0)
                        {
                            foreach (var id in items)
                            {
                                Pictures.Remove(id);
                                PicturesDate.Remove(id);
                            }
                        }
                    }

                }), null, 10000, 60000);
            }
        }

        #endregion

        #region METHOD GetPicturesByKey

        public List<WOFile> GetPicturesByKey(string key)
        {
            if (Pictures.ContainsKey(key))
            {
                PicturesDate[key] = DateTime.Now;
                return Pictures[key];
            }

            return null;
        }

        #endregion

        #region METHOD AddPictures

        public void AddPictures(string key, List<WOFile> pictures)
        {
            CreateClearTimer();
            Pictures[key] = pictures;
            PicturesDate[key] = DateTime.Now;
        }

        #endregion
    }
}
