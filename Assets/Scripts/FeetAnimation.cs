using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetAnimation : MonoBehaviour
{
    public PlayerMovement PMovementScript;
    private bool moving;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var movement = PMovementScript.GetMovement;

        if (movement.magnitude == 0 && moving)
        {
            transform.rotation = PMovementScript.transform.rotation;
            moving = false;
            _animator.SetBool("Moving", false);
        }
        else if (movement.magnitude != 0 && !moving)
        {
            moving = true;
            _animator.SetBool("Moving", true);
        }

        if (moving)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
