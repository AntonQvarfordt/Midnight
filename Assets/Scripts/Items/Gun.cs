using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public Transform Point;
    public GameObject ImpactAnimation;
    public GameObject FireLine;

    private void Update()
    {
        if (!Equipped)
            return;

        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    private void Fire()
    {
        var camRay = new Ray(Point.position, transform.forward);

        RaycastHit hit;

        if (!Physics.Raycast(camRay, out hit, 100))
            return;

        FireEffects(hit);
    }

    private void FireEffects(RaycastHit hit)
    {
        var impact = Instantiate(ImpactAnimation);
        var fireLine = Instantiate(FireLine);
        impact.transform.position = hit.point;
        fireLine.transform.position = Point.position;
        fireLine.transform.rotation = transform.rotation;

        var gunPos = transform.position;
        gunPos.y = 0;

        var hitPoint = hit.point;
        hitPoint.y = 0;

        impact.transform.LookAt(gunPos - hitPoint);
    }
}