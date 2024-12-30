using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class min_button : MonoBehaviour
{
    public GameObject Schang;
    public GameObject Epress;
    public GameObject minT;
    public void CloseClick()
    {
        Schang.SetActive(false);
        min_detect.dontmovescreen = false;
        Yun_button.Reset();
        yeppy_player.catched = false;
        min_detect.detect = false;
        min_detect.aniplay = false;
        min_spawn.spawned = false;
        Destroy(minT);
    }
}
