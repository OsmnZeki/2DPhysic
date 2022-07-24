using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    public static class DrawDebug
    {
        public static void DrawCircle(Vector2 center, float radius, Color color)
        {
            int nStep = 20;
            float perAngle = (float)360/nStep;
            
            var startPoint =new Vector2(center.x, center.y - radius);
            
            var oldPos = startPoint;
            
            for (int i = 0; i < nStep; i++)
            {
                var rotated = Quaternion.AngleAxis(i * perAngle, Vector3.forward);

                var currentPos = (Vector3)center + rotated * Vector2.down * radius;
                Debug.DrawLine(oldPos,currentPos,color);
                oldPos = currentPos;
            }
            
            Debug.DrawLine(oldPos,startPoint,color);
        }
        
    }
}


