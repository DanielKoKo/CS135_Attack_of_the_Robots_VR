using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController1 : MonoBehaviour
{
    //public GameObject player;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;

    private AudioSource audio;

    public bool roomTriggered;
    public bool roomComplete;
    private bool alienLifeFormDetected;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player triggered room 1");
        roomTriggered = true;
        if (alienLifeFormDetected == false)
        {
            audio.Play();
            alienLifeFormDetected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        roomTriggered = false;
        roomComplete = false;

        spawn1.SetActive(false);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (roomTriggered == true && roomComplete == false)
        {
            spawn1.SetActive(true);
            spawn2.SetActive(true);
            spawn3.SetActive(true);
            GlobalVariables.Door_Open = false;
        }
        else if(roomTriggered == true && roomComplete == true)
        {
            spawn1.SetActive(false);
            spawn2.SetActive(false);
            spawn3.SetActive(false);
            GlobalVariables.Door_Open = true;
        }

        //test code
        //if(Input.GetKeyDown("t"))
        //{
        //    Debug.Log("Room complete!");
        //    roomComplete = true;
        //}
    }
}
