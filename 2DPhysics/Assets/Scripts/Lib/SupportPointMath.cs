using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    public struct SupPoint
    {
        public Vector2 point;
        public float distance;
        public Vector2 normal;
    }
    
    public class SupportPointMath
    {
        public static bool FindSupportPoint(Vector2 edgeNormal, Vector2 edgePoint, Vector2[] vertices, out SupPoint supPoint)
        {
            supPoint = new SupPoint();
            supPoint.distance = -float.MaxValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                var toVertices = vertices[i] - edgePoint;
                var projection = Vector2.Dot(toVertices, -edgeNormal);

                if (projection > 0 && projection > supPoint.distance)
                {
                    supPoint.distance = projection;
                    supPoint.point = vertices[i];
                    supPoint.normal = edgeNormal;
                }
                
            }

            if (supPoint.point == null)
            {
                return false;
            }

            return true;
        }

        public static bool FindAxisLeastPenetration(RectangleRigid2D r1, RectangleRigid2D r2,out CollisionInfo collisionInfo)
        {
            collisionInfo = new CollisionInfo();
            SupPoint supPoint = new SupPoint();
            supPoint.distance = float.MaxValue;
        
            bool hasSupport = true;

            for (int i = 0; i < r1.faceNormals.Length; i++)
            {
                var edgePoint = r1.vertices[i];

                hasSupport = SupportPointMath.FindSupportPoint(r1.faceNormals[i], edgePoint, r2.vertices, out var tempSupPoint);
                if (!hasSupport) return false;

                if (tempSupPoint.distance < supPoint.distance)
                {
                    supPoint.distance = tempSupPoint.distance;
                    supPoint.point = tempSupPoint.point;
                    supPoint.normal = tempSupPoint.normal;
                }
            }
        
            collisionInfo.SetInfo(supPoint.distance,supPoint.normal,supPoint.point);
        
            return true;
        }
    }
}


