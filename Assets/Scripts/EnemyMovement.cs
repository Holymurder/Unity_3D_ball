using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    // ���� ��� ������� ������
    private bool isStopped = false;
    private float stopTimer = 0f;
    private float stopDuration = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null && !isStopped)
        {
            navMeshAgent.SetDestination(player.position);
        }

        if (isStopped)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopDuration)
            {
                ResumeMovement();
            }
        }
    }

    // ����� ��� ��������� ������ (������� ������)
    public void StopEnemy(float duration)
    {
        isStopped = true;
        stopDuration = duration;
        stopTimer = 0f;
        navMeshAgent.isStopped = true; // ��������� ��� ������
    }

    // ����� ��� ���������� ����
    private void ResumeMovement()
    {
        isStopped = false;
        navMeshAgent.isStopped = false;
    }
}
