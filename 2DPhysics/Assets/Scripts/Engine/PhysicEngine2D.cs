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
        

        public void Draw()
        {
            foreach (var rigidbody in rigidBodies)
            {
                rigidbody.Draw();
            }
            
        }
    }
}


