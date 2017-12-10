using System.Collections.Generic;
using UnityEngine;

public enum PatrolGroups
{
    Blank,
    A,
    B,
    C,
    D,
    E
}

public enum SpawnTypes
{
    Player,
    AIAgent,
    Object
}

public class AIService : MonoBehaviour
{
    public List<PatrolPoint> PatrolPointsOnMap = new List<PatrolPoint>();

    public List<KeyValuePair<PatrolGroups, List<AIBase>>> AcitvePatrolGroups = new List<KeyValuePair<PatrolGroups, List<AIBase>>>();

    public static List<PatrolGroups> PatrolGroupList = new List<PatrolGroups>();

    private void Awake()
    {
        PatrolGroupList.Add(PatrolGroups.A);
        PatrolGroupList.Add(PatrolGroups.B);
        PatrolGroupList.Add(PatrolGroups.D);
        PatrolGroupList.Add(PatrolGroups.E);
        PatrolGroupList.Add(PatrolGroups.Blank);
    }

    private void Start()
    {
        PopulateControlPoints();
    }

    public void SubscribeToPatrolGroup(AIBase ai, PatrolGroups group)
    {
        var patrolGKeys = GetPatrolGroupKeys();

        if (patrolGKeys.Contains(group))
        {
            var groupIndex = patrolGKeys.IndexOf(group);
            AcitvePatrolGroups[groupIndex].Value.Add(ai);
            return;
        }
        else
        {

        }
    }

    private void InitPatrolGroups()
    {
        var iterations = 5;

        for (int i = 0; i < iterations; i++)
        {
            AcitvePatrolGroups.Add(new KeyValuePair<PatrolGroups, List<AIBase>>(PatrolGroups.A, new List<AIBase>()));
        }
    }

    private List<PatrolGroups> GetPatrolGroupKeys()
    {
        var returnValue = new List<PatrolGroups>();

        for (int i = 0; 0 < AcitvePatrolGroups.Count; i++)
        {
            var pGroup = AcitvePatrolGroups[i];
            returnValue.Add(pGroup.Key);
        }

        return returnValue;
    }

    private PatrolGroups GetStateFromIndex(int index)
    {
        var patrolGKeys = GetPatrolGroupKeys();

        for (int i = 0; i < patrolGKeys.Count; i++)
        {
            if (index == (int)patrolGKeys[i]) 
            return patrolGKeys[i];
        }

        return PatrolGroups.Blank;
    }

    private void PopulateControlPoints()
    {
        var pPoints = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (var pObj in pPoints)
        {
            PatrolPointsOnMap.Add(pObj.GetComponent<PatrolPoint>());
        }
    }
}
