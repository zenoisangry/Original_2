using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public Color GizmoColor = Color.green;
    public bool drawLine = true;
    private void OnDrawGizmosSelected()
    {
        for(int currentChild = 0; currentChild < transform.childCount; currentChild++)
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.GetChild(currentChild).position, 0.2f);
            int nextIndex = (currentChild + 1) % transform.childCount;
            if(drawLine)Gizmos.DrawLine(transform.GetChild(currentChild).position, transform.GetChild(nextIndex).position);
        }
    }
}