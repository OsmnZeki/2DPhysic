using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Engine2D;
using Scripts.Lib;
using UnityEngine;

public class TestMono : MonoBehaviour
{
    public PhysicEngine2D physicEngine2D;

    public float circleRadius;
    public float boxWidth;
    public float boxHeight;

    public int selectedIdx = 0;
    
    // Start is called before the first frame update
    void Start()
    {

        physicEngine2D = new PhysicEngine2D();
        
    }

    void FixedUpdate()
    {
        physicEngine2D.Update();
    }

    // Update is called once per frame
    void Update()
    {
        
        HandleInput();
        
        
        physicEngine2D.DrawMesh();
        //physicEngine2D.DrawBoundsCircle();
    }


    public void HandleInput()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CircleRigid circle = new CircleRigid(mousePos, circleRadius, 0);
            physicEngine2D.rigidBodies.Add(circle);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RectangleRigid2D rect = new RectangleRigid2D(mousePos, 0, boxWidth,boxHeight);
            physicEngine2D.rigidBodies.Add(rect);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            selectedIdx = (selectedIdx + 1) % physicEngine2D.rigidBodies.Count;
        }
        
        if(selectedIdx >= physicEngine2D.rigidBodies.Count) return;

        if (Input.GetKey(KeyCode.W))
        {
            physicEngine2D.rigidBodies[selectedIdx].Move(new Vector2(0,.01f));
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            physicEngine2D.rigidBodies[selectedIdx].Move(new Vector2(0,-.01f));
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            physicEngine2D.rigidBodies[selectedIdx].Move(new Vector2(-.01f,0));
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            physicEngine2D.rigidBodies[selectedIdx].Move(new Vector2(.01f,0));
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            physicEngine2D.rigidBodies[selectedIdx].Rotate(.1f);
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            physicEngine2D.rigidBodies[selectedIdx].Rotate(-.1f);
        }
    }
}
