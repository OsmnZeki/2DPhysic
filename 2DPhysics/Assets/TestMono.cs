using System.Collections;
using System.Collections.Generic;
using Scripts.Engine2D;
using Scripts.Lib;
using UnityEngine;

public class TestMono : MonoBehaviour
{

    public Transform testObj;
    public Vector2 pivot;
    public float degreeAngle;

    public PhysicEngine2D engine2D;
    
    // Start is called before the first frame update
    void Start()
    {
        engine2D = new PhysicEngine2D();
        engine2D.rigidBodies.Add(new Rectangle2D(Vector2.zero, 0,1,1));
    }

    // Update is called once per frame
    void Update()
    {
        engine2D.Draw();
    }
}
