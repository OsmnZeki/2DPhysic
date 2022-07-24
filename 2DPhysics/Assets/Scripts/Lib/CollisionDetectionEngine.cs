using System.Collections;
using System.Collections.Generic;
using Scripts.Lib;
using UnityEngine;
using Rigidbody2D = Scripts.Lib.Rigidbody2D;

public class CollisionDetectionEngine
{
    public static bool CollisionTest(Rigidbody2D c1, Rigidbody2D c2, out CollisionInfo collisionInfo)
    {
        collisionInfo = new CollisionInfo();
        var status = false;

        if (c1.shapeType == ShapeType.Circle)
        {
            if (c2.shapeType == ShapeType.Circle)
            {
                status = CircleCircleTest((CircleRigid) c1, (CircleRigid) c2, out collisionInfo);
            }
            else
            {
                status = CircleRectTest((RectangleRigid2D) c2, (CircleRigid) c1, out collisionInfo);
            }
        }
        else
        {
            if (c2.shapeType == ShapeType.Rectangle)
            {
                status = RectRectTest((RectangleRigid2D) c1, (RectangleRigid2D) c2, out collisionInfo);
            }
            else
            {
                status = CircleRectTest((RectangleRigid2D) c1, (CircleRigid) c2, out collisionInfo);
            }
        }

        return status;
    }

    public static bool CircleCircleTest(CircleRigid c1, CircleRigid c2, out CollisionInfo collisionInfo)
    {
        collisionInfo = new CollisionInfo();
        var from1To2 = c2.center - c1.center;
        var rSum = c1.radius + c2.radius;
        var dist = from1To2.magnitude;

        //no collision
        if (dist > rSum) return false;

        // collision detected
        // if the circles overlap and same pos
        if (dist != 0)
        {
            var from2To1 = from1To2.normalized * -1;
            var radiusC2 = from2To1 * c2.radius;
            collisionInfo.SetInfo(rSum - dist, from1To2.normalized, c2.center + radiusC2);
        }
        else //if the circles overlap and not same pos
        {
            if (c1.radius > c2.radius)
            {
                collisionInfo.SetInfo(c1.radius, new Vector2(0, 1), c2.center + new Vector2(0, c1.radius));
            }
            else
            {
                collisionInfo.SetInfo(c2.radius, new Vector2(0, 1), c2.center + new Vector2(0, c2.radius));
            }
        }

        return true;
    }


    public static bool CircleRectTest(RectangleRigid2D r1, CircleRigid c2, out CollisionInfo collisionInfo)
    {
        collisionInfo = new CollisionInfo();

        float bestDistance = -float.MaxValue;
        bool isInside = true;
        int nearestEdge = -1;

        //Step A : compute the nearest edge
        for (int i = 0; i < r1.vertices.Length; i++)
        {
            Vector2 toCenter = c2.center - r1.vertices[i];
            var projection = Vector2.Dot(toCenter, r1.faceNormals[i]);

            if (projection > 0)
            {
                bestDistance = projection;
                nearestEdge = i;
                isInside = false;
                break;
            }

            if (projection > bestDistance)
            {
                bestDistance = projection;
                nearestEdge = i;
            }
        }

        DrawDebug.DrawCircle(r1.vertices[nearestEdge], .1f, Color.magenta);

        if (!isInside)
        {
            Vector2 verticesToCirc = c2.center - r1.vertices[nearestEdge];
            Vector2 edgeVect = r1.vertices[(nearestEdge + 1) % 4] - r1.vertices[nearestEdge];
            var dot = Vector2.Dot(verticesToCirc, edgeVect);

            var centerDist = verticesToCirc.magnitude;
            if (bestDistance > c2.radius)
            {
                Debug.Log("not collision");
                return false;
            }

            if (dot < 0) // Step B1 : if ceter is in Region R1
            {
                Debug.Log("Center is region R1");
                var normal = verticesToCirc.normalized;
                collisionInfo.SetInfo(c2.radius - centerDist, normal, c2.center - normal * c2.radius);
                return true;
            }

            verticesToCirc = c2.center - r1.vertices[(nearestEdge + 1) % 4];
            edgeVect *= -1;
            centerDist = verticesToCirc.magnitude;
            if (bestDistance > c2.radius)
            {
                Debug.Log("not collision");
                return false;
            }

            dot = Vector2.Dot(verticesToCirc, edgeVect);
            if (dot < 0) // Step B2 : if center is in Region R2
            {
                Debug.Log("Center is region R2");
                var normal = verticesToCirc.normalized;
                collisionInfo.SetInfo(c2.radius - centerDist, normal, c2.center - normal * c2.radius);
                return true;
            }

            if (bestDistance < c2.radius) // Step B3 : if center is in Region R3
            {
                Debug.Log("Center is region R3");
                Vector2 radiusVect = r1.faceNormals[nearestEdge] * c2.radius;
                collisionInfo.SetInfo(c2.radius - bestDistance, r1.faceNormals[nearestEdge],
                    c2.center - radiusVect);
                return true;
            }
        }
        else
        {
            Debug.Log("inside");
            Vector2 radiusVect = r1.faceNormals[nearestEdge] * c2.radius;
            collisionInfo.SetInfo(c2.radius - bestDistance, r1.faceNormals[nearestEdge], c2.center - radiusVect);
            return true;
        }

        return false;
    }

    public static bool RectRectTest(RectangleRigid2D r1, RectangleRigid2D r2, out CollisionInfo collisionInfo)
    {
        collisionInfo = new CollisionInfo();

        var status1 = false;
        var status2 = false;

        status1 = SupportPointMath.FindAxisLeastPenetration(r1, r2, out var collisionR1Info);
        if (status1)
        {
            status2 = SupportPointMath.FindAxisLeastPenetration(r2, r1, out var collisionR2Info);
            if (status2)
            {
                if (collisionR1Info.depth < collisionR2Info.depth)
                {
                    //Vector2 depthVect = collisionR1Info.normal * collisionR1Info.depth;
                    //collisionInfo.SetInfo(collisionR1Info.depth,collisionR1Info.normal,collisionR1Info.start-depthVect);

                    collisionInfo = collisionR1Info;
                }
                else
                {
                    //collisionInfo.SetInfo(collisionR2Info.depth,collisionR2Info.normal,collisionR2Info.start);
                    collisionInfo = collisionR2Info;
                }
            }
        }

        return status1 && status2;
    }

    public static void DrawCollisionInfo(CollisionInfo collisionInfo)
    {
        Debug.DrawLine(collisionInfo.start, collisionInfo.end, Color.yellow);
    }
}