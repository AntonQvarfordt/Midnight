using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public bool Occupied;
    public bool Disabled;
    public int FloorLevel;

    private AIBase _occupant;

    public AIBase GetOccupant 
    {
       get { return _occupant; }
    }

    [Range(0, 100)]
    public float Weight = 50;

    public bool AssignOccupant (AIBase occupant)
    {
        if (Occupied)
            return false;

        Occupied = true;
        _occupant = occupant;
        return true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var cPos = transform.position;
        //cPos.y = 0.05f;
        Gizmos.DrawWireCube(cPos, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
