using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform targetPlayer; // 플레이어를 참조
    public float speed = 3f;
    public float detectionAngle = 45f;
    public float wallDetectionDistance = 1f;
    public float avoidanceForce = 3f;
    public LayerMask obstacleMask;
    public Light enemyLight;
    public float detectionInterval = 2f;

    private bool isPlayerDetected = false;

    void Start()
    {
        StartCoroutine(HandleLightAndDetection()); // 메서드 이름 변경
    }

    void FixedUpdate()
    {
        if (isPlayerDetected)
        {
            MoveTowardsPlayer();
            FollowLight();
        }
    }

    private IEnumerator HandleLightAndDetection() // 이름 변경
    {
        while (true)
        {
            DetectPlayer();
            yield return new WaitForSeconds(detectionInterval);
        }
    }

    private void DetectPlayer()
    {
        RaycastHit hit;
        Vector3 lightDirection = enemyLight.transform.forward;

        if (Physics.Raycast(enemyLight.transform.position, lightDirection, out hit, enemyLight.range, obstacleMask))
        {
            float angle = Vector3.Angle(enemyLight.transform.forward, hit.transform.position - enemyLight.transform.position);
            isPlayerDetected = hit.transform == targetPlayer && angle < enemyLight.spotAngle / 2 && IsPlayerInDetectionAngle();
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    private bool IsPlayerInDetectionAngle()
    {
        Vector3 directionToPlayer = (targetPlayer.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        return angleToPlayer < detectionAngle / 2;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (targetPlayer.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, wallDetectionDistance, obstacleMask))
        {
            Vector3 avoidanceDirection = Vector3.Reflect(transform.forward, hit.normal);
            transform.position += avoidanceDirection * avoidanceForce * Time.deltaTime;
            Debug.Log("Avoiding wall");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
            transform.LookAt(targetPlayer);
        }
    }

    private void FollowLight()
    {
        enemyLight.transform.position = Vector3.MoveTowards(enemyLight.transform.position, targetPlayer.position, speed * Time.deltaTime);
        enemyLight.transform.LookAt(targetPlayer);
    }

    void OnDrawGizmos()
    {
        if (enemyLight)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(enemyLight.transform.position, enemyLight.transform.position + enemyLight.transform.forward * enemyLight.range);
        }
    }
}
