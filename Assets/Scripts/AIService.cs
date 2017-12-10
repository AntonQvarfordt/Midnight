using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
using System.Collections;

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
    #region properties
    public int PatrolShiftMovementRigidity;

    public GameObject GuardAIPrefab;

    public List<PatrolPoint> PatrolPointsOnMap = new List<PatrolPoint>();
    public List<SpawnPoint> SpawnPointsOnMap = new List<SpawnPoint>();
    public List<KeyValuePair<PatrolGroups, List<AIBase>>> AcitvePatrolGroups = new List<KeyValuePair<PatrolGroups, List<AIBase>>>();
    #endregion

    #region monoCalls

    private void Awake()
    {
        InitPatrolGroups();
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

    #endregion

    #region publicFunctions

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

    #endregion

    #region initializations

    private void AssignAllAIAgentsToPatrolGroupA ()
    {

    }

    private void InitPatrolGroups()
    {
        var iterations = 5;

        for (int i = 0; i < iterations; i++)
        {
            AcitvePatrolGroups.Add(new KeyValuePair<PatrolGroups, List<AIBase>>(PatrolGroups.A, new List<AIBase>()));
        }
    }

    private void GatherSpawnPoints()
    {
        var pPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach (var pObj in pPoints)
        {
            SpawnPointsOnMap.Add(pObj.GetComponent<SpawnPoint>());
        }

        StartPatrolShiftClock();
    }

    private void PopulateSpawnPoints()
    {
        foreach (var spawnPoint in SpawnPointsOnMap)
        {
            SpawnAIActor(AIType.Guard, spawnPoint);
        }


    }

    #endregion

    #region functions

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

    private void SpawnAIActor(AIType type, SpawnPoint point)
    {
        var aiGuard = Instantiate(GuardAIPrefab);
        var spawnPos = point.transform.position;
        var navMesh = aiGuard.GetComponent<NavMeshAgent>();
        spawnPos.y = 0;
        navMesh.Warp(spawnPos);

        SubscribeToPatrolGroup(aiGuard.GetComponent<AIGuard>(), PatrolGroups.A);
    }

    #endregion

    private void StartPatrolShiftClock()
    {
        StartCoroutine("PatrolClockIterator", PatrolShiftMovementRigidity);
    }

    private void StopPatrolShiftClock()
    {
        StopCoroutine("PatrolClockIterator");
    }

    private void EngagePatrolPointTransition(AIGuard guard, Vector3 position)
    {

        guard.Move(position);
    }

    private AIBase GetRandomGuard ()
    {
        return null;
    }

    private PatrolPoint GetRandomPatrolPoint ()
    {
        return null;
    }

    private static AIGuard NewMethod(KeyValuePair<PatrolGroups, List<AIBase>> aiActor)
    {
        var agentList = (List<AIBase>)aiActor.Value;
        var agent = (AIGuard)agentList[Random.Range(0, agentList.Count)];
        return agent;
    }

    private IEnumerator PatrolClockIterator (int randomNumberFromSeed)
    {
        var tickValue = Random.Range(0, PatrolShiftMovementRigidity);

        while (tickValue != 1)
        {
            Debug.Log("Clock Tick " + tickValue);
            yield return new WaitForSeconds(4);
            tickValue = Random.Range(0, PatrolShiftMovementRigidity);
        }

        EngagePatrolPointTransition();



    }
}
