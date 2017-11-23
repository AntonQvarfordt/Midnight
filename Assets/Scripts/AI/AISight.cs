using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AISight : MonoBehaviour
{
    public LayerMask RaycastAgainstLayers;

    public float SightDistance;
    public float SightCone;

    public Vector3 arcOrigin;
    public Vector3 arcEnd;

    public Vector3 AINormalVector;

    public List<KeyValuePair<GameObject, RaycastHit>> VisibleObjects = new List<KeyValuePair<GameObject, RaycastHit>>();

    private void Awake()
    {

    }

    private void OnGUI()
    {
        LookUpdate();
    }

    private void LookUpdate()
    {
        arcOrigin = transform.forward;
        arcOrigin.x -= SightCone * 0.5f;
        arcEnd = arcOrigin;
        arcEnd.x += SightCone;

        var hitsInSight = ArcRaycast(arcOrigin, arcEnd);
        VisibleObjects.Clear();



        for (var i = 0; i < hitsInSight.Length; i++)
        {
            RaycastHit hit = hitsInSight[i];
            VisibleObjects.Add(new KeyValuePair<GameObject, RaycastHit>(hit.transform.gameObject, hit));
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

            if (Physics.Raycast(origin, rayVectorStep * SightDistance, out hit, RaycastAgainstLayers))
            {
                if (returnArray.Contains(hit))
                    continue;

                returnArray.Add(hit);
            }

            #region debug

            //Debug.DrawRay(transform.position, rayVectorStep*SightDistance, Color.cyan, 3);
            //Debug.Log("Arc Raycast Iteration Point: " + rayVectorStep);
            #endregion
        }
        return returnArray.ToArray();


    }
}
