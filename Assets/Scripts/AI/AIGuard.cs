using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuard : AIBase
{
    public LayerMask FloorMask;

    [SerializeField]
    private bool _atDestination;

    public PatrolPoint AssignedPoint;

    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (!_atDestination)
        {
            DestinationCheck();
        }
    }

    private void DestinationCheck()
    {
        Debug.DrawLine(transform.position, Agent.destination);
        if (Agent.remainingDistance < Agent.stoppingDistance)
        {
            _atDestination = true;
            OnDestinationReached();
        }
    }

    public void MoveToPosition (Vector3 position)
    {

        //Agent.ResetPath();
        //Agent.CalculatePath(position, Agent.path);
        Debug.Log(position);
        Debug.Log(Agent.destination);

        Agent.Move(position);

        _atDestination = false;
        Debug.Log("AI Guard Moving To Destination");
    }

    public void MoveToPatrolPoint (PatrolPoint point)
    {
        Agent.Move(point.transform.position);
    }

    public PatrolPoint SetRandomPatrolDestination (PatrolPoint[] randomBetween)
    {
        var newPatrolPoint = randomBetween[Random.Range(0, randomBetween.Length)];

        if (newPatrolPoint.Occupied)
            return null;

        newPatrolPoint.AssignOccupant(this);
        AssignedPoint = newPatrolPoint;

        MoveToPosition(newPatrolPoint.transform.position);

        return newPatrolPoint;
    }

    private void OnDestinationReached ()
    {
        Debug.Log("AI Guard Destination Reached");
    }
}
