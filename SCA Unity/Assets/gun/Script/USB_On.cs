using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms;

public class USB_On : MonoBehaviour
{
    private bool state;
    private bool isClose;
    private bool on;
    public GameObject Target;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) // E Ű ����
        {
            if (state == true)
            {
                Target.SetActive(false); // �����
                state = false;
                Debug.Log("on " + state);
            }
            else
            {
                Target.SetActive(true); // ����
                state = true;
                Debug.Log("off " + state);
            }
        }
    }
}
