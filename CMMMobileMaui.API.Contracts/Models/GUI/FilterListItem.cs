using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    internal class FilterListItem: FilterItem
    {
        #region PROPERTY SourceDict

        private Dictionary<string, string> sourceDict;

        public Dictionary<string, string> SourceDict
        {
            get => sourceDict;
            set => SetProperty(ref sourceDict, value);
        }

        #endregion

        #region PROPERTY Source

        public List<string> Source
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY FilterValue

        public override string FilterValue
        {
            get
            {
                if (SourceDict != null)
                {
                    return SourceDict.FirstOrDefault(tt => tt.Value == Value).Key;
                }

                return string.Empty;
            }
        }

        public override string DisplayValue =>
            Value;

        public override void ClearValue()
        {
            Value = string.Empty;
        }

        #endregion

        #region METHOD InitSourceValue

        public void InitSourceValue(IList defaultList)
        {
            //TODO : implement source for filter list item

            //IAPIDictionaryResource dictionaryResource = Container.Instance.ServiceProvider.GetService<IAPIDictionaryResource>();

            //if (dictionaryResource != null)
            //{
            //    if (Name.Equals("SymbolMagazynu")
            //        || Name.Equals("MagazynDocelowy"))
            //    {
            //        SourceDict = dictionaryResource.MagazynList.
            //            ToDictionary(tk => tk.Symbol, tv => tv.Nazwa);

            //        Source = SourceDict.Values.ToList();

            //        if (SourceDict != null
            //            && SourceDict.Count > 0
            //            && defaultList != null && defaultList.Count == 1)
            //        {
            //            Value = SourceDict[defaultList[0].ToString()];
            //        }
            //    }
            //}
        }

        #endregion
    }
}
