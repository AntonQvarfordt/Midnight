using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AITests))]
public class AITestEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var targetScript = (AITests)target;

        if (GUILayout.Button("Find Edges"))
        {
            targetScript.SpawnCubesOnPoints();
        }

        if (GUILayout.Button("Show Triangulation"))
        {
            targetScript.SpawnCubesOnPoints();
        }

        if (GUILayout.Button("Hide Triangulation"))
        {
            targetScript.SpawnCubesOnPoints();
        }
    }

}
