using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_detect : MonoBehaviour
{
    private Animator ani;
    void Start()
    {
        ani=GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider col)
    {
        ani.SetTrigger("isHello");
        FirstPersonMovement.speed = 0f;
    }
}
