using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Linq;
//using DG.Tweening;

public static class CustomExtensionMethods
{

#if UNITY_EDITOR
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif

    public static void DrawLine(Vector3 from, Vector3 to, float stay, Material material)
    {
        var go = new GameObject();
        var lineRenderer = go.AddComponent<LineRenderer>();

        lineRenderer.sharedMaterial = material;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.2f;

        Vector3[] positions = new Vector3[2];
        positions[0] = from;
        positions[1] = to;

        lineRenderer.SetPositions(positions);

        //Color2 color = new Color2(Color.clear, Color.white);
        //Color2 color2 = new Color2(Color.clear, Color.clear);

        //lineRenderer.DOColor(color, color2, 0.25f);

        Debug.Log("DrawLine");
    }

    public static TimerInvoke TimerInvokeInit(this GameObject go)
    {
        var returnValue = go.AddComponent<TimerInvoke>();
        return returnValue;
    }
}
