using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yeppy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    void Start()
    {
        player= GameObject.Find("FPC");
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }
    void Update()
    {
        agent.destination = player.transform.position;
        if (yeppy_player.seewhite == true)
        {
            yeppy_player.seewhite = false;
            yeppy_spawn.spawned = false;
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider col)
    {
        yeppy_spawn.spawned = false;
        Destroy(gameObject);
    }
}
