using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : LocalPrefab {

    public static CursorController Cursor;
    RectTransform rTransform;

    [SerializeField]
    private Vector3 mouseDeltaPos;
    private Vector2 mouseLastPos;

    public Vector2 GetCursorPosition
    {
        get { return RectTransformUtility.WorldToScreenPoint(null, rTransform.position); }
    }

    private void Awake()
    {
        Cursor = this;
        rTransform = (RectTransform)transform;
        mouseLastPos = Input.mousePosition;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = false;
        rTransform.anchoredPosition = Vector3.zero;
        enabled = true;
    }

    private void Update()
    {
        mouseDeltaPos = (Vector2)Input.mousePosition - mouseLastPos;
        rTransform.anchoredPosition += (Vector2)mouseDeltaPos;
    }

    private void LateUpdate()
    {
        mouseLastPos = Input.mousePosition;
    }

    public void Bop ()
    {
        GetComponent<CursorEffects>().Bop(transform);
    }
}
