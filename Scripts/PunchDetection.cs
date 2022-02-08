using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * - This script should be attached to the enemy's root GameObject.
 * - Duplicate the enemy's gameobject in the hierarchy menu
 * - Right click on duplicated enemy's root -> 3D Object -> Ragdoll...
 * - Attach enemy's limbs to the ragdoll menu wizard, specify mass (make sure it's at least 100, otherwise the enemy will fly off) and click create. 
 *   This will set the duplicated enemy with ragdoll.
 * - Drag the newly created enemy into your project assets ui at the bottom, and name it ragdoll or something. This sets it as a new prefab.
 * - Delete the ragdoll gameobject from the hierarchy.
 * - Attach the ragdoll prefab to the "Ragdoll" section of the "Punch Detection" script on the Inspector menu on the right.
 * 
 */

public class PunchDetection : MonoBehaviour
{
    public GameObject ragdoll;
    public Rigidbody robotRigid;
    public AudioSource punchSound;
    public int health;
    public bool hand_exited = false; //checks if hand has exited enemy's collision
    float vel_elapsed = 0;           //checks how long the robot has been sliding for

    void OnTriggerEnter(Collider other)
    {
        if (health > 0) //only check for collision if robot is still alive
        {
            //left hand collision check
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0 &&
                OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0)  
            {
                //Debug.Log("Left hand collision detected");

                //checks if hand velocity is fast enough for a punch
                if ((OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).x > 1.5f ||
                     OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).y > 1.5f ||
                     OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).z > 1.5f))
                {
                    //if the player punches the enemy while the punchSound audio clip is still playing, 
                    // stop the audio clip and restart it
                    if (hand_exited)
                    {
                        punchSound.Stop();
                        hand_exited = false;
                    }
                    //Debug.Log("Left hand collision detected");

                    punchSound.Play();

                    health -= 1;

                    //Debug.Log("Health = " + health);

                    if (health <= 0)
                    {
                        DeathAnimation();
                    }
                }
            }
            //right hand collision check
            else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0 &&
                     OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0)
            {
                //Debug.Log("Right hand collision detected");

                if ((OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).x > 1.5f ||
                     OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).y > 1.5f ||
                     OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).z > 1.5f))
                {
                    if (hand_exited)
                    {
                        punchSound.Stop();
                        hand_exited = false;
                    }
                    //Debug.Log("Right hand punch detected");

                    punchSound.Play();

                    health -= 1;

                    //Debug.Log("Health = " + health);

                    if (health == 0)
                    {
                        DeathAnimation();
                    }
                }
            }
            else //if hand is not a fist
            {
                if (other.gameObject.tag == "Hand" &&
                    (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).z != 0 ||
                    OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).z != 0))
                {
                    //robotRigid.freezeRotation = true;

                    if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).z != 0)
                    {
                        robotRigid.velocity = new Vector3(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).x * 3.0f, 0,
                                                          OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).z * 3.0f);
                    }
                    else if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).z != 0)
                    {
                        robotRigid.velocity = new Vector3(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).x * 3.0f, 0,
                                                          OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).z * 3.0f);
                    }
                }
            }
        }
    } 

    //checks if a user's hand has exited the enemy's collider after punching
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            hand_exited = true;
        }
    }

    //checks if robot's feet collider collides with the player or dummy. If so, ignore collision 
    void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag == "dummy" || collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, box_col);
        }
        */
    }

    void Start()
    {
        punchSound = GetComponent<AudioSource>();
        robotRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log("Robots destroyed: " + GlobalVariables.robotCount);
        /*
        if (robotRigid.freezeRotation)
        {
            vel_elapsed += Time.deltaTime;
            if (vel_elapsed >= 2)
            {
                robotRigid.freezeRotation = false;
            }
        }
        */
    }

    void DeathAnimation()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(gameObject);
        GlobalVariables.robotCount++;
        GlobalVariables.robotsAlive--;
        ragdoll.AddComponent<DestroyRagdoll>();
        Debug.Log("Robots alive: " + GlobalVariables.robotsAlive);
    }
}
