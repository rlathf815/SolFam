using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public float speed;
    public int point=3;
    private bool state;
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
        if (state == true && other.gameObject.corridor)
        {
            point -= 1;
        }

        if (other.gameObject.corridor && speed>1)
        {
            point -= 1;
        }
        if(other.gameObject.crm && other.gameObject.tag == "item")
        { 
            
        }
    }
}
