using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float Delay = 0;

    private void Awake()
    {
        if (Delay == 0)
            return;

        Invoke("DestroyGO", Delay);

    }

    public void Init(float delay)
    {
        Invoke("DestroyGO", delay);
    }

    private void DestroyGO()
    {
        Destroy(gameObject);
    }

}
