using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FaceTargetRotator : NetworkBehaviour
{
    public float Speed = 10;
    public LayerMask FloorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.

    private readonly float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    private bool _initialized = false;
    private Camera _playerCamera;

    public void Init(Camera playerCamera)
    {
        enabled = true;
        _playerCamera = playerCamera;
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized)
            return;

        Turning();
    }

    private void Turning()
    {
        var camRay = _playerCamera.ScreenPointToRay(CursorController.Cursor.transform.position);
        RaycastHit floorHit;

        if (!Physics.Raycast(camRay, out floorHit, camRayLength, FloorMask)) return;

        var playerToMouse = floorHit.point - transform.position;
        //playerToMouse.y = 0;

        //if (playerToMouse == Vector3.zero)
        //    return;

        var newRotatation = Quaternion.LookRotation(playerToMouse);
        transform.rotation = newRotatation;
    }
}
