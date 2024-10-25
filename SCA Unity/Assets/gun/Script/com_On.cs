using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class com_On : MonoBehaviour
{


    private bool state;
    private bool on;
    public GameObject Target;
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //E Å° ´©¸§
        {
            if(state == true)
            {
                Target.SetActive(false); //»ç¶óÁü
                state = false;
            }
            else
            {
                Target.SetActive(true); //»ý±è
                state = true;
            }
        }  
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            on = true;
        }
    }
}
