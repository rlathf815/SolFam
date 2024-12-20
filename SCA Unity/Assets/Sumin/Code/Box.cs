using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms;

public class Box : MonoBehaviour
{
    public int Coffee_I;
    public int MenSeter_I;
    public int Coffee_b;
    public int MenSeter_b;

    public GameObject text;
    private bool isClose;
    private bool state;

    public int point;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (point == 0)
        {
            Coffee_b = Coffee_I;
            MenSeter_b = MenSeter_I;
            state = true;
        }

        if (isClose && Input.GetKeyDown(KeyCode.E)) // E Å° ´©¸§
        {
            if (state == true && (Coffee_b>=1 || MenSeter_b>=1))
            { 
                state = false;
                Debug.Log("on " + state);
                Coffee_I += Coffee_b;
                MenSeter_I += MenSeter_b;
            }
            /*else
            {
                state = true;
                Debug.Log("off " + state);
            }*/
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Player")
        {
            isClose = true;
            text.SetActive(true);
        }
    }
}
