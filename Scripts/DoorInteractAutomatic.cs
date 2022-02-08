using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractAutomatic : MonoBehaviour
{
     [SerializeField] private DoorAnimated door;
    private void OnTriggerEnter(Collider collider)
    {
        if ( (collider.GetComponent<OVRPlayerController>() != null) && (GlobalVariables.Door_Open))
        {
            door.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<OVRPlayerController>() != null) 
        {
            door.CloseDoor();
        }
    }

}
