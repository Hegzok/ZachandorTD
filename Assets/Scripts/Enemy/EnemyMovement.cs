using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement
{
    private NavMeshAgent navMeshAgent;
    private Transform[] patrolPoints;

    private float timeToWaitIdleOnPatrol;
    private float lastTimeWaited;
    private bool lastTimeWaitedAlreadySet;

    private int patrolIndex = 0;

    public EnemyMovement(NavMeshAgent navMeshAgent, Transform[] patrolPoints, float timeToWaitIdleOnPatrol)
    {
        this.navMeshAgent = navMeshAgent;
        this.patrolPoints = patrolPoints;
        this.timeToWaitIdleOnPatrol = timeToWaitIdleOnPatrol;
    }

    public void MoveTo(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    public void Patrol(Vector3 currentPos)
    {
        if (patrolPoints.Length >= 1)
        {
            MoveTo(patrolPoints[patrolIndex].position);
            CheckIfArrivedAtPatrolPoint(currentPos);
        }
    }

    public void GoToNextSpot()
    {
        if (Time.time > timeToWaitIdleOnPatrol + lastTimeWaited)
        {
            patrolIndex++;
            lastTimeWaitedAlreadySet = false;

            if (patrolIndex >= patrolPoints.Length)
            {
                patrolIndex = 0;
            }
        }
    }

    private void CheckIfArrivedAtPatrolPoint(Vector3 currentPos)
    {
        float dist = Vector3.Distance(currentPos, (patrolPoints[patrolIndex].position));

        if (dist <= 1f)
        {
            if (!lastTimeWaitedAlreadySet)
            {
                lastTimeWaited = Time.time;
                lastTimeWaitedAlreadySet = true;
            }

            GoToNextSpot();
        }
    }
}
