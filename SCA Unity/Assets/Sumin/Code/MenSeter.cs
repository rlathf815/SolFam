using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenSeter : MonoBehaviour
{
    private float speed = 1f;
    private float time = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            speed = speed * 1.3f;
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
        }
        speed = 1f;
    }
}