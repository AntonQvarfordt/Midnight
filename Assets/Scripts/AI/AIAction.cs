using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActionData
{
    public PatrolPoint Point;
    public MovementState State;
}

public abstract class AIAction : ScriptableObject {

    public abstract void Act(AIGuard aiGuard, ActionData aData);

}
