using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_ui : MonoBehaviour
{
    public static GameObject PressE;
    public static GameObject ShopUI;
    public GameObject player;
    public static bool UI_opened = false;
    public static bool delete = false;
    private Animator animator;
    void Start()
    {
        PressE = GameObject.Find("Press_E");
        ShopUI = GameObject.Find("Shop_UI");
        player = GameObject.Find("FPC");
        animator = GetComponent<Animator>();
        PressE.SetActive(false);
        ShopUI.SetActive(false);
    }
    void Update()
    {
        if (delete == true)
        {
            delete = false;
            min_spawn.spawned = false;
            min_spawn.GoSpawn = true;
            Destroy(gameObject);
        }
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
    void OnTriggerStay(Collider col) //E 누름 감지
    {
        if (col.name == "FPC")
        {
            if (Input.GetKey(KeyCode.E) && UI_opened == false&&yeppy_player.catched==false)
            {
                UI_opened = true;
                yeppy_player.catched = true;
                yeppy_player.gojung = player.transform.rotation;
                FirstPersonLook.canlook = false;
                Jump.jumpStrength = 0;
                FirstPersonMovement.canRun = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Crouch.movementSpeed = 0f;
                FirstPersonMovement.speed = 0f;
                animator.SetTrigger("isHello");
                PressE.SetActive(false);
                ShopUI.SetActive(true);
            }
        }
    }
}
