using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minInter : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ
    public float moveRadius = 10f; // ���� �̵� ����
    public float waitTime = 2f; // ���� �̵� �� ��� �ð�
    public UnityEngine.AI.NavMeshAgent agent; // NavMesh ������Ʈ

    [HideInInspector] public bool isTalking = false; // VendorTrigger���� �����
    private Vector3 startPosition; // �ʱ� ��ġ
    public Animator animator;

    public AudioSource hi;
    private bool hasPlayedSound = false; // 오디오 중복 실행 방지용

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        startPosition = transform.position;
        StartCoroutine(MoveRandomly());
    }

    void Update()
    {

        if (animator.GetBool("isTalking"))
        {
            //Debug.Log("isTalking on");
            agent.isStopped = true; // �ŷ� ���̸� �̵� ����
            LookAtPlayer();
            if (!hasPlayedSound)
            {
                hi.Play();
                hasPlayedSound = true;
            }
        }
        else
        {
            //Debug.Log("isTalking off");
            agent.isStopped = false; // �ŷ� ���� �ƴ� �� �̵� ����
            hasPlayedSound = false;
        }

    }
    void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // ������ ���Ʒ��� �������� �ʵ��� y���� 0���� ����
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
    IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (!isTalking) //  �ŷ� ���� �ƴ� ���� �̵�
            {
                Vector3 randomDestination = GetRandomPoint(startPosition, moveRadius);
                agent.SetDestination(randomDestination);

                while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            randomPos.y = center.y;

            if (UnityEngine.AI.NavMesh.SamplePosition(randomPos, out UnityEngine.AI.NavMeshHit hit, 2f, UnityEngine.AI.NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }
}
