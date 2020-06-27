using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Transform[] controlPoints; //The control points of the bezier route

    private Vector2 gizmosPosition; //the current position of the gizmos being drawn

    private void OnDrawGizmos()
    {
        //Bezier Curve gizmo
        for (float t=0; t <= 1; t += 0.05f)
        {
            //cubic bezier curve
            gizmosPosition = (1 - t) * ((1 - t) * controlPoints[0].position + t * controlPoints[1].position)
                + t * ((1 - t) * controlPoints[1].position + t * controlPoints[2].position);

            //draw the curve
            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }
    }
}
