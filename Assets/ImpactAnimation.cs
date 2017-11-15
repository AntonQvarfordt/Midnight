using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAnimation : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Destroy", 1);
    }

    private void Start()
    {
    //Debug.Break();
        
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}