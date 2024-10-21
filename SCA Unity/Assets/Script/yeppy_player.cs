using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeppy_player : MonoBehaviour
{
    RaycastHit hit;
    public static bool seewhite=false;
    void Update()
    {
        if (Physics.SphereCast(transform.position, 2, transform.forward, out hit,80))
        {
            if (hit.transform.gameObject.name == "white")
            {
                seewhite = true;
            }
        }
    }
}
