using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBase : ActorMobile
{
    public bool Disabled;

    private NavMeshAgent _navAgent;

    public NavMeshAgent Agent
    {
        get { return _navAgent; }
    }

    public void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }
}
