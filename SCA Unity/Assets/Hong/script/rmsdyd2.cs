using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float wallDetectionDistance = 1f;
    public float avoidanceForce = 3f;
    public LayerMask obstacleMask;
    public Light spotLight;
    public float detectionInterval = 2f; // 감지 및 조명 토글 간격

    private bool isPlayerDetected = false;

    void Start()
    {
        StartCoroutine(LightAndDetectionCoroutine());
    }

    void FixedUpdate()
    {
        if (isPlayerDetected)
        {
            MoveTowardsPlayer();
            spotLight.transform.LookAt(player);
        }
    }

    private IEnumerator LightAndDetectionCoroutine()
    {
        while (true)
        {
            ToggleLight();
            DetectPlayer();

            yield return new WaitForSeconds(detectionInterval); // 일정한 간격으로 실행
        }
    }

    private void ToggleLight()
    {
        if (!isPlayerDetected)
        {
            spotLight.enabled = !spotLight.enabled;
        }
    }

    private void DetectPlayer()
    {
        RaycastHit hit;
        Vector3 lightDirection = spotLight.transform.forward;

        if (Physics.Raycast(spotLight.transform.position, lightDirection, out hit, spotLight.range, obstacleMask))
        {
            float angle = Vector3.Angle(spotLight.transform.forward, hit.transform.position - spotLight.transform.position);
            isPlayerDetected = hit.transform == player && angle < spotLight.spotAngle / 2;
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // 벽 감지 및 회피
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, wallDetectionDistance, obstacleMask))
        {
            Vector3 avoidanceDirection = Vector3.Reflect(transform.forward, hit.normal);
            transform.position += avoidanceDirection * avoidanceForce * Time.deltaTime;
            Debug.Log("Avoiding wall");
        }
        else
        {
            // 플레이어에게 이동
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.LookAt(player);
        }
    }
}
