using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 spawnPos;
    public GameObject spawnObject;
    private GameObject spawn;
    private float newSpawnDuration = 1f;
    public float timer;

    private bool robotStop;

    public int i;

    private AudioSource audio;

    public GameObject spawnEffect;

    #region Singleton

    public static Spawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        audio = GetComponent<AudioSource>();
        spawn = spawnObject;
    }

    void SpawnNewObject()
    {
        Instantiate(spawnObject, spawnPos, Quaternion.identity);
    }

    public void NewSpawnRequest()
    {
        Invoke("SpawnNewObject", newSpawnDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalVariables.robotsAlive >= 20)
        {
            robotStop = true;
        }
        else
        {
            robotStop = false;
        }
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            if(robotStop == false)
            {
                spawn.name = "Robot" + (i + 1);
                SpawnNewObject();
                i++;
                audio.Play();
                Instantiate(spawnEffect, transform.position, transform.rotation);
                GlobalVariables.robotsAlive++;
            }
            timer = 0;
        }
    }
}
