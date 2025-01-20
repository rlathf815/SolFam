using DevionGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeppy_player : MonoBehaviour
{
    public static bool catched = false;
    public static Quaternion gojung;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (FirstPersonLook.canlook == false)
        {
            player.transform.rotation = gojung;
        }
    }
}
