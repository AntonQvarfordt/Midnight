using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (AIVisionComponent))]
public class AIVisionComponentEditor : Editor {

	void OnSceneGUI() {

		AIVisionComponent fow = (AIVisionComponent)target;

        DrawViewField(fow);
	}

    private void DrawViewField (AIVisionComponent fow)
    {
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.Eyes.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);

        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.Eyes.position, fow.Eyes.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.Eyes.position, fow.Eyes.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;

        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }

}
