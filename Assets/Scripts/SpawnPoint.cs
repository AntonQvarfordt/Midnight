using UnityEngine.Networking;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Set false if spawning from script")]
    public bool AutomaticSpawn = true;
    [Space]

    public SpawnTypes SpawnType;

    public AIType ActorType;

    public string DefaultActor;

    public GameObject AIToSpawn;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var cPos = transform.position;
        cPos.y = 0.05f;
        Gizmos.DrawWireCube(cPos, new Vector3(0.4f, 0.1f, 0.4f));
    }
}
