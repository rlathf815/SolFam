using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_text : MonoBehaviour
{
    private bool isClose;
    public GameObject text;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = true;
            text.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isClose = false;
            text.SetActive(false);
        }
    }
}
