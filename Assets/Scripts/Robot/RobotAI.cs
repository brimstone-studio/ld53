using System;
using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    [NonSerialized]
    public Transform player;

    private readonly float searchRadius = 30f;
    
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 targetPosition = player.position;

        // Find the nearest point on the NavMesh within the search radius
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, searchRadius, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
        }

        if (NavMeshAgent.isOnNavMesh && NavMeshAgent.isActiveAndEnabled)
        {
            NavMeshAgent.SetDestination(targetPosition);
        }
    }
}