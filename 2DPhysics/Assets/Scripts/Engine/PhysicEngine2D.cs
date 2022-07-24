using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Lib;
using UnityEngine;
using Rigidbody2D = Scripts.Lib.Rigidbody2D;

namespace Scripts.Engine2D
{
    public class PhysicEngine2D
    {
        public List<Rigidbody2D> rigidBodies;

        public static Vector2 GravityVect = new Vector2(0, -10);
        public bool movement = false;

        public static int IterationCount = 15;

        public PhysicEngine2D()
        {
            rigidBodies = new List<Rigidbody2D>();
        }

        public void Update()
        {
            if (movement)
            {
                for (int i = 0; i < rigidBodies.Count; i++)
                {
                    var rigid = rigidBodies[i];

                    rigid.velocity += rigid.acceleration * Time.fixedDeltaTime;
                    rigid.Move(rigid.velocity * Time.fixedDeltaTime);

                    rigid.angularVelocity += rigid.angularAcceleration * Time.fixedDeltaTime;
                    rigid.Rotate(rigid.angularVelocity * Time.fixedDeltaTime);
                }
            }


            CollisionTest();
        }

        public void CollisionTest()
        {
            for (int k = 0; k < IterationCount; k++)
            {
                for (int i = 0; i < rigidBodies.Count; i++)
                {
                    for (int j = i + 1; j < rigidBodies.Count; j++)
                    {
                        if (!rigidBodies[i].BoundTest(rigidBodies[j])) continue;

                        rigidBodies[i].DrawBoundsCircle();
                        rigidBodies[j].DrawBoundsCircle();

                        if (CollisionDetectionEngine.CollisionTest(rigidBodies[i], rigidBodies[j],
                            out CollisionInfo collisionInfo))
                        {
                            if (Vector2.Dot(collisionInfo.normal, rigidBodies[j].center - rigidBodies[i].center) < 0)
                            {
                                collisionInfo.ChangeDir();
                            }

                            CollisionDetectionEngine.DrawCollisionInfo(collisionInfo);
                            CollisionResolverEngine.ResolveCollision(rigidBodies[i], rigidBodies[j], ref collisionInfo);
                        }
                    }
                }
            }
        }

        public void DrawMesh()
        {
            foreach (var rigidbody in rigidBodies)
            {
                rigidbody.Draw();
            }
        }

        public void DrawAttrib()
        {
        }
    }
}