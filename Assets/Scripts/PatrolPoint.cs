using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public bool Occupied;
    public bool Disabled;
    public int FloorLevel;

    [Range(0, 100)]
    public float Weight = 50;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var cPos = transform.position;
        cPos.y = 0.05f;
        Gizmos.DrawWireCube(cPos, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
