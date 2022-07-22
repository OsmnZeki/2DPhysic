using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    [Serializable]
    public class Rectangle2D : Rigidbody2D
    {
        public float width;
        public float height;
        
        [NonSerialized]public Vector2[] vertices;
        [NonSerialized]public Vector2[] faceNormals;
        
        public Rectangle2D(Vector2 center, float angle,float width, float height) : base(center, angle)
        {
            shapeType = ShapeType.Rectangle;
            this.width = width;
            this.height = height;
            boundRadius = Mathf.Sqrt(width * width + height * height) / 2;

            vertices = new Vector2[4];
            faceNormals = new Vector2[4];

            vertices[0] = new Vector2(center.x - width / 2, center.y + height / 2);
            vertices[1] = new Vector2(center.x + width / 2, center.y + height / 2);
            vertices[2] = new Vector2(center.x + width / 2, center.y - height / 2);
            vertices[3] = new Vector2(center.x - width / 2, center.y - height / 2);

            faceNormals[0] = vertices[1] - vertices[2];
            faceNormals[1] = vertices[2] - vertices[3];
            faceNormals[2] = vertices[3] - vertices[0];
            faceNormals[3] = vertices[0] - vertices[1];
        }
        

        public override void Move(Vector2 moveVector)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += moveVector;
            }

            center += moveVector;
        }

        public override void Rotate(float angle)
        {
            this.angle += angle;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = vertices[i].Rotate(center, angle);
            }
            
            faceNormals[0] = (vertices[1] - vertices[2]).normalized;
            faceNormals[1] = (vertices[2] - vertices[3]).normalized;
            faceNormals[2] = (vertices[3] - vertices[0]).normalized;
            faceNormals[3] = (vertices[0] - vertices[1]).normalized;
        }

        public override void Draw()
        {
            Debug.DrawLine(vertices[0],vertices[1]);
            Debug.DrawLine(vertices[1],vertices[2]);
            Debug.DrawLine(vertices[2],vertices[3]);
            Debug.DrawLine(vertices[3],vertices[0]);
        }
    }
}


