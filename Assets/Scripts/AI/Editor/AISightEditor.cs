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

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        for (var i = 0; i < targetScript.VisibleObjects.Count; i++)
        {
            var visibleObject = targetScript.VisibleObjects[i];
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(visibleObject.Key.transform.name);
            EditorGUILayout.LabelField(visibleObject.Value.point.ToString());
            EditorGUILayout.EndHorizontal();
            targetScript.AINormalVector = -targetScript.transform.forward;

        }
    }

    private void OnSceneGUI()
    {
        Handles.color = new Color(1, 1, 1, 0.2f);

        Handles.DrawSolidArc(targetScript.transform.position, targetScript.transform.up, targetScript.arcOrigin, targetScript.SightCone, targetScript.SightDistance);

        Handles.color = Color.white;
    }
}
