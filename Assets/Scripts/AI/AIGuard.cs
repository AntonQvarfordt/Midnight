using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuard : AIBase
{
    public LayerMask FloorMask;
    public CursorController Cursor;

    public Camera CCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camRay = CCamera.ScreenPointToRay(Cursor.transform.position);
            RaycastHit floorHit;

            if (!Physics.Raycast(camRay, out floorHit, 100, FloorMask)) return;

            //Move(Cursor.transform.position);
            Move(floorHit.point);
            Cursor.Bop();
        }
    }

    private void Move(Vector3 position)
    {
        Agent.SetDestination(position);
    }

}
