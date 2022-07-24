using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Engine2D;
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

        public float mass;
        public float invMass;
        public float friction;
        public float restitution;
        public float inertia;
        
        public Vector2 velocity;
        public Vector2 acceleration;

        public float angularVelocity;
        public float angularAcceleration;
        
        public Rigidbody2D(Vector2 center, float mass, float friction, float restitution)
        {
            this.center = center;

            this.mass = mass;
            if (mass != 0)
            {
                invMass = 1 / mass;
                acceleration = PhysicEngine2D.GravityVect;
            }
            else
            {
                invMass = 0;
                acceleration = Vector2.zero;
            }

            this.friction = friction;
            this.restitution = restitution;

            velocity = Vector2.zero;
            
            
            angularAcceleration = 0;
            angularVelocity = 0;

            this.angle = 0;
            this.boundRadius = 0;
        }

        public void UpdateMass(float delta)
        {
            mass += delta;

            if (mass <= 0)
            {
                this.invMass = 0;
                this.velocity = new Vector2(0, 0);
                this.acceleration = new Vector2(0, 0);
                this.angularVelocity = 0;
                this.angularAcceleration = 0;
            }
            else
            {
                this.invMass = 1 / mass;
                this.acceleration = PhysicEngine2D.GravityVect;
            }

            UpdateInertia();
        }

        public abstract void Move(Vector2 moveVector);

        public abstract void Rotate(float angle);

        public abstract void Draw();

        public abstract void UpdateInertia();

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

        public void DrawAttrib()
        {
            Vector2 pos = center;
            float offset = .2f;
            DrawDebug.DrawText("Mass: " +mass,new Vector3(pos.x,pos.y,0),Color.cyan);
            DrawDebug.DrawText("Friction: " +friction.ToString(),new Vector3(pos.x,pos.y+offset,0),Color.cyan);
            DrawDebug.DrawText("Restitution: "+restitution.ToString(),new Vector3(pos.x,pos.y+offset*2,0),Color.cyan);
            DrawDebug.DrawText("Inertia: "+inertia.ToString(),new Vector3(pos.x,pos.y+offset*3,0),Color.cyan);
            DrawDebug.DrawText("Velocity: "+velocity.ToString(),new Vector3(pos.x,pos.y+offset*4,0),Color.cyan);
            DrawDebug.DrawText("Acceleration: "+acceleration.ToString(),new Vector3(pos.x,pos.y+offset*5,0),Color.cyan);
            DrawDebug.DrawText("AngularVel: "+angularVelocity.ToString(),new Vector3(pos.x,pos.y+offset*6,0),Color.cyan);
            DrawDebug.DrawText("AngularAcc: "+angularAcceleration.ToString(),new Vector3(pos.x,pos.y+offset*7,0),Color.cyan);
        }
        
       
        
    }
}


