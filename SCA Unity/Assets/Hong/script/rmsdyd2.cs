using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float detectionAngle = 45f; 
    public float wallDetectionDistance = 1f;
    public float avoidanceForce = 3f;
    public LayerMask obstacleMask; 
    private bool isPlayerDetected = false;
    public Light spotLight; 

    void Start()
    {
        StartCoroutine(ToggleLightCoroutine());
        StartCoroutine(ShootLightCoroutine());
    }

    void FixedUpdate()
    {
        if (isPlayerDetected)
        {
            MoveTowardsPlayer();
        }
    }

    private IEnumerator ToggleLightCoroutine()
    {
        while (true)
        {
            spotLight.enabled = !spotLight.enabled; 
            yield return new WaitForSeconds(2f); 
        }
    }

    private IEnumerator ShootLightCoroutine()
    {
        while (true)
        {
            RaycastHit hit;
            Vector3 lightDirection = spotLight.transform.forward;

            
            if (Physics.Raycast(spotLight.transform.position, lightDirection, out hit, spotLight.range, obstacleMask))
            {
                float angle = Vector3.Angle(spotLight.transform.forward, hit.transform.position - spotLight.transform.position);
                if (hit.transform == player && angle < spotLight.spotAngle / 2)
                {
                    isPlayerDetected = true; 
                }
                else
                {
                    isPlayerDetected = false; 
                }
            }
            yield return null; 
        }
    }

    private void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        if (distance < spotLight.range && isPlayerDetected) 
        {
            if (!IsPlayerVisible(directionToPlayer))
            {
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, wallDetectionDistance, obstacleMask))
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
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, spotLight.range, obstacleMask))
        {
            return hit.transform == player; 
        }
        return true;
    }
}
