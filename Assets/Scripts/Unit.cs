using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class Unit : Entity
{
    [Range(0.01f, 10f)]
    public float movementSpeed = 1f;

    Seeker seeker;
    Path currentPath;
    IEnumerator moveRoutine;

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
        if(moveRoutine != null) StopCoroutine(moveRoutine);
        StartCoroutine(moveRoutine = MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while(currentPath.path.Count > 0)
        {
            var node = currentPath.path[0];
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)node.position, 
                movementSpeed * Time.deltaTime);

            var distToNode = Vector3.Distance(transform.position, (Vector3)node.position);
            if(distToNode < 0.1f)
                currentPath.path.RemoveAt(0);

            yield return null;
        }
    }
}
