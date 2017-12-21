using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FBTS.Library.Common
{
    public static class Lists
    {
        public static int FirstIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            int index = 0;
            foreach (T item in list)
            {
                if (predicate(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static int LastIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            int index = 0;
            foreach (T item in list.Reverse())
            {
                if (predicate(item))
                {
                    return list.Count() - index - 1;
                }
                index++;
            }
            return -1;
        }
        public static string ToCharSeperatedString(this List<string> list , string splitter)
        {   
            return string.Join(splitter, list) ;
        }
        public static string ToCommaSeperatedString(this List<string> list)
        {
            return string.Join(",", list);
        }

        public static List<T> SortList<T>(this List<T> list, string columnName, SortDirection direction)
        {
            string[] columnNameSplit = columnName.Split('.');
            var property1 = typeof(T).GetProperty(columnNameSplit[0]);
            var typeProperties=property1.PropertyType;
            var property2 = typeProperties.GetProperty(columnNameSplit[1]);
            var property = typeof(T).GetProperty(columnName);
            var multiplier = direction == SortDirection.Descending ? -1 : 1;
            list.Sort((t1, t2) =>
            {
                var col1 = property2.GetValue(t1);
                var col2 = property2.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
            return list;
        }
    }
}