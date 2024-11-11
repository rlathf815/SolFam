using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    // Start is called before the first frame update
    public int Coffee_I=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            Coffee_I -=1;

        }
    }
}
