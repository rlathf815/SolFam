using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class min_detect : MonoBehaviour
{
    public static bool detect = false;
    public static bool aniplay = false;
    public static bool dontmovescreen = false;
    public GameObject Schang;
    public GameObject Epress;
    public Transform player;
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
            Crouch.movementSpeed = 0f;
            FirstPersonMovement.canRun = false;
            yeppy_player.catched = true;
            if (aniplay == false)
            {
                yeppy_player.gojung = player.rotation;
                Cursor.visible = true;
                Jump.jumpStrength = 0;
                Cursor.lockState= CursorLockMode.None;
                dontmovescreen = true;
                Epress.SetActive(false);
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
        if (Input.GetKey(KeyCode.E)&&detect==false&&yeppy_player.catched == false)
        {
            detect = true;
            Schang.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        Epress.SetActive(true);
    }
    void OnTriggerExit(Collider col)
    {
        Epress.SetActive(false);
    }
}
