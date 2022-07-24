using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    public enum ShapeType
    {
        Rectangle,Circle
    }
    
    [Serializable]
    public abstract class Rigidbody2D
    {
        public Vector2 center;
        public float angle;
        [NonSerialized] public ShapeType shapeType;

        public float boundRadius;
        public Color overlapColor = Color.green;
        
        public Rigidbody2D(Vector2 center, float angle)
        {
            this.center = center;
            this.angle = angle;
            this.boundRadius = 0;
        }


        public abstract void Move(Vector2 moveVector);

        public abstract void Rotate(float angle);

        public abstract void Draw();
        

        public void DrawBoundsCircle()
        {
            int nStep = 20;
            float perAngle = (float)360/nStep;
            
            var startPoint =new Vector2(center.x, center.y - boundRadius);
            
            var oldPos = startPoint;
            
            for (int i = 0; i < nStep; i++)
            {
                var rotated = Quaternion.AngleAxis(i * perAngle, Vector3.forward);

                var currentPos = (Vector3)center + rotated * Vector2.down * boundRadius;
                Debug.DrawLine(oldPos,currentPos,overlapColor);
                oldPos = currentPos;
            }
            
            Debug.DrawLine(oldPos,startPoint,overlapColor);
        }

        public bool BoundTest(Rigidbody2D other)
        {
            var vFrom1To2 = other.center - center;
            var rSum = other.boundRadius + boundRadius;
            var dis = vFrom1To2.magnitude;

            if (rSum < dis)
            {
                return false;
            }
            
            return true;
        }
        
       
        
    }
}


