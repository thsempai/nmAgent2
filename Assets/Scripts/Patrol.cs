using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    public GameObject [] waypoints;
    private NavMeshAgent agent;
    private int index = 0;
    private bool isBack = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(waypoints.Length > 0)
            agent.SetDestination(waypoints[index].transform.position);
    }

    private void NextDestination(){

        if(index >= waypoints.Length -1 && !isBack || index <= 0 && isBack){
            isBack = !isBack;
        }
        index += isBack ? -1 : 1;
        agent.SetDestination(waypoints[index].transform.position);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("waypoint")){
            NextDestination();
        }
    }
}
