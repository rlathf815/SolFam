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
    private Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();
        agent.isStopped = false;
        anim.SetBool("isWalk",true);
    }
    void Update()
    {
        agent.destination = player.transform.position;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"&& yeppy_player.catched==false)
        {
            //여기다가 아이템 빼가는거 넣어주세요
            yeppy_spawn.spawned = false;
            Destroy(gameObject);
        }
    }
}
