using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemy : MonoBehaviour
{
    [Header("MOVEMENT")]
    public bool canMove = true;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private Transform pathPointsParent; // Padre dei punti del percorso
    [SerializeField] private float distanceFromPoint = .1f;
    private int currentPathIndex = 0; // Indice del punto del percorso corrente

    [Header("Rotation")]
    [SerializeField] private float lookRotationTime = 0.2f;
    private float lookRotationTimer = 0;

    #region MOVEMENT
    private Vector3 PointToLook
    {
        set
        {
            transform.forward = Vector3.Lerp(transform.forward, value - transform.position, lookRotationTimer / lookRotationTime);
        }
    }
    private int NextPathPoint
    {
        get
        {
                if (currentPathIndex + 1 > pathPointsParent.childCount - 1)
                    return 0;
                return currentPathIndex + 1;
            
        }
    }
    private void LoadNextPoint()
    {
        currentPathIndex = NextPathPoint;
        lookRotationTimer = 0;
    }
    private void MoveToNextPoint()
    {
        Vector3 currentDirection = (pathPointsParent.GetChild(currentPathIndex).position - transform.position).normalized;
        transform.position += movementSpeed * Time.deltaTime * currentDirection;

        if ((transform.position - pathPointsParent.GetChild(currentPathIndex).position).sqrMagnitude <= Mathf.Pow(distanceFromPoint, 2))
                LoadNextPoint();

    }
    #endregion
    #region PATROL
    // Metodo per far muovere il nemico lungo un percorso di punti
    private void PatrolState()
    {
        if(!canMove) return;
        // Look at the next point
        PointToLook = pathPointsParent.GetChild(currentPathIndex).position;
        //Move to the next point
        MoveToNextPoint();
    }
    #endregion
    
    private void Update()
    {
        lookRotationTimer += Time.deltaTime;
        PatrolState();
    }
}