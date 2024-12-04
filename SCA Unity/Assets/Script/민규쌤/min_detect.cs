using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class min_detect : MonoBehaviour
{
    bool detect = false;
    bool aniplay = false;
    public GameObject Schang;
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
            FirstPersonMovement.canRun = false;
            yeppy_player.catched = true;
            if (aniplay == false)
            {
                aniplay = true;
                ani.SetBool("isHello", true);
                StartCoroutine("WaitAni");
            }
        }
    }
    IEnumerator WaitAni()
    {
        yield return new WaitForSeconds(4.067f);
        ani.SetBool("isHello",false);
    }
    void OnTriggerStay(Collider col)
    {
        if (Input.GetKey(KeyCode.E)&&detect==false)
        {
            detect = true;
            Schang.SetActive(true);
        }
    }
}
