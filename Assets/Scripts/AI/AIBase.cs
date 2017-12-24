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

    public virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public override void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Killed()
    {
        throw new System.NotImplementedException();
    }
}
