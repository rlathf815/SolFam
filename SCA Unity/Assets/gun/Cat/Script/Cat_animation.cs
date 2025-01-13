using DevionGames.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat_animation : MonoBehaviour
{
    private Animation Animator;
    private bool isClose;
    // Start is called before the first frame update
    void Start()
    {
        isClose = false;
        Animator = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
