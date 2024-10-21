using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rmsdyd : MonoBehaviour
{
    public Transform player; 
    public float speed = 3f; 
    public float detectionAngle = 45f; 
    public float detectionRange = 10f; 

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, player.position);

        
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        
        if (distance < detectionRange && angle < detectionAngle)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

           
            transform.LookAt(player);
        }
    }
}
