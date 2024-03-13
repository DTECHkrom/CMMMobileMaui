using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.Models.GUI;

namespace CMMMobileMaui.API.Contracts.Models.COMMON
{
    public static class SMethods
    {
        #region METHOD GetSortItemList

        public static List<SortItem> GetSortItemList<T>()
        {
            var properties = typeof(T).GetProperties()
                .Where(tt => tt.GetCustomAttribute(typeof(CanSortItemAttribute)) != null);

            if (properties.Any())
            {
                return properties.Select(tt => new SortItem(tt.Name, Enums.SortDirection.Down))
                    .OrderBy(tt => tt.DisplayName)
                    .ToList();
            }

            return new List<SortItem>();
        }

        #endregion
    }
}
