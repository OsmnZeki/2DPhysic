using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    [Serializable]
    public class CircleRigid : Rigidbody2D
    {
        public float radius;
        [NonSerialized]public Vector2 startPoint;
        
        public CircleRigid(Vector2 center,float radius, float angle) : base(center, angle)
        {
            shapeType = ShapeType.Circle;
            this.radius = radius;
            startPoint =new Vector2(center.x, center.y - radius);
            boundRadius = radius+.01f;
        }


        public override void Move(Vector2 moveVector)
        {
            center += moveVector;
            startPoint += moveVector;
        }

        public override void Rotate(float angle)
        {
            this.angle += angle;
            startPoint = startPoint.Rotate(center, angle);
        }

        public override void Draw()
        {
            int nStep = 20;
            float perAngle = (float)360/nStep;
            
            startPoint =new Vector2(center.x, center.y - radius);
            
            var oldPos = startPoint;
            
            for (int i = 0; i < nStep; i++)
            {
                var rotated = Quaternion.AngleAxis(i * perAngle, Vector3.forward);

                var currentPos = (Vector3)center + rotated * Vector2.down * radius;
                Debug.DrawLine(oldPos,currentPos);
                oldPos = currentPos;
            }
            
            Debug.DrawLine(oldPos,startPoint);
        }

    }
}


