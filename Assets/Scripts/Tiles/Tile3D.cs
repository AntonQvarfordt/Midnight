using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Tile3D : Tile
{
    public bool Blocker;

    [ExecuteInEditMode]
    private void Awake()
    {
        colliderType = 0;
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);        
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/3DTile")]
    public static void Create3DTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save 3D Tile", "New 3D Tile", "Asset", "Save 3D Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Tile3D>(), path);
    }
#endif

}
