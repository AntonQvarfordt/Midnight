using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ResourceManager))]
public class ResourceManagerEditor : Editor
{
    readonly GUIStyle _styleGreen = new GUIStyle();
    readonly GUIStyle _styleBlack = new GUIStyle();
    readonly GUIStyle _styleHeader = new GUIStyle();

    private List<string> resourceFolders;
    private List<List<KeyValuePair<string, Object>>> listItems = new List<List<KeyValuePair<string, Object>>>();

    private void Awake()
    {
        _styleHeader.fontStyle = FontStyle.Bold;

        _styleGreen.richText = true;
        _styleGreen.fontStyle = FontStyle.Italic;

        _styleBlack.richText = true;
        _styleBlack.fontSize = 10;

        var targetScript = (ResourceManager)target;

        resourceFolders = targetScript.GetSubfolders;
        listItems = ResourceManager.ManagedResources;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        for (var i = 0; i < resourceFolders.Count; i++)
        {
            var folder = resourceFolders[i];
            EditorGUILayout.LabelField(folder, _styleHeader);

            for (var j = 0; j < listItems[i].Count; j++)
            {
                var item = listItems[i][j];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(j.ToString(), GUILayout.Width(32));
                EditorGUILayout.LabelField(item.Key, _styleBlack);
                EditorGUILayout.LabelField(item.Value.GetType().ToString(), _styleGreen);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
