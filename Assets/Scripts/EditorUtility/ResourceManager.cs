using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceManager : MonoBehaviour
{
    public string ManagedResourcesRoot = "Assets/Resources/ManagedResources";

    public static List<List<KeyValuePair<string, Object>>> ManagedResources = new List<List<KeyValuePair<string, Object>>>();

    private List<string> _subFolders = new List<string>();

    public List<string> GetSubfolders
    {
        get { return _subFolders; }
    }

    private void Awake()
    {
        LoadManagedResources();
    }

    private void LoadManagedResources()
    {
        _subFolders = AssetDatabase.GetSubFolders(ManagedResourcesRoot).ToList();

        for (var i = 0; i < _subFolders.Count; i++)
        {
            var folder = _subFolders[i];
            var splitPath = folder.Split('/');
            _subFolders[i] = splitPath[splitPath.Length - 1];

            ManagedResources.Add(new List<KeyValuePair<string, Object>>());
        }

        PopulateCategories();
    }

    private void PopulateCategories()
    {
        for (var i = 0; i < ManagedResources.Count; i++)
        {
            var subFolder = _subFolders[i];
            var subfolderPath = "ManagedResources" + "/" + subFolder + "/";
            var subFolderResources = Resources.LoadAll(subfolderPath);

            for (var j = 0; j < subFolderResources.Length; j++)
            {
                var obj = subFolderResources[j];

                if (!ManagedResources[i].Contains(ManagedResources[i].Find(x => x.Key == obj.name)))
                    ManagedResources[i].Add(new KeyValuePair<string, Object>(obj.name, obj));
            }
        }
    }

    public static Object GetResource(string key)
    {
        for (var i = 0; i < ManagedResources.Count; i++)
        {
            var assetList = ManagedResources[i];

            foreach (var resource in assetList)
            {
                if (resource.Key == key)
                    return resource.Value;
            }
        }

        return null;
    }
}
