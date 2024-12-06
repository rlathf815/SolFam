using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeppy_player : MonoBehaviour
{
    RaycastHit hit;
    public static bool seewhite=false;
    public static bool catched = false;
    void Update()
    {
        if (Physics.SphereCast(transform.position, 2.5f, transform.forward, out hit,80))
        {
            if (hit.transform.gameObject.tag=="whiteT")
            {
                seewhite = true;
            }
        }
    }
}
