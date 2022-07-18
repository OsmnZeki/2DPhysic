using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    public static class Vector2Extensions
    {
        //rotate clockwise, angle with radians
        public static Vector2 Rotate(this Vector2 vect, Vector2 center, float rAngle)
        {
            var x = vect.x - center.x;
            var y = vect.y - center.y;

            Vector2 rotatedVect = vect;
            rotatedVect.x = x * Mathf.Cos(rAngle) - y * Mathf.Sin(rAngle);
            rotatedVect.y = x * Mathf.Sin(rAngle) + y * Mathf.Cos(rAngle);

            rotatedVect.x += center.x;
            rotatedVect.y += center.y;

            return rotatedVect;
        }
    }
}


