using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class Unit : Entity
{
    public float movementSpeed;

    private Seeker seeker;
    private Path currentPath;
    private IEnumerator moveRoutine;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    public void MoveTo(Vector3 targetPosition)
    {
        seeker.StartPath(transform.position, targetPosition, OnPathCalculated);
    }

    public void OnPathCalculated(Path path)
    {
        if(path.error)
        {
            Debug.LogError("[Unit] Path calculation failed.");
        }
        else
        {
            Debug.Log("Me be moving.");

            currentPath = path;
            StartMoveRoutine();
        }
    }

    private void StartMoveRoutine()
    {
        if(moveRoutine != null)
        StopCoroutine(moveRoutine);
        moveRoutine = MoveRoutine();
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while(currentPath.path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)currentPath.path[0].position, 
            movementSpeed * Time.deltaTime);

            float distToNode = Vector2.Distance(transform.position, (Vector3)currentPath.path[0].position);
            if(distToNode < 0.1f)
            {
                currentPath.path.RemoveAt(0);
            }

            yield return null;
        }
    }
}
