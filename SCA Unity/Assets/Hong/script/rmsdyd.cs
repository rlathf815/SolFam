using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rmsdyd : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5.0f;
    public float moveSpeed = 3.0f;
    public float viewAngle = 45.0f;
    // Start is called before the first frame update
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 diretictionToPlayer = (player.position - transform.position).normalized;

        float angle = Vector3.Angle(transform.forward, diretictionToPlayer);

        if (distance < followDistance && angle < viewAngle)
        {
            transform.position += diretictionToPlayer * moveSpeed * Time.deltaTime;
        }
        transform.LookAt(player);
    }
}
