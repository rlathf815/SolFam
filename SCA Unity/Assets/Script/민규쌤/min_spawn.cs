using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class min_spawn : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public GameObject minT;
    public static bool spawned = false;

    void Start()
    {
        StartCoroutine("spawn");
    }
    IEnumerator spawn()
    {
        yield return new WaitForSeconds(10);
        if (Random.Range(0, 3) == 0 &&spawned==false)
        {
            spawned = true;
            switch (Random.Range(0, 3))
            {
                case 0:
                    Instantiate(minT,spawn1);
                    break;
                case 1:
                    Instantiate(minT,spawn2);
                    break;
                case 2:
                    Instantiate(minT,spawn3);
                    break;
            }
        }
        StartCoroutine("spawn");
    }
}
