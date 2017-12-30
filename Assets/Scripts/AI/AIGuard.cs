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
    public delegate void AIStateChanged(AIState moveState);
    public AIStateChanged OnAIStateChanged;

    public List<Player> TargetList;
    public Player ActiveTarget;

    public bool GunDrawn;

    public PatrolPoint AssignedPoint;

    public AIState State;
    public MovementState MoveState;

    private LayerMask _floorMask;

    [SerializeField]
    private bool _atDestination = true;
    private TimerInvoke _timerBehaviour;
    private AIVisionComponent _visionComponent;
    private bool _hasActiveTarget;

    public override void Awake()
    {
        base.Awake();
        _floorMask = LayerMask.NameToLayer("Floor");
        _timerBehaviour = gameObject.TimerInvokeInit();
        _visionComponent = GetComponent<AIVisionComponent>();
    }

    private void Update()
    {

        switch (State)
        {
            #region patrolUpdate
            case AIState.Patrolling:
                if (!_atDestination && Agent.hasPath)
                {
                    if (CheckPatrolDestinationReached())
                        OnDestinationReached();

                }

                if (!_hasActiveTarget)
                {
                    ActiveTarget = CheckForHostile();
                    if (ActiveTarget != null)
                        OnHostileFound();
                }

                break;
            #endregion
            case AIState.Chasing:

                bool targetInSight = false;

                if (ActiveTarget == null)
                    break;

                    targetInSight = CheckIfActiveTargetInView();

                if (!targetInSight)
                {
                    OnHostileLost();
                    SwitchState(AIState.Patrolling);

                }

                break;
        }

    }

    private void SwitchState(AIState state)
    {
        switch (State)
        {
            case AIState.Patrolling:
                PatrolExit();
                break;
            case AIState.Chasing:
                ChasingExit();
                break;
            case AIState.Shooting:
                ShootingExit();
                break;
            case AIState.Dead:
                break;
        }

        switch (state)
        {
            case AIState.Patrolling:
                State = AIState.Patrolling;
                PatrolEnter();
                break;
            case AIState.Chasing:
                State = AIState.Chasing;
                ChasingEnter();
                break;
            case AIState.Shooting:
                State = AIState.Shooting;
                ShootingEnter();
                break;
            case AIState.Dead:
                State = AIState.Dead;
                break;
        }

        OnAIStateChanged(state);
    }

    #region Patrol

    private void PatrolEnter()
    {

    }

    private void PatrolExit()
    {
        if (AssignedPoint != null)
            AssignedPoint.Clear();
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

    private bool CheckPatrolDestinationReached()
    {
        return (Agent.remainingDistance - 0.05f <= Agent.stoppingDistance);
    }

    private void OnDestinationReached()
    {
        _atDestination = true;

        Debug.Log("AI Guard Destination Reached");

        OnMovementStateChanged(MovementState.Idle);

        var moveActions = new UnityAction[1];
        moveActions[0] = new UnityAction(() => AIService.Instance.SetRandomPatrolPoint(this));

        var cancelTimerCallback = _timerBehaviour.SetTimedInvoke(1, 6, moveActions);

        if (AssignedPoint != null)
            transform.DOLocalRotate(AssignedPoint.transform.localRotation.eulerAngles, 0.5f);
    }

    private void OnHostileFound()
    {
        Debug.Log("OnHostileFound");
        _hasActiveTarget = true;
        SwitchState(AIState.Chasing);
    }

    private void OnHostileLost ()
    {
        _hasActiveTarget = false;
        ActiveTarget = null;
    }

    private bool CheckIfActiveTargetInView()
    {
        return _visionComponent.GetVisibleMobileActors().Contains(ActiveTarget);
    }

    private Player CheckForHostile()
    {
        var visibleTargets = _visionComponent.GetVisibleTargets();
        foreach (var target in visibleTargets)
        {
            var t = (Player)target.GetComponent<ActorMobile>();

            if (t != null)
            {
                if (!TargetList.Contains(t))
                    continue;

                return t;
            }
        }

        return null;
    }

    #endregion

    private void ShootingEnter()
    {

    }

    private void ShootingExit()
    {

    }

    private void ChasingExit()
    {

    }

    private void ChasingEnter()
    {
        State = AIState.Chasing;
    }

    private void WalkToPosition(Vector3 position)
    {
        Agent.SetDestination(position);

        _atDestination = false;
    }



    public void ChangeMovementState(MovementState state)
    {
        MoveState = state;
        OnMovementStateChanged(state);
    }
}