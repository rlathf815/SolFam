using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Yun_Spawn : MonoBehaviour
{
    public static bool spawn = false;
    public GameObject yunaT;
    void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(25);
        if (Random.Range(0, 3) == 1 && spawn == false)
        {
            spawn = true;
            Instantiate(yunaT, gameObject.transform);
        }
        StartCoroutine("Spawn");
    }
}
