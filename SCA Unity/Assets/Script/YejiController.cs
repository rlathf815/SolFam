using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class YejiController : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;
    public static bool shoot = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.isStopped = false;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (shoot)
        {
            shoot = false;
            anim.SetTrigger("Shoot");
        }
        if (yun_ui.hasopenui == true && yun_ui.openui == true)
        {
            agent.isStopped = true;
        }
        else if (yun_ui.openui == false && yun_ui.hasopenui == true)
        {
            yun_ui.hasopenui = false;
            Destroy(gameObject);
        }
        else
        {
            agent.destination = player.transform.position;
        }
    }
    



}
