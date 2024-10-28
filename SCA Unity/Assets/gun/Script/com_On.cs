using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class com_On : MonoBehaviour
{


    private bool state;
    private bool isClose;
    private bool on;
    public GameObject Target;
    void Start()
    {
        state = false;
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isClose && Input.GetKeyDown(KeyCode.E)) //E Å° ´©¸§
        {
            Debug.Log("E pressed");

            if (state == true)
            {
                Target.SetActive(false); //»ç¶óÁü
                state = false;
                Debug.Log("on " + state);
            }
            else
            {
                Target.SetActive(true); //»ý±è
                state = true;
                Debug.Log("off " + state);

            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player entered");
            isClose = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player entered");
            isClose = false;
        }
    }
}
