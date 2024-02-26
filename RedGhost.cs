using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedGhost : MonoBehaviour
{
    public Transform target; // 캐릭터의 Transform을 가리키는 변수
    public float followDistance = 5f; // 몬스터가 캐릭터를 따라가는 거리
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 캐릭터와 몬스터 사이의 거리를 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 만약 캐릭터와 몬스터 사이의 거리가 followDistance보다 작거나 같다면
        if (distanceToTarget <= followDistance)
        {
            // 몬스터의 목적지를 캐릭터의 위치로 설정하여 캐릭터를 따라가도록 함
            navMeshAgent.SetDestination(target.position);
        }
    }
}
