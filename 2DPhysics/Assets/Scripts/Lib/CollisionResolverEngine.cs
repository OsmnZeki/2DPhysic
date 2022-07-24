using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Scripts.Lib
{
    
    public static class CollisionResolverEngine
    {
        public static float PosCorrectionRate = .8f;

        public static void ResolveCollision(Rigidbody2D r1, Rigidbody2D r2, ref CollisionInfo collisionInfo)
        {
            if(r1.invMass == 0 && r2.invMass == 0) return;
            
            ProjectionMethod(r1,r2,ref collisionInfo);
            ImpulseMethod(r1, r2, ref collisionInfo);
        }
        
        public static void ProjectionMethod(Rigidbody2D r1, Rigidbody2D r2, ref CollisionInfo collisionInfo)
        {
            var num = collisionInfo.depth / (r1.invMass + r2.invMass) * PosCorrectionRate;
            var correctionAmount = collisionInfo.normal * num;
            
            r1.Move(correctionAmount * -r1.invMass);
            r2.Move(correctionAmount * r2.invMass);
        }

        public static void ImpulseMethod(Rigidbody2D r1, Rigidbody2D r2, ref CollisionInfo collisionInfo)
        {
            var n = collisionInfo.normal;
            var relativeVelocity = r2.velocity - r1.velocity;

            var relVelocityInNormalDir = Vector2.Dot(relativeVelocity, n);
            if (relVelocityInNormalDir > 0)
            {
                return;
            }
            
            var restituion = Mathf.Min(r1.restitution,r2.restitution);
            var friction =  Mathf.Min(r1.friction, r2.friction);
            
            var jN = -(1 + restituion) * relVelocityInNormalDir;
            jN = jN / (r1.invMass + r2.invMass);

            var impulse = jN * n;

            r1.velocity = r1.velocity - impulse * r1.invMass;
            r2.velocity = r2.velocity + impulse * r2.invMass;

            var tangent = relativeVelocity - n *relVelocityInNormalDir;
            tangent = -tangent.normalized;
            
            var jT = -(1 + restituion) *Vector2.Dot(relativeVelocity,tangent)* friction;
            jT = jT / (r1.invMass + r2.invMass);
            
            if (jT > jN) jT = jN;

            impulse = tangent * jT;
            r1.velocity = r1.velocity - impulse * r1.invMass;
            r2.velocity = r2.velocity + impulse * r2.invMass;
        }
    }
}


