using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuard : AIBase
{
    public LayerMask FloorMask;
    public CursorController CCursor;

    public Camera CCamera;

    private void Start()
    {
        CCamera = Camera.main;
        CCursor = CursorController.Cursor;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camRay = CCamera.ScreenPointToRay(CCursor.transform.position);
            RaycastHit floorHit;

            if (!Physics.Raycast(camRay, out floorHit, 100, FloorMask)) return;

            //Move(Cursor.transform.position);
            Move(floorHit.point);
            CCursor.Bop();
        }
    }

    private void Move(Vector3 position)
    {
        Agent.SetDestination(position);
    }

}
