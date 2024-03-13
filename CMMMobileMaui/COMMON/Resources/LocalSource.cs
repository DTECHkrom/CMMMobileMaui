using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace CMMMobileMaui.COMMON.Resources
{
    public class LocalSource: ITextGetter
    {
        const string ResourceId = "CMMMobileMaui.Resources.AppResource";
        private string languageCode;

        #region PROPERTY Resmgr

        private readonly Lazy<ResourceManager> Resmgr =
            new Lazy<ResourceManager>(() =>
                new ResourceManager(ResourceId, typeof(LocalSource)
                        .GetTypeInfo().Assembly));

        #endregion
        public string GetText(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            var ci = CultureInfo.CreateSpecificCulture(languageCode);
            var translation = Resmgr.Value.GetString(key, ci);

            if (translation == null)
            {
                translation = key; // returns the key, which GETS DISPLAYED TO THE USER
            }
            return translation;
        }

        public Task InitData(string language)
        {
            languageCode = language;
            return Task.CompletedTask;
        }
    }
}
