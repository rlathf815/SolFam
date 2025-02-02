using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yun : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;
    public static bool punch=false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent =GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.isStopped = false;
        anim.SetBool("isWalk",true);
    }

    void Update()
    {
        if (punch)
        {
            punch = false;
            anim.SetTrigger("Punch");
        }
        if (yun_ui.hasopenui == true&&yun_ui.openui==true)
        {
            agent.isStopped = true;
        }
        else if(yun_ui.openui == false&&yun_ui.hasopenui == true)
        {
            yun_ui.hasopenui = false;
            Destroy(gameObject);
        }
        else
        {
            agent.destination = player.transform.position;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"&&yeppy_player.catched==false)
        {
            anim.SetBool("isWalk", false);
            yun_ui.openui = true;
            yeppy_player.catched = true;
            yeppy_player.gojung = player.transform.rotation;
            FirstPersonLook.canlook = false;
            Jump.jumpStrength = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            yeppy_player.catched = true;
            FirstPersonMovement.canRun = false;
            FirstPersonMovement.speed = 0f;
            Crouch.movementSpeed = 0f;
        }
    }
}
