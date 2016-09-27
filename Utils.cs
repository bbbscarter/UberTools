using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace UberTools
{
    public static class Utils
    {
        /// Clears texture to 'col'. Slowly, using the CPU. Arg.
        public static void ClearTexture(Texture2D texture, Color col)
        {
            for(int cy=0; cy<texture.height; cy++)
            {
                for(int cx=0; cx<texture.width; cx++)
                {
                    texture.SetPixel(cx, cy, col);
                }
            }
            texture.Apply();
        }

        public static bool IsWithinRangeNoY(Vector3 p1, Vector3 p2, float distance)
        {
            p1.y = p2.y;
            return (p1-p2).sqrMagnitude<distance*distance;
        }

        public static bool IsWithinRangeNoY(Transform p1, Transform p2, float distance)
        {
            return IsWithinRangeNoY(p1.position, p2.position, distance);
        }

        public static bool IsWithinRangeNoY(GameObject p1, GameObject p2, float distance)
        {
            return IsWithinRangeNoY(p1.transform.position, p2.transform.position, distance);
        }

        public static bool IsWithinRange(Vector3 p1, Vector3 p2, float distance)
        {
            return (p1-p2).sqrMagnitude<distance*distance;
        }

        public static bool IsWithinRange(Transform p1, Vector3 p2, float distance)
        {
            return IsWithinRange(p1.position, p2, distance);
        }
        
        public static bool IsWithinRange(Vector3 p1, Transform p2, float distance)
        {
            return IsWithinRange(p1, p2.position, distance);
        }
        
        public static bool IsWithinRange(Transform p1, Transform p2, float distance)
        {
            return IsWithinRange(p1.position, p2.position, distance);
        }
        
        public static bool IsWithinRange(GameObject p1, GameObject p2, float distance)
        {
            return IsWithinRange(p1.transform.position, p2.transform.position, distance);
        }
    }
}
