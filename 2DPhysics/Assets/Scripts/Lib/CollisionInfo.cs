using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    public struct CollisionInfo
    {
        public float depth;
        public Vector2 normal;
        public Vector2 start;
        public Vector2 end;
        

        public void SetInfo(float depth, Vector2 normal, Vector2 start)
        {
            this.depth = depth;
            this.normal = normal;
            this.start = start;

            end = start + normal.normalized * depth;
        }

        public void ChangeDir()
        {
            normal *= -1;
            var n = start;
            start = end;
            end = n;
        }
        
    }
}


