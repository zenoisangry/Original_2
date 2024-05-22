using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform pathPointsParent; // The parent object containing the path points
    public float moveSpeed = 2.0f; // Speed at which the platform moves

    private Transform[] pathPoints; // Array to hold references to the path points
    private int currentPathIndex = 0; // Index of the current path point
    private Transform targetPoint; // The current target point
    private bool movingForward = true; // Flag to track direction

    private void Start()
    {
        // Initialize the path points array with the child points of the parent object
        pathPoints = new Transform[pathPointsParent.childCount];
        for (int i = 0; i < pathPointsParent.childCount; i++)
        {
            pathPoints[i] = pathPointsParent.GetChild(i);
        }

        // Set the initial target point
        targetPoint = pathPoints[currentPathIndex];
    }

    private void Update()
    {
        // Move the platform towards the current target point
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (targetPoint != null)
        {
            // Calculate the step size based on move speed and time
            float step = moveSpeed * Time.deltaTime;

            // Move the platform towards the target point
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

            // Check if the platform has reached the target point
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.001f)
            {
                if (movingForward)
                {
                    currentPathIndex++;
                    if (currentPathIndex >= pathPoints.Length)
                    {
                        currentPathIndex = pathPoints.Length - 2; // Start moving backwards
                        movingForward = false;
                    }
                }
                else
                {
                    currentPathIndex--;
                    if (currentPathIndex < 0)
                    {
                        currentPathIndex = 1; // Start moving forward
                        movingForward = true;
                    }
                }
                targetPoint = pathPoints[currentPathIndex];
            }
        }
    }

}
