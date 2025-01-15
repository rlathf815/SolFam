using DevionGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeppy_player : MonoBehaviour
{
    RaycastHit hit;
    public static bool seewhite=false;
    public static bool catched = false;
    public static Quaternion gojung;
    public Transform player;
    void Update()
    {
        if (Physics.SphereCast(transform.position, 1.5f, transform.forward, out hit,80))
        {
            if (hit.transform.gameObject.tag=="whiteT")
            {
                seewhite = true;
            }
        }
        if (FirstPersonLook.canlook == false)
        {
            player.rotation = gojung;
        }
    }
}
