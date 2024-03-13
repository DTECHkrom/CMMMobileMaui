using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using CMMMobileMaui.COMMON.Resources;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CMMMobileMaui.API;

namespace CMMMobileMaui.CONV
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        #region FIELDS

        private static DictionaryResourceManager dictionaryManager;

        #endregion

        #region ACTION

        public static Action RefreshAppText
        {
            get;
            set;
        }

        #endregion

        #region METHOD InitDictionarySource
        public static async void InitDictionarySource(DictionaryResourceManager dm)
        {
            dictionaryManager = dm;

            if (!string.IsNullOrEmpty(languageCode))
            {
                await dictionaryManager.InitData(languageCode, false);
            }
        }

        #endregion

        #region PROPERTY LanguageCode

        private static string languageCode;
        public static string LanguageCode
        {
            get
            {
                return languageCode;
            }
            private set
            {
                languageCode = value;
            }
        }

        #endregion

        public static async Task InitLanguage(string langCode, bool shouldUseHost)
        {
            if (langCode != Thread.CurrentThread.CurrentCulture.Name)
            {    
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langCode);
                CultureInfo.CurrentUICulture = new CultureInfo(langCode, true);
                CultureInfo.CurrentCulture = new CultureInfo(langCode, true);
            }

            LanguageCode = langCode;

            if (dictionaryManager != null)
            {
                await dictionaryManager.InitData(langCode, shouldUseHost);
                RefreshAppText?.Invoke();
                await Task.Delay(500);
            }
        }

        #region PROPERTY Text

        public string Text
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsUpper
        public bool IsUpper
        {
            get;
            set;
        }

        #endregion

        #region METHOD ProvideValue

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            return  GetTextFormat(dictionaryManager.GetText(Text));
        }

        #endregion

        #region METHOD GetTextFormat

        private string GetTextFormat(string text) =>
            IsUpper ? text.ToUpperInvariant() : text;

        #endregion

        #region METHOD GetResourceText

        public static string GetResourceText(string text) =>
            dictionaryManager.GetText(text);

        #endregion
    }
}
