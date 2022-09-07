using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public WayPoint currentWayPoint;
    private NavMeshAgent agent;
    public float timeBeforeRestart = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(currentWayPoint.transform.position);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject == currentWayPoint.gameObject){
            currentWayPoint = currentWayPoint.GetNextWayPoint();
            if(currentWayPoint != null)
                agent.SetDestination(currentWayPoint.transform.position);
        }

    }
}
