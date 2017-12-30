using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileActorReader : MonoBehaviour
{
    public AIGuard AIGuardReference;
    public Animator AnimatorReference;
    public Rigidbody RigidbodyReference;

    public float Speed;

    private Vector3 _lastPos;
    [SerializeField]
    private Vector3 _deltaPos;

    private void Awake()
    {
        _lastPos = transform.position;
    }

    private void OnEnable()
    {
        AIGuardReference.OnMovementStateChanged += OMovementStateChanged;
        AIGuardReference.OnAIStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        AIGuardReference.OnMovementStateChanged -= OMovementStateChanged;
        AIGuardReference.OnAIStateChanged -= OnStateChanged;
    }

    void Update()
    {
        _deltaPos = transform.position - _lastPos;
        Speed = _deltaPos.magnitude * 100 * Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        _lastPos = transform.position;
    }

    private void OMovementStateChanged(MovementState state)
    {
        ResetAnimationParameters();

        Debug.Log("OMovementStateChanged " + state.ToString());

        switch (state)

        {
            case MovementState.Idle:
                AnimatorReference.SetBool("Idle", true);
                break;
            case MovementState.Walking:
                AnimatorReference.SetBool("Walking", true);
                break;
            case MovementState.Running:
                AnimatorReference.SetBool("Running", true);
                break;
            case MovementState.Other:
                break;
            default:
                break;
        }
    }

    private void OnStateChanged(AIState state)
    {
        ResetAnimationParameters();

        Debug.Log("OnStateChanged " + state.ToString());

        switch (state)
        {
            case AIState.Patrolling:
                AnimatorReference.SetBool("GunDrawn", false);
                break;
            case AIState.Chasing:
                AnimatorReference.SetBool("GunDrawn", true);
                break;
            case AIState.Shooting:
                AnimatorReference.SetBool("GunDrawn", true);
                break;
            case AIState.Dead:
                break;
            default:
                break;
        }
    }

    private void ResetAnimationParameters()
    {
        AnimatorReference.SetBool("Idle", false);
        AnimatorReference.SetBool("Running", false);
        AnimatorReference.SetBool("Walking", false);
    }


}
