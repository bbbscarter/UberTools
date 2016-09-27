using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UberTools
{
    static public class ListExtensions 
    {
        public static void Resize<T>(this List<T> list, int size, T defaultValue)
        {
            int count = list.Count;
            if(size < count)
            {
                list.RemoveRange(size, count - size);
            }
            else if(size > count)
            {
                //Prevent multiple capacity changes
                if(size > list.Capacity)
                {
                    list.Capacity = size;
                }
                list.AddRange(Enumerable.Repeat(defaultValue, size - count));
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int pos = UnityEngine.Random.Range(i, list.Count);

                T temp = list[i];
                list[i] = list[pos];
                list[pos] = temp;
            }
        }

        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> match)
        {
            var itemCount = list.Count;
            for (int i = 0; i < itemCount - 1; ++i)
            {
                if (match(list[i]) == true)
                {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public static List<T> RemoveDuplicates<T>(this List<T> list)
        {
            var newList = new List<T>();
            foreach(var item in list)
            {
                if(!newList.Contains(item))
                {
                    newList.Add(item);
                }
            }
            return newList;
        }
    }
}
