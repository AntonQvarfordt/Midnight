using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public Transform Point;
    public GameObject ImpactAnimation;

    private void Update()
    {
        if (!Equipped)
            return;

        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    private void Fire()
    {
        Debug.Log("Fire");

        var camRay = new Ray(Point.position, transform.forward);
        RaycastHit floorHit;

        if (!Physics.Raycast(camRay, out floorHit, 100))
            return;

        FireEffects(floorHit);

        Debug.Log(floorHit.transform.name);
    }

    private void FireEffects (RaycastHit hit)
    {
        var impactAnimation = Instantiate(ImpactAnimation);
        ImpactAnimation.transform.position = hit.point;
        ImpactAnimation.transform.rotation = Quaternion.Euler(hit.normal);
    }
}