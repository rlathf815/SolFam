using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public float speed;
    public int point=3;
    private bool state;
    public int HP = 3;
    // Start is called before the first frame update
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
            other.gameObject.tag == "corridor" && speed > 1 || 
            other.gameObject.tag == "crm" && other.gameObject.tag == "item")
        {
            point -= 1;
        }
        if(other.gameObject.tag == "crm13")
        {
            HP -=3;
        }
    }
}