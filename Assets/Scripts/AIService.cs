using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
using System.Collections;

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
    public static AIService Instance;
    public int PatrolShiftMovementRigidity;

    public GameObject GuardAIPrefab;
    public Transform AIActorRoot;

    public List<AIBase> ActiveAiAgents = new List<AIBase>();
    public static List<PatrolPoint> PatrolPointsOnMap = new List<PatrolPoint>();
    public static List<SpawnPoint> SpawnPointsOnMap = new List<SpawnPoint>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            //SpawnAIActor(AIType.Guard, SpawnPointsOnMap[Random.Range(0, 4)]);
            SetRandomAgentNewDestination();

        }
    }

    private void Start()
    {
        GatherSpawnPoints();
        GatherPatrolPoints();
    }

    public void Init()
    {
        GatherSpawnPoints();
        GatherPatrolPoints();
        PopulateSpawnPoints();
        SetWaypointAll();

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

    private void GatherPatrolPoints()
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
        aiGuard.transform.SetParent(AIActorRoot);

        var spawnPos = point.transform.position;

        spawnPos.y = 0;

        var navMesh = aiGuard.GetComponent<NavMeshAgent>();
        var aiScript = aiGuard.GetComponent<AIGuard>();

        ActiveAiAgents.Add(aiScript);
        navMesh.Warp(spawnPos);

        aiGuard.gameObject.SetActive(true);

        Debug.Log("Spawned AI Actor");
        //var patrolPoint = aiScript.SetRandomPatrolDestination(PatrolPointsOnMap.ToArray());


        //while (patrolPoint == null)
        //{
        //    patrolPoint = aiScript.SetRandomPatrolDestination(PatrolPointsOnMap.ToArray());
        //}

        //NetworkServer.Spawn(aiGuard);
    }

    public void SetRandomAgentNewDestination()
    {
        var guard = (AIGuard)ActiveAiAgents[Random.Range(0, ActiveAiAgents.Count)];
        var guardScript = guard.SetRandomPatrolDestination(PatrolPointsOnMap.ToArray());
        Debug.Log("Set Random New Destination");
    }

    public void SetWaypointAll()
    {
        foreach (AIGuard ai in ActiveAiAgents)
        {
            SetRandomPatrolPoint(ai);
        }
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

    private AIBase GetRandomGuard()
    {
        return null;
    }

    private static PatrolPoint GetRandomPatrolPoint()
    {
        foreach (var patrolPoint in PatrolPointsOnMap)
        {
            if (!patrolPoint.Occupied)
                return patrolPoint;
        }

        Debug.LogWarning("AIState: Couldn't Find a Patrol Point that wasn't Occupied");
        return null;
    }

    public void SetRandomPatrolPoint(AIGuard ai)
    {
        ai.SetRandomPatrolDestination(PatrolPointsOnMap.ToArray());
    }

    private IEnumerator PatrolClockIterator(int randomNumberFromSeed)
    {
        var tickValue = Random.Range(0, PatrolShiftMovementRigidity);

        while (tickValue != 1)
        {
            Debug.Log("Clock Tick " + tickValue);
            yield return new WaitForSeconds(4);
            tickValue = Random.Range(0, PatrolShiftMovementRigidity);
        }
    }
}
