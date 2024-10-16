using UnityEngine;
using UnityEngine.AI;

public class CustomerHandler : MonoBehaviour
{
    public Transform targetPoint;
    public Transform targetPoint2;// The point the NPC will walk towards

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (targetPoint == null)
        {
            Debug.LogError("Target point is not set for NPC: " + gameObject.name);
        }
        else
        {
            SetDestinationEntrance();
        }
    }

    void Update()
    {
        // Check if the NPC has reached the target point
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // You may perform some actions when the NPC reaches the destination
            Debug.Log("NPC reached the destination!");
        }
    }

    public void SetDestinationEntrance()
    {
        if (targetPoint != null)
        {
            navMeshAgent.SetDestination(targetPoint.position);
        }
    }

    public void SetDestinationExit()
    {
        if (targetPoint2 != null)
        {
            navMeshAgent.SetDestination(targetPoint2.position);
        }
    }
}


