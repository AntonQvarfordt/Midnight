using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMovementAnimationBridge : MonoBehaviour {

    private PlayerMovement _movementScript;
    private Animator _animator;

    private void Awake()
    {
        _movementScript = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("vInput", _movementScript.GetInputV);
        _animator.SetFloat("hInput", _movementScript.GetInputH);
        _animator.SetBool("Grounded", _movementScript.IsGrounded);
        _animator.SetFloat("Velocity", _movementScript.GetMovementSpeed);
    }
}
