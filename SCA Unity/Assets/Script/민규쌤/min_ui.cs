using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_ui : MonoBehaviour
{
    public GameObject PressE;
    public GameObject ShopUI;
    public GameObject player;
    public static bool UI_opened=false;
    void Start()
    {
        PressE = GameObject.Find("Press_E");
        ShopUI = GameObject.Find("Shop_UI");
        player = GameObject.Find("FPC");
        PressE.SetActive(false);
        ShopUI.SetActive(false);
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "FPC")
        {
            PressE.SetActive(true);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name == "FPC")
        {
            PressE.SetActive(false);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.name == "FPC")
        {
            if (Input.GetKeyDown(KeyCode.E) && UI_opened == false)
            {
                UI_opened = true;
                yeppy_player.gojung = player.transform.rotation;
                FirstPersonLook.canlook = false;
            }
        }
    }
}
