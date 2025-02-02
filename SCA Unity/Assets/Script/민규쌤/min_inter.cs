using DevionGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_inter : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    bool opened = false;
    public static bool delete=false;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (delete)
        {
            delete = false;
            min_spawn.spawned= false;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player")
        {
            min_ui.Eopen = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            min_ui.Eopen = false;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"&&Input.GetKey(KeyCode.F)&&opened==false)
        {
            opened = true;
            min_ui.Eopen = false;
            min_ui.Sopen = true;
            anim.SetTrigger("isHello");
            /*
            yeppy_player.catched = true;
            yeppy_player.gojung = player.transform.rotation;
            Jump.jumpStrength = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FirstPersonMovement.canRun = false;
            FirstPersonMovement.speed = 0;
            FirstPersonLook.canlook = false;
            Crouch.movementSpeed = 0;
            */
        }
    }
}
