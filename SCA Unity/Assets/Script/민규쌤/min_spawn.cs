using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_spawn : MonoBehaviour
{
    public static bool spawned = false;
    int random;
    public GameObject minT;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.1f);
        if (spawned == false)
        {
            random = Random.Range(0, 3);
            if (random == 0)
            {
                Instantiate(minT, spawn1.transform);
                spawned = true;
            }
            else if (random == 1)
            {
                Instantiate(minT, spawn2.transform);
                spawned = true;
            }
            else if (random == 2)
            {
                Instantiate(minT, spawn3.transform);
                spawned = true;
            }
        }
        StartCoroutine("Spawn");
    }
}
