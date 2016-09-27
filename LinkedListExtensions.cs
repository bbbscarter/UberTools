using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UberTools
{
    static public class LinkedListExtensions 
    {
        //Removes all elements that match the predicate
        //Works the same as List.RemoveAll
        public static void RemoveAll<T>(this LinkedList<T> thisList, Predicate<T> predicate)
        {
            for(var node = thisList.First; node != null;)
            {
                var nextNode = node.Next;
                if(predicate(node.Value))
                {
                    thisList.Remove(node);
                }
                node = nextNode;
            }
        }
    }
}
