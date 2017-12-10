using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCameraFollower : MonoBehaviour {

    private Camera PlayerCamera;
    public CursorController PlayerCursor;

    private void Awake()
    {
        SpawnCamera();

    }
    private void SpawnCamera()
    {
        //if (Camera.main != null)
        //    Camera.main.enabled = false;
        PlayerCamera = Instantiate(GameManager.GetLocalPlayerPrefab("PlayerCamera")).GetComponent<Camera>();
        PlayerCamera.transform.position = transform.position;
        PlayerCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        //GetComponent<FaceTargetRotator>().Init(PlayerCamera);
        PlayerCursor.GetComponent<CursorController>().enabled = true;
        //PlayerCamera.transform.DOMoveY(10, 2);
    }
}
