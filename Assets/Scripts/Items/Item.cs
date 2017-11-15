using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Item : NetworkBehaviour {

    public int Id;
    public string Name;

    private void Awake()
    {
        Name = GetType().Name;
    }

}
