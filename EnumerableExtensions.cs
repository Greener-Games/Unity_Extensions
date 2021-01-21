using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace GG.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns a list of ever X entry in an IEnumerable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="nth"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetEveryXEntry<T>(this IEnumerable<T> source, int nth)
        {
            return source.Every(nth).ToList();
        }
        
        /// <summary>
        /// Iterates though every X entry in an IEnumerable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Every<T>(this IEnumerable<T> source, int count)
        {
            int cnt = 0;
            foreach(T item in source)
            {
                cnt++;
                if (cnt == count)
                {
                    cnt = 0;
                    yield return item;
                }
            }
        }
        
        /// <summary>
        /// Moves the item matching the <paramref name="itemSelector"/> to the <paramref name="newIndex"/> in a list.
        /// </summary>
        public static void Move<T>(this List<T> list, T itemSelector, int newIndex) where T : class
        {
            Assert.IsNotNull(list, "list");
            Assert.IsNotNull(itemSelector, "itemSelector");
            Assert.IsTrue(newIndex >= 0, "New index must be greater than or equal to zero.");

            int currentIndex = list.IndexOf(itemSelector);
            Assert.IsTrue(currentIndex >= 0, "No item was found that matches the specified selector.");

            // Copy the current item
            T item = list[currentIndex];

            // Remove the item
            list.RemoveAt(currentIndex);

            // Finally add the item at the new index
            list.Insert(newIndex, item);
        }
    }
}