using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] private DoorAnimated door, door2;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            door.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            door.CloseDoor();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            door2.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            door2.CloseDoor();
        }

    }
}
