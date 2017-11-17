using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public Transform Point;

    public GameObject MuzzleFlash;
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
        var camRay = new Ray(Point.position, transform.forward);

        RaycastHit hit;

        if (!Physics.Raycast(camRay, out hit, 100))
            return;

        FireEffects(hit);
    }

    private void FireEffects(RaycastHit hit)
    {
        var muzzleFlash = Instantiate(MuzzleFlash);
        var flashPos = Point.transform.position;
        muzzleFlash.transform.position = flashPos;
        muzzleFlash.AddComponent<DestroyAfterTime>().Init(0.5f);

        var impact = Instantiate(ImpactAnimation);
        impact.transform.position = hit.point;

        var gunPos = transform.position;
        gunPos.y = 0;

        var hitPoint = hit.point;
        hitPoint.y = 0;

        impact.transform.LookAt(gunPos - hitPoint);
    }
}