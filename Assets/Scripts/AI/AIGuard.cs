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

    public float AttackMaxRange;

    public delegate void MovementStateChanged(MovementState moveState);
    public MovementStateChanged OnMovementStateChanged;
    public delegate void AIStateChanged(AIState moveState);
    public AIStateChanged OnAIStateChanged;
    public bool GunDrawn;

    public AIState State;
    public MovementState MoveState;

    [HideInInspector]
    public PatrolPoint AssignedPoint;
    //[SerializeField]
    //private bool _hasActiveTarget;

    [SerializeField]
    private Player ActiveTarget = null;
    private LayerMask _floorMask;

    private bool _atDestination = true;
    private TimerInvoke _timerBehaviour;
    private AIVisionComponent _visionComponent;

    private float _walkSpeed = 1.7f;
    private float _runSpeed = 2.7f;
    private List<Player> _targetList = new List<Player>();

    public override void Awake()
    {
        base.Awake();
        _floorMask = LayerMask.NameToLayer("Floor");
        _timerBehaviour = gameObject.TimerInvokeInit();
        _visionComponent = GetComponent<AIVisionComponent>();
        _targetList.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());
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

                ActiveTarget = CheckForHostile();

                if (ActiveTarget != null)
                    OnHostileFound();

                Debug.Log("Patrol-UpdateFin");
                break;
            #endregion
            case AIState.Chasing:

                bool targetInSight = false;

                if (ActiveTarget == null)
                    break;

                targetInSight = CheckIfActiveTargetInSight();

                if (!targetInSight)
                {
                    OnHostileLost();
                    SwitchState(AIState.Patrolling);
                    break;
                }

                if (CheckIfActiveTargetInFiringRange())
                {
                    SwitchState(AIState.Shooting);
                    break;
                }

                Debug.Log("Chasing-UpdateFin");

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
                PatrolEnter();
                break;
            case AIState.Chasing:
                ChasingEnter();
                break;
            case AIState.Shooting:

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

    private Player CheckForHostile()
    {
        var visibleTargets = _visionComponent.GetVisibleTargets();

        foreach (var target in visibleTargets)
        {
            var t = (Player)target.GetComponent<ActorMobile>();

            if (t == null)
                continue;

            if (!_targetList.Contains(t))
                continue;

            return t;

        }

        return null;
    }

    private void OnHostileFound()
    {
        Debug.Log("OnHostileFound");
        SwitchState(AIState.Chasing);
    }

    #endregion

    #region Chasing

    private void ChasingEnter()
    {

        State = AIState.Chasing;
        ChangeMovementState(MovementState.Running);
        Agent.stoppingDistance = AttackMaxRange;

    }

    private void ChasingExit()
    {
        Agent.speed = _walkSpeed;
        ChangeMovementState(MovementState.Idle);
    }

    private bool CheckIfActiveTargetInSight()
    {
        return _visionComponent.GetVisibleMobileActors().Contains(ActiveTarget);
    }

    private bool CheckIfActiveTargetInFiringRange()
    {
        if (Vector3.Distance(transform.position, ActiveTarget.transform.position) <= AttackMaxRange)
            return true;
        else
            return false;
    }

    private void OnHostileLost()
    {
        ActiveTarget = null;
    }

    #endregion

    #region Shooting;

    private void ShootingEnter()
    {
        State = AIState.Shooting;
    }

    private void ShootingExit()
    {

    }

    #endregion

    private void WalkToPosition(Vector3 position)
    {
        Agent.SetDestination(position);

        _atDestination = false;
    }

    public void ChangeMovementState(MovementState state)
    {
        switch (state)
        {
            case MovementState.Idle:
                break;
            case MovementState.Walking:
                break;
            case MovementState.Running:
                break;
            case MovementState.Other:
                break;
        }

        MoveState = state;
        OnMovementStateChanged(state);
    }
}
