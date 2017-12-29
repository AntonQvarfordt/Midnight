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
    }

    private void OnDisable()
    {
        AIGuardReference.OnMovementStateChanged -= OMovementStateChanged;
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

    private void ResetAnimationParameters()
    {
        AnimatorReference.SetBool("Idle", false);
        AnimatorReference.SetBool("Running", false);
        AnimatorReference.SetBool("Walking", false);
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
}
