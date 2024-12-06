using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item : MonoBehaviour
{
    public static int HP = 3;
    public int Coffee_I = 0;
    public int MenSeter_I = 0;
    public int Noudell_I = 0;
    private float time = 5;
    private bool state;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)&&MenSeter_I>=1)
        {
            MenSeter_I -= 1;
            FirstPersonMovement.speed = FirstPersonMovement.speed * 1.3f;
            FirstPersonMovement.runSpeed = FirstPersonMovement.runSpeed * 1.3f;
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            FirstPersonMovement.speed = 3f;
            FirstPersonMovement.runSpeed = 5f;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)&&Noudell_I>=1)
        {
            Noudell_I -= 1;
            HP += 1;
        }

        if (Input.GetKeyUp(KeyCode.Keypad3) && Coffee_I >= 1) // ¹Ì¿Ï
        {
            Coffee_I -= 1;
        }
    }
}
