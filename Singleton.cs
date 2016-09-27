using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace UberTools
{
    //Lazy on-demand creation singleton. Rarely useful in Unity, generally prefer ManualSingleton
    //Use this by deriving from it e.g. class A : Singleton<A> {}
    [AddComponentMenu("")]
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static private T _Instance;

        static public T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    System.Type t = typeof(T);
                    GameObject go = new GameObject(t.Name);
                    Object.DontDestroyOnLoad(go);
                    _Instance = go.AddComponent<T>();
                    var c = Instance as Singleton<T>;
                    c.OnSingletonCreate();
                }
                return _Instance;
            }
            private set { _Instance = value; }
        }

        static public bool Exists()
        {
            return _Instance != null;
        }

        virtual public void OnSingletonCreate() { }
    }
}
