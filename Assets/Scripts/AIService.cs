using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

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



public enum AIType
{
    Guard
}

public class AIService : NetworkBehaviour
{
    public GameObject GuardAIPrefab;

    public List<PatrolPoint> PatrolPointsOnMap = new List<PatrolPoint>();
    public List<SpawnPoint> SpawnPointsOnMap = new List<SpawnPoint>();

    public List<KeyValuePair<PatrolGroups, List<AIBase>>> AcitvePatrolGroups = new List<KeyValuePair<PatrolGroups, List<AIBase>>>();

    public static List<PatrolGroups> PatrolGroupList = new List<PatrolGroups>();

    private void Awake()
    {
        PatrolGroupList.Add(PatrolGroups.A);
        PatrolGroupList.Add(PatrolGroups.B);
        PatrolGroupList.Add(PatrolGroups.D);
        PatrolGroupList.Add(PatrolGroups.E);
        PatrolGroupList.Add(PatrolGroups.Blank);

        GatherControlPoints();
        GatherSpawnPoints();
    }

    private void Start()
    {

        PopulateSpawnPoints();
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            SpawnAIActor(AIType.Guard, SpawnPointsOnMap[Random.Range(0, 4)]);
        }
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

    private void GatherControlPoints()
    {
        var pPoints = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (var pObj in pPoints)
        {
            PatrolPointsOnMap.Add(pObj.GetComponent<PatrolPoint>());
        }
    }

    private void GatherSpawnPoints()
    {
        var pPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach (var pObj in pPoints)
        {
            SpawnPointsOnMap.Add(pObj.GetComponent<SpawnPoint>());
        }
    }

    private void PopulateSpawnPoints ()
    {
        foreach (var spawnPoint in SpawnPointsOnMap)
        {
            SpawnAIActor(AIType.Guard, spawnPoint);
        }
    }

    private void SpawnAIActor (AIType type, SpawnPoint point)
    {
        var aiGuard = Instantiate(GuardAIPrefab);
        var spawnPos = point.transform.position;
        var navMesh = aiGuard.GetComponent<NavMeshAgent>();
        spawnPos.y = 0;
        navMesh.Warp(spawnPos);
        //Debug.Log(point.transform.position);
        //aiGuard.transform.position = point.transform.position;
    }
}
