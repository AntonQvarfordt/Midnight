using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Item : NetworkBehaviour {

    public int Id;
    public string Name;

    [SerializeField]
    private bool _equipped;
    private bool _disabled;

    public bool Equipped { set { _equipped = value; } get { return _equipped; } }

    private void Awake()
    {
        Name = GetType().Name;
    }


}
