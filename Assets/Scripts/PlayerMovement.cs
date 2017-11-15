using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
	public float Speed = 5;

	private float hInput;
	private float vInput;

	[SerializeField]
	private Vector3 movement;

	public Vector3 GetMovement
	{
		get { return movement; }
	}

	private void Update()
	{
		hInput = Input.GetAxis("Horizontal");
		vInput = Input.GetAxis("Vertical");

		SetMovement();

		bool isBlocked = IsBlockedByObstacle();

		if (isBlocked)
			return;

		transform.Translate(movement, Space.World);
	}

	private bool IsBlockedByObstacle()
	{
		var sightHit = MoveDirectionRaycast(movement);
		if (sightHit != null)
		{
			var sh = (RaycastHit)sightHit;
#if UNITY_EDITOR
			Debug.DrawLine(transform.position, sh.transform.position, Color.red);
#endif
			return true;
		}

		return false;
	}

	private void SetMovement()
	{
		movement.Set(hInput, 0, vInput);
		movement = movement * Speed * Time.deltaTime;
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
