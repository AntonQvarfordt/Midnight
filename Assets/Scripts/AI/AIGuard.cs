using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public enum MovementState
{
    Idle,
    Walking,
    Running,
    Other
}

public enum AIState
{
    Patrolling,
    Chasing,
    Shooting,
    Dead
}

public class AIGuard : AIBase
{
    public delegate void MovementStateChanged(MovementState moveState);
    public MovementStateChanged OnMovementStateChanged;

    public PatrolPoint AssignedPoint;

    public AIState State;
    public MovementState MoveState;

    private LayerMask _floorMask;

    [SerializeField]
    private bool _atDestination = true;
    private TimerInvoke _timerBehaviour;

    public override void Awake()
    {
        base.Awake();
        _floorMask = LayerMask.NameToLayer("Floor");
        _timerBehaviour = gameObject.TimerInvokeInit();
    }

    private void Update()
    {
        if (State == AIState.Patrolling)
        {
            if (!_atDestination)
            {
                if (Agent.hasPath)
                    PatrolDestinationCheck();
            }
        }
    }

    private void PatrolDestinationCheck()
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
        var newPoint = randomBetween[Random.Range(0, randomBetween.Length)];

        while (newPoint.Occupied)
        {
            newPoint = randomBetween[Random.Range(0, randomBetween.Length)];
        }

        AssignedPoint = newPoint.AssignOccupant(this);

        ChangeMovementState(MovementState.Walking);

        WalkToPosition(AssignedPoint.transform.position);

        return AssignedPoint;
    }

    private void OnDestinationReached()
    {
        Debug.Log("AI Guard Destination Reached");
        //if (MoveState == MovementState.Walking)
        OnMovementStateChanged(MovementState.Idle);
        var moveActions = new UnityAction[1];
        moveActions[0] = new UnityAction(() => AIService.Instance.SetRandomPatrolPoint(this));
        var cancelTimerCallback = _timerBehaviour.SetTimedInvoke(1, 6, moveActions);

        if (AssignedPoint != null)
            transform.DOLocalRotate(AssignedPoint.transform.localRotation.eulerAngles, 0.5f);
    }

    public void ChangeMovementState(MovementState state)
    {
        MoveState = state;
        OnMovementStateChanged(state);
    }
}
