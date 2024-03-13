using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.COMMON.Resources
{
    public class DBSource : ITextGetter
    {
        private string lang = string.Empty;

        #region PROPERTY DatabaseResource

        public List<GetAppDictResponse> LangItems
        {
            get;
            set;
        }

        #endregion

        #region METHOD GetText

        public string GetText(string key)
        {
            if (LangItems != null)
            {
                var item = LangItems.FirstOrDefault(tt => tt.Key.ToLower() == key.ToLower());

                return item == null ? key : item.Value;
            }

            return key;
        }

        #endregion

        #region METHOD InitData

        public async Task InitData(string language)
        {
            if (!lang.Equals(language.ToLower()) || LangItems == null)
            {
                this.lang = language;

                var sourceMethod = (IOtherController)API.MainObjects
                    .Instance
                    .ServiceProvider.GetService(typeof(IOtherController));

                var result = await sourceMethod.GetDict(new API.Contracts.v1.Requests.Other.GetAppDictRequest
                {
                    Lang = lang
                    ,
                    Source = "M"
                });

                if (result != null
                    && result.IsValid)
                {
                    LangItems = result.Data.ToList();
                }
            }
        }

        #endregion
    }
}
