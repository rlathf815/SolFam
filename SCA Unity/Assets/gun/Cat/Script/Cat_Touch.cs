using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool isClose;
    public 

    //만나면 확률적으로 냥택을하고 아닐경우(플레이어에게 딜을 넣는다) 호감도작을 하여 호감스택을 쌓아야한다 호감작키[E]키 
    void Start()
    {
        isClose = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) {
            if(Random.value < 0.5f)
            {

            }
        }
    }
}
