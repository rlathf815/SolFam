using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yun : MonoBehaviour
{
    bool catched = false;
    public GameObject player;
    private NavMeshAgent agent;
    void Start()
    {
        player = GameObject.Find("FPC");
        agent=GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    void Update()
    {
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
        if (col.name == "FPC"&&yeppy_player.catched==false)
        {
            yun_ui.openui = true;
            yeppy_player.catched = true;
            yeppy_player.gojung = player.transform.rotation;
            min_detect.dontmovescreen = true;
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
