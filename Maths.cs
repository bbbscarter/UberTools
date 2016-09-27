using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace UberTools
{
    [System.Serializable]
    // public class IntVector2
    // {
    //     public int x;
    //     public int y;

    //     public IntVector2(int inX, int inY)
    //     {
    //         x = inX; 
    //         y = inY; 
    //     }
         
    //     public IntVector2(float inX, float inY)
    //     {
    //         x = (int) inX; 
    //         y = (int) inY; 
    //     }

    //     public IntVector2(double inX, double inY)
    //     {
    //         x = (int) inX; 
    //         y = (int) inY; 
    //     }
    //     public IntVector2(Vector3 vec)
    //     {
    //         x = (int) vec.x; 
    //         y = (int) vec.z;
    //     }

    //     public Vector2 Vector2()
    //     {
    //         return new Vector2(x, y);
    //     }

    //     public IntVector2 Rotate90(int rotation)
    //     {
    //         if(rotation<0)
    //         {
    //             rotation = 360 - (rotation%360);
    //         }
    //         else
    //         {
    //             rotation = rotation%360;
    //         }

    //         int ninetySteps = rotation / 90;
    //         // Debug.Log(String.Format("{0} - {1}", rotation, ninetySteps));
    //         if(rotation != ninetySteps*90)
    //         {
    //             Debug.LogError(String.Format("Can't rotate by anything other than multiples of 90! {0} - {1}", rotation, ninetySteps));
    //         }

    //         switch(ninetySteps)
    //         {
    //             case 1: return new IntVector2(y, -x);
    //             case 2: return new IntVector2(-x, -y);
    //             case 3: return new IntVector2(-y, x);
    //             default: return new IntVector2(x, y);
    //         }
    //     }
    //     public static IntVector2 operator+(IntVector2 a, IntVector2 b)
    //     {
    //         return new IntVector2(a.x + b.x, a.y + b.y);
    //     }

    //     public override bool Equals(object o)
    //     {
    //         return o is IntVector2 && this == (IntVector2) o;
    //     }

    //     public override int GetHashCode()
    //     {
    //         return x.GetHashCode() ^ y.GetHashCode();
    //     }

    //     public static bool operator==(IntVector2 a, IntVector2 b)
    //     {
    //         return a.x==b.x && a.y==b.y;
    //     }

    //     public static bool operator!=(IntVector2 a, IntVector2 b)
    //     {
    //         return a.x!=b.x || a.y!=b.y;
    //     }

    //     override public string ToString()
    //     {
    //         return String.Format("({0}, {1})", x, y);
    //     }
    // }


    public static class Maths
    {
        /// Returns the bounding radius of all colliders in that hierarchy 
        public static float GetRadiusOfHierarchy(GameObject root)
        {
            var rootPos = root.transform.position;
            float totalRadius = 0.0f;
            var colliders = root.GetComponentsInChildren<Collider>();

			foreach (var c in colliders)
            {
                float boundsRadius = c.bounds.extents.magnitude;
				float distanceFromRoot = (c.transform.position - rootPos).magnitude;
				float radiusFromRoot = distanceFromRoot + boundsRadius;
                totalRadius = Mathf.Max(radiusFromRoot, boundsRadius);
            }
		
            return totalRadius;
        }

        public static float MaxComponent(this Vector3 vec)
        {
            float returnVal = vec.x;
            float comparison = Mathf.Abs(vec.x);

            float test = Mathf.Abs(vec.y);
            if(test>comparison)
            {
                comparison = test;
                returnVal = vec.y;
            }
            test = Mathf.Abs(vec.z);
            if(test>comparison)
            {
                comparison = test;
                returnVal = vec.z;
            }
            return returnVal;
        }
	
        /// Double precision clamp function since there isn't one in Mathf
        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /// Returns a random velocity between minSpeed and max_speed 
        static public Vector3 RandomDirection(float minLength, float maxLength)
        {
            var vec = RandomVector(-1, 1);
			return vec.normalized * UnityEngine.Random.Range(minLength, maxLength);
        }

        /// Returns a random vector with components between min and max
        static public Vector3 RandomVector(float min, float max)
        {
            return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
        }

        /// Lerp between the two colours
        static public Color Lerp(Color c1, Color c2, float amount)
        {
            return new Color(Mathf.Lerp(c1.r, c2.r, amount),
                             Mathf.Lerp(c1.g, c2.g, amount),
                             Mathf.Lerp(c1.b, c2.b, amount),
                             Mathf.Lerp(c1.a, c2.a, amount));
        }

        /// Do a float range
        [System.Serializable]
        public class FloatRange
        {
            public float MinVal; 
            public float MaxVal;
            public FloatRange(float min, float max)
            {
                MinVal = min;
                MaxVal = max;
            }

            public float Random()
            {
                return UnityEngine.Random.Range(MinVal, MaxVal);
            }
        }
        
        static public float GetAngleSignBetween(Vector3 dir1, Vector3 dir2)
        {
            return GetAngleSignBetween(dir1, dir2, Vector3.up);
        }

        static public float GetAngleSignBetween(Vector3 dir1, Vector3 dir2, Vector3 up)
        {
            Vector3 right = Vector3.Cross(up, dir1);
            if(Vector3.Dot(right, dir2)<0.0f)
            {
                return -1.0f;
            }
            return 1.0f;
        }	

        static public float SignedAngle(Vector3 dir1, Vector3 dir2)
        {
            return SignedAngle(dir1, dir2, Vector3.up);
        }
        
        static public float SignedAngle(Vector3 dir1, Vector3 dir2, Vector3 up)
        {
            return Vector3.Angle(dir1, dir2)*GetAngleSignBetween(dir1, dir2, up);
        }

        static public Vector3 Sign(Vector3 vec)
        {
            return new Vector3(Mathf.Sign(vec.x), Mathf.Sign(vec.y), Mathf.Sign(vec.z));
        }

        static public GameObject Nearest(GameObject go1, GameObject go2, Vector3 pos)
        {
            var dist1 = (go1.transform.position-pos).sqrMagnitude;
            var dist2 = (go2.transform.position-pos).sqrMagnitude;
            if(dist1>dist2)
            {
                return go2;
            }
            return go1;
        }

        static public T Nearest<T>(T go1, T go2, Vector3 pos) where T:MonoBehaviour
        {
            var dist1 = (go1.transform.position-pos).sqrMagnitude;
            var dist2 = (go2.transform.position-pos).sqrMagnitude;
            if(dist1>dist2)
            {
                return go2;
            }
            return go1;
        }

        public struct LineSegment
        {
            public Vector2 start;
            public Vector2 end;
            public LineSegment(Vector2 inStart, Vector2 inEnd)
            {
                start = inStart;
                end = inEnd;
            }

            public LineSegment(float sx, float sy, float ex, float ey)
            {
                start = new Vector2(sx, sy);
                end = new Vector2(ex, ey);
            }
        }


        public static bool CalcLineSegmentIntersection(LineSegment seg1, LineSegment[] segs, out Vector2 result)
        {
            foreach(var seg in segs)
            {
                if(CalcLineSegmentIntersection(seg1, seg, out result))
                {
                    return true;
                }
            }
		
            result = Vector2.zero;
            return false;
        }

        public static bool CalcLineSegmentIntersection(LineSegment seg1, LineSegment seg2, out Vector2 result)
        {
            result = Vector2.zero;
            float denom = ((seg1.end.x - seg1.start.x) * (seg2.end.y - seg2.start.y)) - ((seg1.end.y - seg1.start.y) * (seg2.end.x - seg2.start.x));

            //  AB & CD are parallel 
            if (denom == 0)
            {
                return false;
            }

            float numer = ((seg1.start.y - seg2.start.y) * (seg2.end.x - seg2.start.x)) - ((seg1.start.x - seg2.start.x) * (seg2.end.y - seg2.start.y));
            float r = numer / denom;
            float numer2 = ((seg1.start.y - seg2.start.y) * (seg1.end.x - seg1.start.x)) - ((seg1.start.x - seg2.start.x) * (seg1.end.y - seg1.start.y));
            float s = numer2 / denom;

            if ((r < 0 || r > 1) || (s < 0 || s > 1))
            {
                return false;
            }

            // Find intersection point
            result.x = seg1.start.x + (r * (seg1.end.x - seg1.start.x));
            result.y = seg1.start.y + (r * (seg1.end.y - seg1.start.y));

            return true;
        }

        static public float RoundToNearest(float val, float rounding)
        {
            if(val>=0)
            {
                return Mathf.Floor((val + rounding/2.0f)/rounding)*rounding;
            }
            else
            {
                return Mathf.Ceil((val - rounding/2.0f)/rounding)*rounding;
            }
        }
    }

    static public class Vector3Extensions
    {
        /// Scale a vector in-place by a single scalar value
        static public void Scale(this Vector3 v, float scale)
        {
            v.x *= scale;
            v.y *= scale;
            v.x *= scale;
        }
    }


}
