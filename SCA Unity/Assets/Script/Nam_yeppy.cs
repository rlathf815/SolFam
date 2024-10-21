using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yeppy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.isStopped = false;
        agent.destination = player.position;
        if (yeppy_player.seewhite == true)
        {
            yeppy_player.seewhite = false;
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider col)
    {
        Destroy(gameObject);
    }
}
