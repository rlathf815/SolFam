using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public static int point = 3;
    public bool re = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (0 >= point)
        {
            point = 3;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "crm13")
        {
            Debug.Log("����");
            Player_Item.HP -= 3;
        }

        else if (re == true && other.gameObject.tag == "corridor")
        {
            point -= 1;
            Debug.Log("1");
        }
        else if (other.gameObject.tag == "corridor" && FirstPersonMovement.speed > 3)
        {
            point -= 1;
            Debug.Log("2");
        }
        else if (other.gameObject.tag == "crm" && other.gameObject.tag == "item")
        {
            point -= 1;
        }
        else if (other.gameObject.tag == "up")
        {
            point -= 3;
        }
        else if (other.gameObject.tag == "table")
        {
            point -= 1;
            Debug.Log("Ȯ��");
        }
    }
}
