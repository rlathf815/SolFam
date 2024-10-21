using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rmsdyd : MonoBehaviour
{
    public Transform player; 
    public float speed = 3f; 
    public float detectionAngle = 45f;
    public float detectionRange = 10f;
    public float wallDetectionDistance = 1f;
    public float avoidanceForce = 3f;

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, player.position);

        
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        
        if (distance < detectionRange && angle < detectionAngle)
        {
            if (!IsPlayerVisible(directionToPlayer))
            {
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, wallDetectionDistance))
            {
                Vector3 avoidanceDirection = Vector3.Reflect(transform.forward, hit.normal);
                transform.position += avoidanceDirection * avoidanceForce * Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                transform.LookAt(player);
            }

        }
    }
    private bool IsPlayerVisible(Vector3 directionToPlayer)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            if (hit.transform != player)
            {
                return false; 
            }
        }
        return true; 
    }
}

