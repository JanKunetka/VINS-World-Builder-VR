using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoubakProductions.Core
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the amount of duplicate entries in a list.
        /// </summary>
        /// <typeparam name="T">List type.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>Amount of duplicate entries.</returns>
        public static int GetDuplicatesCount<T>(this List<T> list)
        {
            ISet<T> set = new HashSet<T>();
            ISet<T> duplicates = new HashSet<T>();
            int count = 0;
            foreach (T item in list)
            {
                if (!set.Add(item))
                {
                    count++;
                    if (duplicates.Add(item)) count++;
                }
            }

            return count;
        }

    }
}