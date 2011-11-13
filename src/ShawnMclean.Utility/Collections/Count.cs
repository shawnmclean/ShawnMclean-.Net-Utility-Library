using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShawnMclean.Utility.Collections
{
    public static class Count
    {
        /// <summary>
        /// This returns a count of the item in a sorted list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int CountSorted<T>(this List<T> list, T item) where T : IComparable
        {
            //find random index of the item
            int index = list.BinarySearch(item);

            //if item isn't found, just return -1
            if (index < 0)
                return -1;


            int leftEdge = findLeftEdge(list, 0, index, item, Comparer<T>.Default);
            int rightEdge = findRightEdge(list, index, list.Count - 1,item, Comparer<T>.Default);

            //count it by taking away the left from the right
            return rightEdge-leftEdge + 1;
        }

        /// <summary>
        /// Find the start index of an item in a sorted list using modified binary search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static int findLeftEdge<T>(IList<T> list, int start, int end, T item, Comparer<T> comparer)
        {
            int left = start;
            int right = end;

            //the left or the right always move closer to each other until overlapped.
            while (left <= right)
            {
                //calculate the midpoint between the left and right edge
                int mid = (left + right) / 2;
                int compareVal = comparer.Compare(list[mid], item);

                if (compareVal == 0)
                {
                    //before moving, check if its the start.
                    if (mid == start)
                        return mid;

                    //check to see if there is anymore to the immediate left, then move to the left
                    if(comparer.Compare(list[mid-1], item) == 0)
                        right = mid-1;
                    else
                        //return mid since there is no more to the left
                        return mid;
                }
                else
                {
                    //check to see if there is any to the immediate right, then return that index.
                    if (comparer.Compare(list[mid + 1], item) == 0)
                        return mid + 1;
                    else
                        //else, we move back to the right
                        left = mid;
                }
            }
            //no more was found, so just return the end
            return end;
        }

        /// <summary>
        /// Find the end index of an item in a sorted list using modified binary search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static int findRightEdge<T>(IList<T> list, int start, int end, T item, Comparer<T> comparer)
        {
            int left = start;
            int right = end;

            //the left or the right always move closer to each other until overlapped.
            while (left <= right)
            {
                //calculate the midpoint between the left and right edge
                int mid = (left + right) / 2;
                int compareVal = comparer.Compare(list[mid], item);

                if (compareVal == 0)
                {
                    //before moving, check if its the end.
                    if (mid == end)
                        return mid;

                    //check to see if there is anymore to the immediate right, then move to the left
                    if (comparer.Compare(list[mid + 1], item) == 0)
                        left = mid+1;
                    else
                        //return mid since there is no more to the left
                        return mid;
                }
                else
                {
                    //check to see if there is any to the immediate left, then return that index.
                    if (comparer.Compare(list[mid - 1], item) == 0)
                        return mid - 1;
                    else
                        //else, we move back to the left
                        right = mid;
                }
            }
            //no more was found, so just return the start
            return start;
        }
    }
}
