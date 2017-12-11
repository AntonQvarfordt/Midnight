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
    public int PatrolShiftMovementRigidity;

    public GameObject GuardAIPrefab;

    public List<AIBase> ActiveAiAgents = new List<AIBase>();
    public static List<PatrolPoint> PatrolPointsOnMap = new List<PatrolPoint>();
    public static List<SpawnPoint> SpawnPointsOnMap = new List<SpawnPoint>();


    private void Awake()
    {
        GatherControlPoints();
        GatherSpawnPoints();
    }

    private void Start()
    {

        //PopulateSpawnPoints();
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            SpawnAIActor(AIType.Guard, SpawnPointsOnMap[Random.Range(0, 4)]);
        }
    }


    private void AssignAllAIAgentsToPatrolGroupA ()
    {

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
        spawnPos.y = 0;

        var navMesh = aiGuard.GetComponent<NavMeshAgent>();

        ActiveAiAgents.Add(aiGuard.GetComponent<AIGuard>());

        navMesh.Warp(spawnPos);
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

    private static PatrolPoint GetRandomPatrolPoint ()
    {
        foreach (var patrolPoint in PatrolPointsOnMap)
        {
            if (!patrolPoint.Occupied)
                return patrolPoint;
        }

        Debug.LogWarning("AIState: Couldn't Find a Patrol Point that wasn't Occupied");
        return null;
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





    }

}
