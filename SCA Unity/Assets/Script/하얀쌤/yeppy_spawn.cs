using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeppy_spawn : MonoBehaviour
{
    public GameObject whiteT;
    public static bool spawned=false;
    void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(30);
        if (Random.Range(0, 5) == 1 &&spawned==false)
        {
            spawned = true;
            Instantiate(whiteT,gameObject.transform);
        }
        StartCoroutine("Spawn");
    }
}
