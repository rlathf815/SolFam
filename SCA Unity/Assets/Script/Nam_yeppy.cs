using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class Nam_yeppy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    RaycastHit hit;
    bool canmove;
    public Transform home;
    public bool gohome;
    Transform chatrans;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        chatrans = gameObject.transform;
    }
    void Update()
    {
        if (canmove == true)
        {
            agent.destination = player.position;
        }
        if (Physics.SphereCast(transform.position, 2.5f, transform.forward, out hit, 20)&&canmove==false&&gohome==false)
        {
            if(hit.transform.gameObject.name=="FPC")
            {
                canmove = true;
            }
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.name=="FPC"&&canmove == true)
        {
            gohome = true;
            canmove = false;
            agent.destination = home.position;
            Invoke("jegamji", 5);
        }
    }
    void jegamji()
    {
        gohome = false;
        gameObject.transform.rotation = chatrans.rotation;
    }
}
