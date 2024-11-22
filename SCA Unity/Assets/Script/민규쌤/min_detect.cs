using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_detect : MonoBehaviour
{
    bool detect = false;
    private Animator ani;
    void Start()
    {
        ani=GetComponent<Animator>();
    }
    void Update()
    {
        if (detect == true)
        {
            FirstPersonMovement.speed = 0f;
            yeppy_player.catched = true;
            ani.setBool("isHello",true);
            StartCoroutine(WaitAni);
        }
    }
    IEnumerator WaitAni()
    {
        yield return new WaitForSeconds(4.067f);
        ani.setBool("isHello", false);
    }
    void OnTriggerEnter(Collider col)
    {
        detect = true;
    }
}
