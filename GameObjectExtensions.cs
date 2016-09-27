using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using System.Reflection;
using System.Linq;
using System;

namespace UberTools
{
    public static class GameObjectExtensions
    {
        public static T FindComponentInChildren<T>(this GameObject go, Predicate<T> pred) where T : Component
        {
            foreach(var t in go.GetComponentsInChildren<T>())
            {
                if(pred(t))
                {
                    return t;
                }
            }
            return null;
        }

        /// Delegate for operating on a series of game objects
        public delegate void GameObjectDelegate(GameObject go);

        /// Operate on all child game objects
        public static void ForEachChild(this GameObject go, Action<GameObject> action, bool includeSelf=true)
        {
            foreach(GameObject subGo in GetChildren(go, includeSelf))
            {
                action(subGo);
            }
        }

        /// Destroy all children of a game object
        public static void DestroyChildren(this GameObject go)
        {
            var children = new List<GameObject>();
            foreach (Transform child in go.transform)
                children.Add(child.gameObject);
            children.ForEach(child => GameObject.Destroy(child));
        }

        /// Gets all the game objects below 'go'
        public static IEnumerable<GameObject> GetChildren(this GameObject go, bool includeSelf=true)
        {
            if(includeSelf)
            {
                yield return go;
            }

            foreach (Transform child in go.transform)
            {
                var childGo = child.gameObject;
                yield return childGo;

                foreach (var subGo in GetChildren(childGo))
                {
                    yield return subGo;
                }
            }
        }

        /// Gets all the game objects above 'go'
        public static IEnumerable<GameObject> GetParents(this GameObject go, bool includeSelf=true)
        {
            if(includeSelf)
            {
                yield return go;
            }

            Transform loopTransform = go.transform.parent;
            while (loopTransform != null)
            {
                yield return loopTransform.gameObject;
                loopTransform = loopTransform.parent;
            }
        }

        /// Gets all the components of type T in GameObjects above and including 'go'
        public static IEnumerable<T> GetComponentsInParents<T>(this GameObject go, bool includeSelf = true) where T : Component
        {
            foreach (var parentGo in GetParents(go, includeSelf))
            {
                T component = parentGo.GetComponent<T>();
                if (component != null)
                {
                    yield return component;
                }
            }
        }

        /// Gets all the components of type T in GameObjects above and including 'go'
        public static T GetComponentInParents<T>(this GameObject go, bool includeSelf = true) where T : Component
        {
            foreach (var parentGo in GetParents(go, includeSelf))
            {
                T component = parentGo.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }
            return null;
        }

        /// A helper function for cloning GameObjects; 
        /// "go" - The GameObject to clone
        /// "parent" - The parent for the new object, which may be null
        /// "pos" - The position for the new object
        /// "rotation" - The quat rotation for the new object
        /// "scale" - The vector scale of the new object
        /// "space" - The 'space' in which to apply pos/scale/rotation. Defaults to 'World', but if you're parenting to another object you may want 'Local'
        static public GameObject Clone(this GameObject go,
                                       GameObject parent=null,
                                       Vector3? pos=null,
                                       Quaternion? rotation=null,
                                       Vector3? scale=null,
                                       TransformSpace space=TransformSpace.World)
        {
            var newGO = UnityEngine.Object.Instantiate(go) as GameObject;
            if (newGO != null)
            {
                newGO.name = go.name;
            }

            if(pos!=null)
            {
                newGO.transform.position = pos ?? Vector3.zero;
            }

            if (rotation != null)
            {
                newGO.transform.rotation = rotation ?? Quaternion.identity;
            }

            if (scale != null)
            {
                newGO.transform.localScale = scale ?? Vector3.one;
            }

            if (parent != null)
            {
                newGO.transform.SetParent(parent.transform, space);
            }

            return newGO;
        }

        /// Return the bounds of the renderers of the gameObject and its children
        /// Set includeParticles to false if you want to exclude particles
        static public Bounds GetBoundsInclusive(this GameObject go, bool includeParticles = true)
        {
            Bounds bounds = new Bounds();
            bool boundsSet = false;
            Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                if(r.GetType()!=typeof(ParticleSystemRenderer) || includeParticles)
                {
                    if (boundsSet)
                    {
                        bounds.Encapsulate(r.bounds);
                    }
                    else
                    {
                        bounds = r.bounds;
                        boundsSet = true;
                    }
                }
            }

            return bounds;
        }
    }
}
