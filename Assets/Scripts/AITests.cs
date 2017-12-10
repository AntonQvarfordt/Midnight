using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIVisionComponent))]
public class AITests : MonoBehaviour {

    public GameObject VisualizationObject;
    AIVisionComponent fov;

    private void Awake()
    {
        fov = GetComponent<AIVisionComponent>();
    }

    public void SpawnCubesOnPoints ()
    {
        var points = fov.GetEdges();

        var pointA = Instantiate(VisualizationObject);
        pointA.transform.SetParent(transform);
        pointA.transform.localPosition = points.pointA;

        var pointB = Instantiate(VisualizationObject);
        pointB.transform.SetParent(transform);
        pointB.transform.localPosition = points.pointB;
    }

    public void ShowTriangulation ()
    {

    }

    public void HideTriangulation()
    {

    }
}
