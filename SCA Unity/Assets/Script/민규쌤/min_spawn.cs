using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_spawn : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public GameObject minT;
    public static bool spawned = false;
    public static bool GoSpawn = false;
    int random=0;
    void Start()
    {
        spawn();
    }
    void Update()
    {
        if(GoSpawn == true)
        {
            GoSpawn = false;
            spawn();
        }
    }
    void spawn()
    {
        if (spawned == false)
        {
            if (true)
            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    spawned = true;
                    Instantiate(minT, spawn1.transform);
                }
                else if (random == 1)
                {
                    spawned = true;
                    Instantiate(minT, spawn2.transform);
                }
                else if (random == 2)
                {
                    spawned = true;
                    Instantiate(minT, spawn3.transform);
                }
            }
        }
    }
}
