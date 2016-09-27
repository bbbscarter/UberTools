using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UberTools
{
    //Singleton that you manually place in a scene
    //Use this by deriving from it e.g. class A : ManualSingleton<A> {}
    [AddComponentMenu("")]
    public class ManualSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool DestroyOnSceneChange = false;

        static private T _Instance;


        static public T Instance
        {
            get { return _Instance;}
            private set {_Instance = value;}
        }

        static public bool Exists()
        {
            return Instance != null;
        }

        /// In general, avoid overriding Awake, as it's called even when the Singleton is going to be deleted
        /// Prefer OnSingletonCreate, which is called only when the Singleton is created
        public virtual void Awake()
        {
            //If an instance of this already exists, delete this one.
            if (Instance != null)
            {
                Object.Destroy(gameObject);
            }
            else
            {
                //Otherwise, tell this singleton to persist across scene changes, and call OnSingletonCreate
                Instance = this as T;
                if (!DestroyOnSceneChange)
                {
                    Object.DontDestroyOnLoad(gameObject);
                }
                OnSingletonCreate();
            }
        }

        virtual public void OnSingletonCreate() { }
    }
}
