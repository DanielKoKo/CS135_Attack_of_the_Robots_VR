using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPos : MonoBehaviour
{
    public GameObject room1;
    public Transform teleportPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Go to room 2
        if (Input.GetKeyDown("2"))
        {
            foreach(var c in room1.GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }
             Debug.Log(teleportPos.transform.position);
            //this.transform.rotation = teleportPos.transform.rotation;
            this.transform.position = new Vector3(10.2f, 1.466f, -130.6f);
            //this.transform.position = teleportPos.transform.position;
             Debug.Log("controller " + this.transform.position);
        }

        // Go to room 1
        if (Input.GetKeyDown("1"))
        {
            this.transform.position = new Vector3(-40, 1, -10);
        }

       
    }
}
