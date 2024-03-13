using System;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.Models.COMMON;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.COMMON.Resources
{
    public class DictionaryResourceManager: ITextDictionaryResource
    {
        #region FIELDS

        private readonly ITextGetter mainSource;
        private readonly ITextGetter localSource;
        private ITextGetter currentSource;
        private bool isHost = false;
        private string language = string.Empty;

        #endregion

        #region Cstr

        public DictionaryResourceManager(ITextGetter mainSource)
        {
            this.mainSource = mainSource;
            this.currentSource = mainSource;
        }

        public DictionaryResourceManager(ITextGetter mainSource, ITextGetter localSource) : this(mainSource)
        {
            this.localSource = localSource;
        }

        #endregion

        #region METHOD GetText

        public string GetText(string key) =>
            currentSource.GetText(key);

        #endregion

        #region METHOD InitData

        public async Task InitData(string language, bool shouldUseHost)
        {
            if (!this.language.Equals(language))
            {
                currentSource = mainSource;
                this.language = language;

                if (localSource != null)
                {
                    if (shouldUseHost)
                    {
                        isHost = await IsMainSourceValid();

                        if (!isHost)
                        {
                            currentSource = localSource;
                        }
                        else
                        {
                            currentSource = mainSource;
                        }
                    }
                    else
                    {
                        currentSource = localSource;
                    }
                }

                await currentSource.InitData(language);
            }
            else
            {
                if (shouldUseHost)
                {
                    CheckSourceForHost();
                }
            }
        }

        #endregion

        #region METHOD IsMainSourceValid

        private async Task<bool> IsMainSourceValid()
        {
            var source = (IOtherController)API.MainObjects.Instance.ServiceProvider.GetService(typeof(IOtherController));

            var result = await source.IsHost();

            if (result != null)
            {
                if (result.IsValid)
                {
                    return result.Data;
                }
            }

            return false;
        }

        #endregion

        #region METHOD CheckSourceForHost

        public async void CheckSourceForHost()
        {
            if (!isHost)
            {
                currentSource = mainSource;

                if (localSource != null)
                {
                    isHost = await IsMainSourceValid();

                    if (!isHost)
                    {
                        currentSource = localSource;
                    }
                    else
                    {
                        currentSource = mainSource;
                    }
                }

                await currentSource.InitData(language);
            }
        }

        #endregion
    }
}
