using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public enum State
    {
        walking,
        waiting
    }

    public State status = State.walking;
    private State previousStatus = State.walking;
    public WayPoint currentWayPoint;
    private NavMeshAgent agent;
    public float timeBeforeRestart = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(currentWayPoint.transform.position);
    }

    void Update(){
        if(status != previousStatus){
            switch (status)
            {
                case State.walking: 
                if(currentWayPoint != null)
                    agent.SetDestination(currentWayPoint.transform.position);
                break;

                case State.waiting:
                    StartCoroutine(Wait());
                    break;
            }
            previousStatus = status;
        }
    }

    IEnumerator Wait(){
        agent.isStopped = true;
        yield return new WaitForSeconds(timeBeforeRestart);
        agent.isStopped = false; 
        status = State.walking; 
    } 

    private void OnTriggerEnter(Collider other){
        if(other.gameObject == currentWayPoint.gameObject){
            currentWayPoint = currentWayPoint.GetNextWayPoint();
            if(currentWayPoint != null)
                status = State.waiting;
        }

    }
}
