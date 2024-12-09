using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public int point = 3;
    public static float speed;
    public float runSpeed;
    private bool state;

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
        if (state == true && other.gameObject.tag == "corridor" ||
            other.gameObject.tag == "corridor" && speed > 3 || runSpeed > 3||
            other.gameObject.tag == "crm" && other.gameObject.tag == "item")
        {
            point -= 1;
        }
        if (other.gameObject.tag == "crm13")
        {
            Debug.Log("¥Í¿Ω");
            Player_Item.HP -= 3;
        } 
    }
}
