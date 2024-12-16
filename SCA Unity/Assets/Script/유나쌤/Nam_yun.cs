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
        if (yun_ui.hasopenui == true)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.destination = player.transform.position;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "FPC")
        {
            yun_ui.openui = true;
        }
    }
}
