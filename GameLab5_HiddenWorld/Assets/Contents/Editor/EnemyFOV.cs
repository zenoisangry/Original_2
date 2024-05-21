using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]

public class EnemyFOV : Editor
{
    private void OnSceneGUI()
    {
        Enemy enemyfov = (Enemy)target; 
        Transform fovTransform = enemyfov.transform.Find("FOV");
        if (fovTransform == null)
        {
            Debug.LogError("FOV child GameObject is missing.");
            return;
        }

        fovTransform.GetPositionAndRotation(out Vector3 fovPosition, out Quaternion fovRotation);
        Handles.color = Color.white;
        Handles.DrawWireArc(fovPosition, Vector3.up, Vector3.forward, 360, enemyfov.Radius);

        Vector3 viewAngleLeft = DirectionFromAngle(fovRotation.eulerAngles.y, -enemyfov.Angle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(fovRotation.eulerAngles.y, enemyfov.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fovPosition, fovPosition + viewAngleLeft * enemyfov.Radius);
        Handles.DrawLine(fovPosition, fovPosition + viewAngleRight * enemyfov.Radius);

        if (enemyfov.CanSeePlayer && enemyfov.PlayerRef != null)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fovPosition, enemyfov.PlayerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;    
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
