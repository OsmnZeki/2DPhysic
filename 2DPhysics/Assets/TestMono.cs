using System.Collections;
using System.Collections.Generic;
using Scripts.Engine2D;
using Scripts.Lib;
using UnityEngine;

public class TestMono : MonoBehaviour
{
    public PhysicEngine2D engine2D;

    public Rectangle2D[] rectangles;
    public Circle[] circles;
    
    // Start is called before the first frame update
    void Start()
    {
        engine2D = new PhysicEngine2D();

        foreach (var r in rectangles)
        {
            engine2D.rigidBodies.Add(r);
        }

        foreach (var c in circles)
        {
            engine2D.rigidBodies.Add(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        engine2D.Draw();
    }
}
