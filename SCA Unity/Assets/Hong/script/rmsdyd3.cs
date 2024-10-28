using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;              
    public Transform spotlight;           
    public float detectionRadius = 10f;   
    public float chaseSpeed = 5f;         
    public float checkInterval = 2f;      

    private float nextCheckTime;
    private bool isChasing = false;       

    void Start()
    {
        StartCoroutine(ToggleSpotlight());
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }

        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - spotlight.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionRadius && spotlight.gameObject.activeSelf)
        {
            if (!IsWallBetween(spotlight.position, player.position))
            {
                isChasing = true; 
            }
        }
        else
        {
            isChasing = false; 
        }
    }

    void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
    }

    bool IsWallBetween(Vector3 from, Vector3 to)
    {
        RaycastHit hit;
        if (Physics.Raycast(from, to - from, out hit, Vector3.Distance(from, to)))
        {
            if (hit.collider.CompareTag("Wall")) 
            {
                return true;
            }
        }
        return false;
    }

    System.Collections.IEnumerator ToggleSpotlight()
    {
        while (true)
        {
            spotlight.gameObject.SetActive(true); 
            yield return new WaitForSeconds(2f); 
            spotlight.gameObject.SetActive(false); 
            yield return new WaitForSeconds(2f); 
        }
    }
}
