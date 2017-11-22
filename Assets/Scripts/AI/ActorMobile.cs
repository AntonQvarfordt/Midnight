using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMobile : MonoBehaviour, IDamageable
{
    public virtual void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Killed()
    {
        throw new System.NotImplementedException();
    }

}
