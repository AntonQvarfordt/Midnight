using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;
using Cinemachine;

public class Player : ActorMobile
{
    private Camera _playerCamera;
    public CursorController PlayerCursor;

    public void OnDisable()
    {

        Invoke("DelayedActivate", 2);
    }

    private void DelayedActivate ()
    {
        gameObject.SetActive(true);
    }

    public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
            return;

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Inventory>().enabled = true;
        var cmScript = GameObject.FindGameObjectWithTag("WPPathfinder").GetComponent<CinemachineVirtualCamera>();
        cmScript.Follow = transform;
        //SpawnCamera();
        //SpawnCursor();
        //PlayerCamera.gameObject.GetComponent<CameraFollow>().Init(transform);
        //GetComponent<FaceTargetRotator>().Init(PlayerCamera);
        //PlayerCursor.GetComponent<CursorController>().enabled = true;
        gameObject.SetActive(true);
    }

    private void SpawnCursor()
    {
        var cursor = Instantiate(GameManager.GetLocalPlayerPrefab("Cursor"));
        cursor.transform.SetParent(GameObject.Find("/Canvas").transform);
        //PlayerCursor = cursor.GetComponent<CursorController>();
    }

    private void Awake()
    {

    }

    private void SpawnCamera()
    {
        if (Camera.main != null)
            Camera.main.gameObject.SetActive(false);

        _playerCamera = Instantiate(GameManager.GetLocalPlayerPrefab("PlayerCamera")).GetComponent<Camera>();
        _playerCamera.transform.position = transform.position;
        _playerCamera.transform.DOMoveY(10, 2);
    }

    public override void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Killed()
    {
        throw new System.NotImplementedException();
    }
}
