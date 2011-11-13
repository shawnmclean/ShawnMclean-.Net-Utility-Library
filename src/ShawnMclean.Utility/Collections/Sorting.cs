using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShawnMclean.Utility.Collections
{
    public static class Sorting
    {

        public static IList<T> InsertionSort<T>(this IList<T> list) where T : IComparable
        {
            for (int i = 1; i < list.Count; i++)
            {
                int j = i;
                while (j > 0 && list[j].CompareTo(list[j - 1]) < 0)
                {
                    T temp = list[j];
                    list[j] = list[j - 1];
                    list[j - 1] = temp;
                    j--;
                }
            }
            return list;
        }


        public static bool IsSorted<T>(this IList<T> list) where T : IComparable
        {
            for (int i = 0; i < list.Count-1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0)
                    return false;
            }
            return true;
        }

    }
}
