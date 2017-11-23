using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuard : AIBase
{
    public LayerMask FloorMask;

    public Camera CCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camRay = CCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (!Physics.Raycast(camRay, out floorHit, 100, FloorMask)) return;

            Debug.Log("AI Click Point: " + floorHit.point);

            Move(floorHit.point);
        }
    }

    private void Move(Vector3 position)
    {
        Agent.SetDestination(position);
    }

}
