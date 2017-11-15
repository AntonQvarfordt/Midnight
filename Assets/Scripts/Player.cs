using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;
using UnityStandardAssets.Cameras;

public class Player : NetworkBehaviour
{
    public Camera PlayerCamera;
    public CursorController PlayerCursor;

    public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
            return;

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Inventory>().enabled = true;

        SpawnCursor();
        SpawnCamera();

        PlayerCamera.gameObject.GetComponent<CameraFollow>().Init(transform);
        GetComponent<FaceTargetRotator>().Init(PlayerCamera);
        PlayerCursor.GetComponent<CursorController>().enabled = true;
    }

    private void SpawnCursor()
    {
        var cursor = Instantiate(GameManager.GetLocalPlayerPrefab("Cursor"));
        cursor.transform.SetParent(GameObject.Find("/Canvas").transform);
        PlayerCursor = cursor.GetComponent<CursorController>();
    }

    private void SpawnCamera ()
    {
        PlayerCamera = Instantiate(GameManager.GetLocalPlayerPrefab("PlayerCamera")).GetComponent<Camera>();
        PlayerCamera.transform.position = transform.position;
        PlayerCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        PlayerCamera.transform.DOMoveY(10, 2);
    }
}
