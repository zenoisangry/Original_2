using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Moving, Stopping, Turning
    }
    public EnemyState currentState = EnemyState.Moving;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private Transform pathPointsParent;
    [SerializeField] private float distanceFromPoint = 0.1f;
    [SerializeField] private float stopDuration = 2.0f; // Duration to stop at each point
    [SerializeField] private float turnDuration = 1.0f; // Duration to perform a U-turn
    private int currentPathIndex = 0;
    private bool movingForward = true; // Direction of movement along the path
    public bool canMove = true;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float stateTimer = 0f;

    [Header("Field of view")]
    [SerializeField] private Color fovColor;  // Color for the FOV mesh
    public float Radius;
    [Range(0,360)] public float Angle;
    public GameObject PlayerRef;

    [Header("Parameters")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    private Color playerDetectedColor = Color.red;
    private MeshFilter fovMeshFilter;
    private MeshRenderer fovMeshRenderer;
    private Transform fovTransform;
    public bool CanSeePlayer;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        PlayerRef = GameObject.FindWithTag("Player");
        ForceFieldOfViewCheck(); // Ensure FOV check when activated

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

        StartCoroutine(FOVRoutine());
    }


    public void ForceFieldOfViewCheck()
    {
        Debug.Log("Forcing FOV check.");
        FieldOfViewCheck();
        Debug.Log($"CanSeePlayer after FOV check: {CanSeePlayer}");
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
            GameManager.Instance.RespawnPlayer(); // Call the RespawnPlayer method
            Debug.Log("Player detected, stopping movement and respawning player.");
        }
        else 
        {
            fovMeshRenderer.material.color = fovColor;
            canMove = true; // Resume movement when player is not detected
            Debug.Log("Player not detected, resuming movement.");
        }
    }

    private void Update()
    {
        if (canMove)
        {
            stateTimer += Time.deltaTime;
            switch (currentState)
            {
                case EnemyState.Moving:
                    MoveToNextPoint();
                    break;
                case EnemyState.Stopping:
                    if (stateTimer >= stopDuration)
                    {
                        if (IsAtEndOfPath())
                        {
                            PrepareTurn();
                        }
                        else
                        {
                            ProceedToNextPoint();
                        }
                    }
                    break;
                case EnemyState.Turning:
                    TurnAround();
                    break;
            }
        }

        UpdateFOVVisualization();
    }
    private void MoveToNextPoint()
    {
        if (pathPointsParent.childCount > 0)
        {
            Transform targetPoint = pathPointsParent.GetChild(currentPathIndex);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, movementSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) <= distanceFromPoint)
            {
                currentState = EnemyState.Stopping;
                stateTimer = 0f;
            }
        }
    }

    private void ProceedToNextPoint()
    {
        currentPathIndex = movingForward ? currentPathIndex + 1 : currentPathIndex - 1;
        currentState = EnemyState.Moving;
    }

    private void PrepareTurn()
    {
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(-90, transform.eulerAngles.y + 180, 0);
        currentState = EnemyState.Turning;
        stateTimer = 0f;
    }

    private void TurnAround()
    {
        float t = stateTimer / turnDuration;
        t = t * t * (3f - 2f * t); // Smoothstep interpolation
        transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

        if (stateTimer >= turnDuration)
        {
            movingForward = !movingForward; // Reverse direction
            ProceedToNextPoint();
        }
    }

    private bool IsAtEndOfPath()
    {
        return (movingForward && currentPathIndex >= pathPointsParent.childCount - 1) ||
               (!movingForward && currentPathIndex <= 0);
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