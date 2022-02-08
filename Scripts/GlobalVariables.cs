using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Global 
 {
     //public static int Health = 0;
     //public static bool Door_Open = false;
     //public static float timer = 0.0f;
    // public static float waitTime = 3.0f;

 }




public class GlobalVariables : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool Door_Open = false;

    // Counter for robots destroyed
    public static int robotCount;

    // Level 1 room completed
    public static bool roomCompleted;

    // Level 1 room start
    public static bool roomStart;

    // Robot alive count
    public static int robotsAlive;


    void Start()
    {
        Door_Open = false;
        robotCount = 0;
        roomCompleted = false;
        roomStart = false;
        robotsAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
