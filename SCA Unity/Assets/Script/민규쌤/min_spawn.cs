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
    int random=0;
    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        if (spawned == false)
        {
            if(Random.Range(0, 4) == 0)
            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    spawned = true;
                    Instantiate(minT, spawn1);
                }
                else if (random == 1)
                {
                    spawned = true;
                    Instantiate(minT, spawn2);
                }
                else if (random == 2)
                {
                    spawned = true;
                    Instantiate(minT, spawn3);
                }
            }
            yield return new WaitForSeconds(5);
        }
        StartCoroutine("Spawn");
    }
}
