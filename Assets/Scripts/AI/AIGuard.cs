using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Idle,
    Walking,
    Running,
    Other
}

public class AIGuard : AIBase
{
    public delegate void MovementStateChanged(MovementState moveState);
    public MovementStateChanged OnMovementStateChanged;

    public MovementState MoveState;

    public LayerMask FloorMask;

    [SerializeField]
    private bool _atDestination = true;

    public PatrolPoint AssignedPoint;

    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (!_atDestination)
        {
            if (Agent.hasPath)
                DestinationCheck();
        }
    }

    private void DestinationCheck()
    {
        if (Agent.remainingDistance - 0.05f <= Agent.stoppingDistance)
        {
            _atDestination = true;
            OnDestinationReached();
        }
    }

    public void WalkToPosition(Vector3 position)
    {
        Agent.SetDestination(position);

        _atDestination = false;
    }

    public PatrolPoint SetRandomPatrolDestination(PatrolPoint[] randomBetween)
    {
        AssignedPoint = randomBetween[Random.Range(0, randomBetween.Length)];

        if (AssignedPoint.Occupied)
            return null;

        AssignedPoint.AssignOccupant(this);

        ChangeMovementState(MovementState.Walking);

        WalkToPosition(AssignedPoint.transform.position);

        return AssignedPoint;
    }

    private void OnDestinationReached()
    {
        Debug.Log("AI Guard Destination Reached");
        //if (MoveState == MovementState.Walking)
        OnMovementStateChanged(MovementState.Idle);
    }

    public void ChangeMovementState(MovementState state)
    {
        MoveState = state;
        OnMovementStateChanged(state);
    }
}
