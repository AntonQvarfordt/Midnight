using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var targetScript = (SpawnPoint)target;

        if (GUILayout.Button("Default"))
        {
            targetScript.SetDefaultValues();
        }
    }

}
