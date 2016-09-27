using System;
using System.Collections.Generic;
using System.Linq;

namespace UberTools
{
    static public class DictionaryExtensions 
    {
        //Returns the element at 'key', or null.
        //Allows you to do 'var t = dict.Get(key); if(t==null)... etc
        public static TVal Get<TKey, TVal>(this Dictionary<TKey, TVal> thisDict, TKey key) where TVal : class
        {
            TVal outVal;
            if(thisDict.TryGetValue(key, out outVal))
            {
                return outVal;
            }
            
            return default (TVal);
        }
                                                                                                    
    }
}
