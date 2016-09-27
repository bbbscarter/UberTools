using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UberTools
{
    public static class EnumerableExtensions
    {
        // Comparison delegate for various functions
        public delegate T Comparer<T>(T obj1, T obj2);
        // Filter predicate for various functions
        public delegate bool Predicate<T>(T obj);


        // Applies 'action' to all items in 'items'
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        // Returns a random element from 'items'
        public static T RandomChoice<T>(this IEnumerable<T> items)
        {
            return items.ToList().RandomChoice();
        }


        /// Finds the 'best' match in the IEnumerable.
        /// Uses 'filter' to filter out unwanted results, and 'comparer' to find the best
        /// If anything is found, returns true
        /// Works with value or reference types
        static public bool GetBest<T>(this IEnumerable<T> items, Predicate<T> filter, Comparer<T> comparer, ref T bestItem)
        {
            bool foundOne = false;
            foreach (var item in items)
            {
                if (filter(item))
                {
                    if (!foundOne)
                    {
                        bestItem = item;
                        foundOne = true;
                    }
                    else
                    {
                        bestItem = comparer(bestItem, item);
                    }
                }
            }
            return foundOne;
        }

        /// Finds the 'best' match in the IEnumerable.
        /// Uses 'filter' to filter out unwanted results, and 'comparer' to find the best
        /// If anything is found, returns that item
        /// Works only with reference types
		static public T GetBest<T>(this IEnumerable<T> items, Predicate<T> filter, Comparer<T> comparer) where T : class
        {
            T bestItem = null;
            GetBest(items, filter, comparer, ref bestItem);
            return bestItem;
        }

        /// Finds the 'best' match in the IEnumerable.
        /// Goes through all the items in 'items' and uses 'comparer' to find the best
        /// If anything is found, returns true
        /// Works with value or reference types
        static public bool GetBest<T>(this IEnumerable<T> items, Comparer<T> comparer, ref T bestItem)
        {
            bool foundOne = false;
            foreach (var item in items)
            {
                if (!foundOne)
                {
                    bestItem = item;
                    foundOne = true;
                }
                else
                {
                    bestItem = comparer(bestItem, item);
                }
            }
            return foundOne;
        }

        /// Finds the 'best' match in the IEnumerable.
        /// Goes through all the items in 'items' and uses 'comparer' to find the best
        /// If anything is found, returns that item
        /// Works with  reference types
        static public T GetBest<T>(this IEnumerable<T> items, Comparer<T> comparer) where T : class
        {
            T bestItem = null;
            GetBest(items, comparer, ref bestItem);
            return bestItem;
        }
                                                                                              
        // Finds the first item in 'items' that passes 'filter'                                                                                    
        // Returns 'true' if it finds anything                                                                                     
        // Works with value or reference types
		static public bool GetFirst<T>(this IEnumerable<T> items, Predicate<T> filter, ref T foundItem)
        {

            foreach (var item in items)
            {
                if (filter(item))
                {
                    foundItem = item;
                    return true;
                }
            }
            return false;
        }

        // Finds the first item in 'items' that passes 'filter'                                                                                    
        // Returns the item found, or null
        // Works with only reference types
		static public T GetFirst<T>(this IEnumerable<T> items, Predicate<T> filter) where T : class
        {
            T foundItem = null;
            GetFirst(items, filter, ref foundItem);
            return foundItem;
        }

        // Generator that returns all items in 'items' matching 'filter'
        // Works with value or reference types
		static public IEnumerable<T> GetMatches<T>(this IEnumerable<T> items, Predicate<T> filter)
        {
            foreach (var item in items)
            {
                if (filter(item))
                {
                    yield return item;
                }
            }
        }

    }
}
