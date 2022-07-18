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

        public Rigidbody2D(Vector2 center, float angle)
        {
            this.center = center;
            this.angle = angle;
        }

        public abstract void Draw();

    }
}


