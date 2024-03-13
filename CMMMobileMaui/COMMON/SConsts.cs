using Microsoft.Maui.Graphics.Converters;
using Mopups.Services;
using CMMMobileMaui.API.Contracts.Models.GUI;
using static CMMMobileMaui.API.Contracts.Models.COMMON.Enums;

namespace CMMMobileMaui.COMMON
{
    public static class SConsts
    {

        public static string COLOR_1 = "#ff4d00";
        public static string COLOR_2 = "#ff7400";
        public static string COLOR_3 = "#ff9a00";
        public static string COLOR_4 = "#ffc100";
        public static string COLOR_5 = "#f4f2cd";

        public static string COLOR_TEXT = "#ff0000";
        public static string COLOR_GREEN = "#53CC0E";
        public static string COLOR_RED = "#f93c16";
        public static string COLOR_YELLOW = "#F4D977";

        public static string TEXT_COLOR = "#000000";

        public static int ACT_CAT_OTHER_DEP = 17;

        public static readonly string PART_MSG_GIVE = "PartGiveMsg";
        public static readonly string PART_SCAN_INV = "ScanPartInv";
        public static readonly string PART_SCAN_GIVER = "ScanPartGiver";
        public static readonly string PART_SCAN = "ScanPart";
        public static readonly string DEV_WO_ADD = "DevWOAdd";
        public static readonly string DEV_REF_BACK = "DevRefBack";
        public static readonly string DEV_HIST_REF = "DevHistListRefresh";
        public static readonly string DEV_SEARCH_REF = "DevSearchListRefresh";
        public static readonly string SC_DEV_SEARCH = "SCDevSearch";
        public static readonly string DEV_OPEN_SEARCH = "DevOpenSearch";
        public static readonly string DEV_MAIN_PAGE = "DevMainPage";
        public static readonly string DEV_FIND_GIVE = "DevFindGive";
        public static readonly string DEV_SCAN_TEXT = "ScanText";
        public static readonly string DEV_SCAN_CODE = "ScanCode";
        public static readonly string APP_SETT = "AppSett";
        public static readonly string DEV_ZEBRA = "DevZebra";
        public static readonly string WO_LOGIN = "WOLogin";
        public static readonly string WO_GIVE = "WOGive";
        public static readonly string SHELL_LOGIN = "ShellLogin";
        public static readonly string SUB_SCAN_PERSON_SETT = "ScanPersonSett";
        public static readonly string SUB_SCAN_PERSON_SAVE = "ScanPersonSave";
        public static readonly string LOGIN_SCAN_PERSON = "ScanPersonLogin";
        public static readonly string DEV_SCAN_TAG = "ScanDevText";
        public static readonly string SET_DEVICE = "SetDevice";
        public static readonly string PART_SCAN_TEXT = "ScanPartText";

        public static NavigationPage GetBaseNavigationPage(Page page)
        {
            return new NavigationPage(page) { BarBackgroundColor = Color.FromArgb(COLOR_1) };
        }

        //public static PageBase GetBaseNavigationPage(PageBase page)
        //{
        //    return page;
        //}

        #region METHOD GetByteArrayFromStream

        public static byte[] GetByteArrayFromStream(Stream s)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                s.CopyTo(ms);
                return ms.ToArray();
            }
        }

        #endregion

        #region METHOD GetStateColor

        public static string GetStateColor(int stateID)
        {
            if (stateID == 1)
            {
                return COLOR_GREEN;
            }
            else if (stateID == 2)
            {
                return COLOR_YELLOW;
            }
            else if (stateID == 3)
            {
                return COLOR_RED;
            }

            return Colors.White.ToHex();
        }
        #endregion

        #region METHOD RunMainFrameAnimationOnPage

        public static void RunMainFrameAnimationOnPage(IAnimatable parent, List<Frame> frameList, List<ListView> listViewList = null, List<VisualElement> gridList = null, uint time = 2000)
        {
            Animation anim = new Animation();

            //if (frameList != null)
            //{
            //    foreach (var item in frameList)
            //    {
            //        item.AnchorY = 1;
            //        item.ScaleY = 0;
            //        item.Opacity = 0;

            //        anim.Add(0, 0.6f, new Animation(
            //        (t) =>
            //        {
            //            item.ScaleY = t;
            //        }, 0, 1, Easing.SinInOut));

            //        anim.Add(0.1, 0.3f, new Animation((t) =>
            //        {
            //            item.Opacity = t;
            //        }, 0, 1, Easing.SinInOut));
            //    }
            //}

            //if (listViewList != null)
            //{
            //    foreach (var item in listViewList)
            //    {
            //        item.Opacity = 0;

            //        anim.Add(0.4, 1f, new Animation((t) =>
            //        {
            //            item.Opacity = t;
            //        }, 0, 1, Easing.SinInOut));
            //    }
            //}

            //if(gridList != null)
            //{
            //    foreach (var item in gridList)
            //    {
            //        item.AnchorX = 0;
            //        item.TranslationX = -200;

            //        anim.Add(0, 1f, new Animation((t) =>
            //        {
            //            item.TranslationX = t;
            //        }, -200, 0, Easing.SinInOut));
            //    }
            //}

            //anim.Commit(parent, "animMain", 16, time);
        }

        #endregion

        private static Dictionary<string, Delegate> actionList = new Dictionary<string, Delegate>();

        public static void SetGlobalAction<T>(string name, Action<T> action)
        {
            // if(!actionList.ContainsKey(name))
            //   {
            actionList[name] = action;
            // }
        }

        public static void SetGlobalAction(string name, Action action)
        {
            // if(!actionList.ContainsKey(name))
            //   {
            actionList[name] = action;
            // }
        }

        public static bool IsActionExists(string name) =>
            actionList.ContainsKey(name);

        public static Action<T> GetGlobalAction<T>(string name)
        {
            return (Action<T>)actionList[name];
        }

        public static Action GetGlobalAction(string name)
        {
            if (actionList.ContainsKey(name))
            {
                return (Action)actionList[name];
            }

            return null;
        }


        #region METHOD GetSortedList

        public static IEnumerable<T> GetSortedList<T>(IEnumerable<T> orgList, SortableItem sortableItem)
        {
            if (sortableItem.DefaultSortItem == null)
            {
                return orgList;
            }

            if (sortableItem.DefaultSortItem.SortDirection == SortDirection.Up)
            {
                return orgList.OrderBy(tt => tt.GetType().GetProperty(sortableItem.DefaultSortItem.PropertyName).GetValue(tt, null)).ToList();
            }

            return orgList.OrderByDescending(tt => tt.GetType().GetProperty(sortableItem.DefaultSortItem.PropertyName).GetValue(tt, null)).ToList();
        }

        #endregion

        public static Color StringToColor(string colorValue, float luminosity = 0)
        {
            Color defColor = Colors.Transparent;

            if (!string.IsNullOrEmpty(colorValue))
            {
                try
                {
                    if (colorValue.StartsWith("#"))
                    {
                        defColor = Color.FromArgb(colorValue);
                    }
                    else
                    {
                        ColorTypeConverter c = new ColorTypeConverter();
                        defColor = (Color)c.ConvertFromInvariantString(colorValue);
                    }
                }
                catch (Exception) { }
            }

            return defColor.AddLuminosity(luminosity);
        }

        public enum StartPageProgressType
        {
            Host,
            Update,
            Login
        }


        #region METHOD CloseLastPopup

        public static async void CloseLastPopup()
        {
            if (MopupService.Instance.PopupStack.Count == 0)
                return;

            await MopupService.Instance.PopAsync();
        }

        #endregion

        public static bool CanOpenPopupPage(Type popUpType)
        {
            var currentPopup = MopupService.Instance.PopupStack.LastOrDefault();

            return currentPopup == null
                || popUpType != currentPopup.GetType();
        }

        public static string GetTimeTextFromMinutes(long minutes)
        {
            if (minutes <= 90)
            {
                return string.Format(" [{0} ']", minutes);
            }
            else if (minutes <= 2880)
            {
                decimal val = (minutes / 60);

                return string.Format(" [{0} h]", val.ToString("0.#"));
            }
            else
            {
                decimal val = (minutes / 1440);

                return string.Format(" [{0} d]", val.ToString("0.#"));
            }
        }
    }
}
