using UnityEngine;
using System;

namespace UberTools
{
    [Flags]
    public enum TransformSpace
    {
        World=0,
        PosLocal=1<<1,
        RotLocal=1<<2,
        ScaleLocal=1<<3,
        PosRotLocal = PosLocal | RotLocal,
        Local = PosLocal | RotLocal | ScaleLocal
    }


    static public class TransformExtentions
    {
        //Turn the transform towards point, without exceeding maxStep
        static public bool TurnTowardsPoint(this Transform transform, Vector3 point, float maxStep)
        {
            float step = maxStep;
            const float minDistanceFromPoint = 0.00001f;
            var vecToTarget = point - transform.position;

            vecToTarget.y = 0;
            if (vecToTarget.magnitude > minDistanceFromPoint)
            {
                vecToTarget.Normalize();
                var targetRotation = Quaternion.LookRotation(vecToTarget);
                var newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step); 
                transform.rotation = newRotation;
                var angleDiff = Quaternion.Angle(targetRotation, newRotation);
                if(angleDiff<0.1f)
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        //Changes a transforms parent, optionally preserving its local rotation and position
        static public void SetParent(this Transform transform, Transform newParent, TransformSpace transformType=TransformSpace.World)
        {
            var oldPos = transform.localPosition;
            var oldRot = transform.localRotation;
            var oldScale = transform.localScale;

            transform.SetParent(newParent);

            if((transformType & TransformSpace.PosLocal)!=0)
            {
                transform.localPosition = oldPos;
            }

            if ((transformType & TransformSpace.RotLocal) != 0)
            {
                transform.localRotation = oldRot;
            }

            if ((transformType & TransformSpace.ScaleLocal) != 0)
            {
                transform.localScale = oldScale;
            }
        }
    }
    
}
