using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AISight))]
public class AISightEditor : Editor
{
    private AISight targetScript;

    private void Awake()
    {
        targetScript = (AISight)target;
    }

    private void OnSceneGUI()
    {
        Handles.color = new Color(1, 1, 1, 0.2f);
        var arcAnchor = new Vector3(-targetScript.SightCone * 0.01f, 0, 1);
        var originPos = targetScript.transform.position;
        originPos.y = 0.6f;
        Handles.DrawSolidArc(originPos, targetScript.transform.up, arcAnchor, targetScript.SightCone, targetScript.SightDistance);
        Handles.color = Color.white;
    }
}
