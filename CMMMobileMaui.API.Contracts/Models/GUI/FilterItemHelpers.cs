using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    public static class FilterItemHelpers
    {
        #region METHOD FromAPIFilterObject

        public static List<FilterItem> FromAPIFilterObject<T>(out T t)
        {
            t = Activator.CreateInstance<T>();
            return FromAPIFilterObject(t);
        }

        #endregion

        #region METHOD FromAPIFilterObject

        public static List<FilterItem> FromAPIFilterObject<T>(T t)
        {
            List<FilterItem> list = new List<FilterItem>();

            PerformPropertiesAnalyze(t, list, t.GetType().GetProperties());

            return list;
        }

        #endregion

        #region METHOD FromUsedFilter

        public static T FromUsedFilter<T>(List<FilterItem> filterItems, SortItem sortItem)
        {
            T t = Activator.CreateInstance<T>();

            var filterProperties = typeof(T).GetProperties();

            foreach (var filterItem in filterItems.Where(tt => tt.IsInUse))
            {
                var property = filterProperties.FirstOrDefault(tt => tt.Name == filterItem.Name);

                if (property != null)
                {
                    if (IsPropertyGenericList(property))
                    {
                        List<string> list = new List<string>();
                        list.Add(filterItem.FilterValue);
                        property.SetValue(t, list);
                    }
                    else
                    {
                        if (Nullable.GetUnderlyingType(property.PropertyType) == null)
                        {
                            property.SetValue(t, Convert.ChangeType(filterItem.FilterValue, property.PropertyType));
                        }
                        else
                        {
                            var nullableType = Nullable.GetUnderlyingType(property.PropertyType);
                            property.SetValue(t, Convert.ChangeType(filterItem.FilterValue, nullableType));
                        }
                    }
                }
            }

            SetSortItemForFilter(t, sortItem);

            return t;
        }

        #endregion

        #region METHOD GetFilterItemForPropertyType

        private static FilterItem GetFilterItemForPropertyType(PropertyInfo property)
        {
            if (Nullable.GetUnderlyingType(property.PropertyType) == null)
            {
                if (property.Name.ToLower().Contains("symbolmagazynu"))
                {
                    return new FilterListItem();
                }
                else
                {
                    if (property.PropertyType == typeof(string))
                    {
                        return new FilterStringItem();
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        return new FilterDateItem();
                    }
                    else if (property.PropertyType == typeof(bool))
                    {
                        return new FilterBoolItem();
                    }
                }
            }
            else
            {
                var nullableType = Nullable.GetUnderlyingType(property.PropertyType);

                if (nullableType == typeof(bool))
                {
                    return new FilterBoolNullItem();
                }
                else if (nullableType == typeof(DateTime))
                {
                    return new FilterDateItem();
                }
            }

            return new FilterStringItem();
        }

        #endregion

        #region METHOD GetFilterListItem
        private static FilterListItem GetFilterListItem<T>(T t, PropertyInfo filterProperty)
        {
            var filterItem = new FilterListItem();
            filterItem.Name = filterProperty.Name;
            filterItem.DataType = filterProperty.PropertyType;

            IList oTheList = filterProperty.GetValue(t, null) as IList;
            filterItem.InitSourceValue(oTheList);
            return filterItem;
        }

        #endregion

        #region METHOD IsPropertyGenericList

        private static bool IsPropertyGenericList(PropertyInfo filterProperty)
        {
            return filterProperty.PropertyType.IsGenericType
                                && filterProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);
        }

        #endregion

        #region METHOD IsPropertyIgnoredInFilters

        private static bool IsPropertyIgnoredInFilters(PropertyInfo filterProperty) =>
            filterProperty.GetCustomAttribute<FilterIgnoreAttribute>() != null;

        #endregion

        #region METHOD PerformPropertiesAnalyze

        private static void PerformPropertiesAnalyze<T>(T t, List<FilterItem> list, PropertyInfo[] filterProperties)
        {
            foreach (var filterProperty in filterProperties)
            {
                if (IsPropertyIgnoredInFilters(filterProperty))
                {
                    continue;
                }

                SetFilterItemDependOnDataType(t, list, filterProperty);
            }
        }

        #endregion

        #region METHOD SetFilterItemDependOnDataType

        private static void SetFilterItemDependOnDataType<T>(T t, List<FilterItem> list, PropertyInfo filterProperty)
        {
            if (IsPropertyGenericList(filterProperty))
            {
                list.Add(GetFilterListItem(t, filterProperty));
            }
            else
            {
                var value = filterProperty.GetValue(t);

                var filterItem = GetFilterItemForPropertyType(filterProperty);
                filterItem.Name = filterProperty.Name;
                filterItem.DataType = filterProperty.PropertyType;

                if (value != null)
                {
                    filterItem.Value = value.ToString();
                }

                list.Add(filterItem);
            }
        }

        #endregion

        #region METHOD SetSortItemForFilter

        private static void SetSortItemForFilter<T>(T t, SortItem sortItem)
        {
            if (sortItem != null)
            {
                if (t is SortableItem sortableItem)
                {
                    sortableItem.DefaultSortItem = sortItem;
                }
            }
        }

        #endregion
    }
}
