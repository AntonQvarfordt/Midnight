using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class GameManager : NetworkManager {

    public static Item GetItemPrefab (string itemName)
    {
        var itemsList = GetItemList();
   
        foreach (var item in itemsList)
        {
            var itemComponent = item.GetComponent<Item>();
            if (itemComponent == null)
                continue;


            if (itemComponent.Name == itemName)
                return itemComponent;
        }

        return null;
    }

    public static LocalPrefab GetLocalPlayerPrefab(string itemName)
    {
        var prefabList = GetLocalPrefabs();

        foreach (var item in prefabList)
        {
            var prefabScript = item.GetComponent<LocalPrefab>();
            if (prefabScript == null)
                continue;

            if (prefabScript.Id == itemName)
            {
                Debug.Log("Found Item: " + prefabScript.Id);
                return prefabScript;
            }
        }

        return null;
    }

    public static List<GameObject> GetItemList()
    {
        GameObject[] items = Resources.LoadAll<GameObject>("Items");
        return items.ToList();
    }

    public static List<GameObject> GetLocalPrefabs ()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("LocalPlayerPrefabs");
        return prefabs.ToList();
    }
}
