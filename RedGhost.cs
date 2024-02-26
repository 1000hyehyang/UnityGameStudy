using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedGhost : MonoBehaviour
{
    public Transform target; // ĳ������ Transform�� ����Ű�� ����
    public float followDistance = 5f; // ���Ͱ� ĳ���͸� ���󰡴� �Ÿ�
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ĳ���Ϳ� ���� ������ �Ÿ��� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ���� ĳ���Ϳ� ���� ������ �Ÿ��� followDistance���� �۰ų� ���ٸ�
        if (distanceToTarget <= followDistance)
        {
            // ������ �������� ĳ������ ��ġ�� �����Ͽ� ĳ���͸� ���󰡵��� ��
            navMeshAgent.SetDestination(target.position);
        }
    }
}
