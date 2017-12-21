using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class ActorMobile : NetworkBehaviour, IDamageable
{
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public abstract void Damage(float damage);

    public abstract void Killed();

}
