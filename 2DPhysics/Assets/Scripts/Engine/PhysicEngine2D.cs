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

        public PhysicEngine2D()
        {
            rigidBodies = new List<Rigidbody2D>();
        }

        public void Update()
        {
            CollisionTest();
        }

        public void CollisionTest()
        {
            for (int i = 0; i < rigidBodies.Count; i++)
            {
                for (int j = i + 1; j < rigidBodies.Count; j++)
                {
                    if (!rigidBodies[i].BoundTest(rigidBodies[j])) return;

                    rigidBodies[i].DrawBoundsCircle();
                    rigidBodies[j].DrawBoundsCircle();

                    if (CollisionDetectionEngine.CollisionTest(rigidBodies[i], rigidBodies[j],
                        out CollisionInfo collisionInfo))
                    {
                        if (Vector2.Dot(collisionInfo.normal, rigidBodies[j].center - rigidBodies[i].center) < 0)
                        {
                            collisionInfo.ChangeDir();
                        }
                    }
                    
                    CollisionDetectionEngine.DrawCollisionInfo(collisionInfo);
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

        public void DrawBoundsCircle()
        {
            foreach (var rigidbody in rigidBodies)
            {
                rigidbody.DrawBoundsCircle();
            }
        }
    }
}