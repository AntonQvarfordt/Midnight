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

    public void MoveToRandomSpawnPoint ()
    {

    }

    public void Move(Vector3 position)
    {
        Agent.SetDestination(position);
    }
}
