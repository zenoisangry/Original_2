using UnityEngine;
using System.Collections;
using System;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private Transform pathPointsParent;
    [SerializeField] private float distanceFromPoint = 0.1f;
    private int currentPathIndex = 0;
    public bool canMove = true;

    [Header("Rotation")]
    [SerializeField] private float lookRotationTime = 0.2f;
    private float lookRotationTimer = 0;

    [Header("Field of view")]
    [SerializeField] private Color fovColor;  // Color for the FOV mesh
    public float Radius;
    [Range(0,360)] public float Angle;
    public GameObject PlayerRef;

    [Header("Player death")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject playerg;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    private Color playerDetectedColor = Color.red;
    private MeshFilter fovMeshFilter;
    private MeshRenderer fovMeshRenderer;
    private Transform fovTransform;
    public bool CanSeePlayer;

    private void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        fovTransform = transform.Find("FOV");
        if (fovTransform != null)
        {
            fovMeshFilter = fovTransform.GetComponent<MeshFilter>();
            fovMeshRenderer = fovTransform.GetComponent<MeshRenderer>();
            fovMeshRenderer.material.color = fovColor;
        }
        else
        {
            Debug.LogError("FOV child GameObject is missing.");
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                CanSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
            }
            else
                CanSeePlayer = false;
        }
        else
            CanSeePlayer = false;

        // Update FOV color based on player detection
        if (CanSeePlayer)
        {
            fovMeshRenderer.material.color = playerDetectedColor;
            canMove = false; // Stop movement when player is detected
            playerg.SetActive(false);
            player.position = destination.position;
            playerg.SetActive(true);
        }
        else 
        {
            fovMeshRenderer.material.color = fovColor;
            canMove = true; // Resume movement when player is not detected
        }
    }

    private void Update()
    {
        if (canMove)
        {
            lookRotationTimer += Time.deltaTime;
            Patrol();
        }

        UpdateFOVVisualization();
    }
    private void Patrol()
    {
        PointToLook = pathPointsParent.GetChild(currentPathIndex).position;
        MoveToNextPoint();
    }
    private Vector3 PointToLook
    {
        set
        {
            transform.forward = Vector3.Lerp(transform.forward, value - transform.position, lookRotationTimer / lookRotationTime);
        }
    }

    private int NextPathPoint => (currentPathIndex + 1) % pathPointsParent.childCount;

    private void MoveToNextPoint()
    {
        Transform targetPoint = pathPointsParent.GetChild(currentPathIndex);
        Vector3 currentDirection = (targetPoint.position - transform.position).normalized;
        transform.position += movementSpeed * Time.deltaTime * currentDirection;

        if (Vector3.Distance(transform.position, targetPoint.position) <= distanceFromPoint)
        {
            currentPathIndex = NextPathPoint;
            lookRotationTimer = 0;
        }
    }

    private void UpdateFOVVisualization()
    {
        if (fovMeshFilter == null)
        {
            return;
        }

        Mesh mesh = new();
        int stepCount = Mathf.RoundToInt(Angle);
        float stepAngleSize = Angle / stepCount;

        int vertexCount = stepCount + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[stepCount * 3];

        vertices[0] = Vector3.zero;
        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= stepCount; i++)
        {
            float currentAngle = -Angle / 2 + stepAngleSize * i;
            Vector3 direction = DirectionFromAngle(currentAngle, false);
            Vector3 vertex;

            if (Physics.Raycast(fovTransform.position, direction, out RaycastHit hit, Radius, obstructionMask))
            {
                vertex = fovTransform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertex = fovTransform.InverseTransformPoint(fovTransform.position + direction * Radius);
            }

            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        fovMeshFilter.mesh = mesh;
    }

    private Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        float radian = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}