using System.Collections;
using System.Collections.Generic;
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
                for (int j = i+1; j < rigidBodies.Count; j++)
                {
                    if (rigidBodies[i].BoundTest(rigidBodies[j]))
                    {
                        rigidBodies[i].DrawBoundsCircle();
                        rigidBodies[j].DrawBoundsCircle();
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

        public void DrawBoundsCircle()
        {
            foreach (var rigidbody in rigidBodies)
            {
                rigidbody.DrawBoundsCircle();
            }
        }
    }
}


