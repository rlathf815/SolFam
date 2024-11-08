using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noudell : MonoBehaviour
{
    private int HP = 5;
    public int Noudell_I = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            Noudell_I -= 1;
            HP += 1;
        }
    }
}
