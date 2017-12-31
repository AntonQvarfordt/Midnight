using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;

public class PlayerMovement : NetworkBehaviour
{
    public LayerMask GroundLayer;
	public float Speed = 5;
    public bool _grounded;
    public Transform GroundCheckPosition;

    public float DodgeDistance = 4;

	[SerializeField]
	private Vector3 movement;

    private float hInput;
    private float vInput;

    private Rigidbody _rigidbody;

    private Vector3 savedPos;
    [SerializeField]
    private Vector3 deltaPos;

    private bool _movementBlocked;

    public Vector3 GetMovement
	{
		get { return movement; }
	}

    public bool IsGrounded
    {
        get { return _grounded; }
    }

    public float GetInputH
    {
        get { return hInput; }
    }

    public float GetInputV
    {
        get { return vInput; }
    }

    public float GetMovementSpeed
    {
        get { return deltaPos.magnitude; }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        savedPos = transform.position;

    }

    private void Update()
	{
		hInput = Input.GetAxis("Horizontal");
		vInput = Input.GetAxis("Vertical");

        _grounded = IsGroundedCheck();

        if (_movementBlocked)
            return;

		SetMovement();
        FaceMovementDirection();


        if (Input.GetButtonDown("Dodge"))
        {
            DodgeRoll();
        }
    }

    private void CalculateDeltaPos ()
    {
        deltaPos = transform.position - savedPos;
        deltaPos *= 10;
        //Debug.Log(GetMovementSpeed);
        savedPos = transform.position;
    }

    private void FixedUpdate()
    {
        bool isBlocked = IsBlockedByObstacle();

        if (isBlocked)
            return;

        _rigidbody.MovePosition(transform.position+=movement);
        CalculateDeltaPos();
    }

    public void BlockMovement ()
    {
        _movementBlocked = true;
    }

    public void UnblockMovement()
    {
        _movementBlocked = false;
    }

    private bool IsBlockedByObstacle()
	{
		var sightHit = MoveDirectionRaycast(movement);
		if (sightHit != null)
		{
			var sh = (RaycastHit)sightHit;
#if UNITY_EDITOR
			Debug.DrawLine(transform.position, sh.transform.position, Color.red, 1);
#endif
			return true;
		}

		return false;
	}

	private void SetMovement()
	{
		movement.Set(hInput, 0, vInput);
		movement = movement * Speed;
	}

    private bool IsGroundedCheck ()
    {
        RaycastHit hit;
        if (Physics.Raycast(GroundCheckPosition.position, Vector3.down, out hit, 0.2f, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FaceMovementDirection ()
    {
        var dir = new Vector3(hInput, 0, vInput);
        var rotationSpeed = 50;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private void DodgeRoll ()
    {
        RaycastHit hit;
        var rayStartPos = transform.position;
        rayStartPos.y = 1;
        Debug.DrawRay(rayStartPos, transform.forward * DodgeDistance, Color.red, 2);
        if (Physics.Raycast(rayStartPos, transform.forward, out hit, DodgeDistance))
        {
            Debug.Log(hit.transform.name);
            return;
        }
        BlockMovement();

        GetComponent<Animator>().SetTrigger("Dodge");
        var targetPos = transform.position + (transform.forward * DodgeDistance);
        _rigidbody.isKinematic = true;
        transform.DOMove(targetPos, 0.7f).SetEase(Ease.Linear).OnComplete(DodgeRollComplete);
        
    }

    private void DodgeRollComplete()
    {
        _rigidbody.isKinematic = false;
        UnblockMovement();
    }

	private RaycastHit? MoveDirectionRaycast(Vector3 movement)
	{
		RaycastHit hit;

		if (Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), movement, out hit, 0.5f))
		{
			return hit;
		}

		return null;
	}
}
