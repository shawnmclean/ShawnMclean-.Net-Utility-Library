using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShawnMclean.Utility.Collections
{
    public static class Search
    {

        /// <summary>
        /// Finds the index of an item in a sorted list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        //public static int BinarySearch<T>(this IList<T> list, T item, IComparer<T> comparer) where T : IComparable
        //{
        //    int left = 0;
        //    int right = list.Count - 1;

        //    //the left or the right always move closer to each other until overlapped.
        //    while (left <= right)
        //    {
        //        //calculate the midpoint between the left and right edge
        //        int mid = (left + right) / 2;
        //        int compareVal = comparer.Compare(list[mid], item);

        //        //If found, just return the index
        //        if (compareVal == 0)
        //        {
        //            return mid;
        //        }else if (compareVal > 0)
        //        {
        //            left = mid;
        //        }
        //        else
        //        {
        //            right = mid;
        //        }
        //    }
        //    return -1;
        //}

        /// <summary>
        /// Finds the index of an item in a sorted list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>index of where the item is located. -1 is returned if not found.</returns>
        //public static int BinarySearch<T>(this IList<T> list, T item) where T : IComparable
        //{
        //    return BinarySearch(list, item, Comparer<T>.Default);
        //}
    }
}
