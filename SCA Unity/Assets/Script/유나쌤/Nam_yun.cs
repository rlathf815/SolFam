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
        agent.destination = player.transform.position;
        if (catched == true)
        {
            agent.isStopped = true;
            yun_ui.uiopen = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.name=="FPC")
        {
            catched = true;
        }
    }
}
