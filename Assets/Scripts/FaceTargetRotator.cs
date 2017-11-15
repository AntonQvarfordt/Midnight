using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FaceTargetRotator : NetworkBehaviour {

    public float Speed = 10;
    public LayerMask floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.

    private float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    private bool initialized = false;
    private Camera _playerCamera;

    public void Init (Camera playerCamera)
    {
        _playerCamera = playerCamera;
        enabled = true;
        initialized = true;
    }
    
    private void Update()
    {
        if (!initialized)
            return;

        Turning(); 
    }

    void Turning()
    {
        Ray camRay = _playerCamera.ScreenPointToRay(CursorController.Cursor.transform.position);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {

            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            if (playerToMouse == Vector3.zero)
                return;

            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotatation;
        }
    }
}
