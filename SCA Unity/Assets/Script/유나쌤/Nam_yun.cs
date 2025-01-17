using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yun : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent =GetComponent<NavMeshAgent>();
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
        if (col.tag == "Player"&&yeppy_player.catched==false)
        {
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
