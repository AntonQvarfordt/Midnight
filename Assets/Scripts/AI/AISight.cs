using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISight : MonoBehaviour
{
    public LayerMask RaycastAgainstLayers;

    public float SightDistance;
    public float SightCone;

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            var arcAnchor = new Vector3(-SightCone * 0.01f, 0, 1);
            var arcEnd = new Vector3(arcAnchor.x + (SightCone * 0.01f), 0, 1);
            ArcRaycast(arcAnchor, arcEnd, 20);
        }
    }

    private RaycastHit[] ArcRaycast(Vector3 start, Vector3 end, int iterations = 20)
    {
        var span = end.x - start.x;
        var increment = span / (iterations * 0.5f);

        List<RaycastHit> returnArray = new List<RaycastHit>();

        for (var i = 0; i < iterations; i++)
        {
            var stepIncrement = increment * i;
            var rayVectorStep = new Vector3(start.x + stepIncrement, start.y, start.z);
            var origin = transform.position;
            origin.y += 0.6f;

            RaycastHit hit;

            if (Physics.Raycast(origin, rayVectorStep, out hit, SightDistance, RaycastAgainstLayers))
            {
                if (returnArray.Contains(hit))
                    continue;

                returnArray.Add(hit);
            }

            //#region debug
            //Debug.DrawRay(transform.position, rayVectorStep*SightDistance, Color.cyan, 3);
            //Debug.Log("Arc Raycast Iteration Point: " + rayVectorStep);
            //#endregion
            //Debug.Break();
        }

        foreach (RaycastHit hit in returnArray)
        {
            Debug.Log(hit.transform.name);
        }

        return returnArray.ToArray();

        
    }
}
