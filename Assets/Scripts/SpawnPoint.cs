using UnityEngine.Networking;
using UnityEngine;

public class SpawnPoint : NetworkBehaviour
{
    [Header("Set false if spawning from script")]
    public bool AutomaticSpawn = true;

    [Space]
    public SpawnTypes SpawnType;

    public string DefaultActor;

    public AIBase AIToSpawn;

    public override void OnStartServer()
    {

    }

    public void SetDefaultValues ()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var cPos = transform.position;
        cPos.y = 0.05f;
        Gizmos.DrawWireCube(cPos, new Vector3(0.4f, 0.1f, 0.4f));
    }
}
