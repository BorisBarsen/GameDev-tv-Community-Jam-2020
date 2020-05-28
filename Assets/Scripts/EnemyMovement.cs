using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] float baseSpeed = 20f;
    [Range(0f, 1f)]
    [SerializeField] float speedFactor = 1f;

    List<Waypoint> path;
    Vector3 target;
    Vector3 travelPath;

    // Counter
    int waypointsPassed = 0;

    public void SetSpeedFactor(float factor)
    {
        if (speedFactor < 0 || speedFactor > 1) Debug.LogWarning("Speed factor set out of range!(" + factor + ")");

        speedFactor = factor;
    }

    // Use this for initialization
    void Start () {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        path = pathfinder.GetPath();
        target = path[++waypointsPassed].transform.position;
    }

    private void Update()
    {
        float step = baseSpeed * speedFactor * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {    
            if (waypointsPassed == path.Count)
            {
                GetComponent<EnemyHealth>().KillEnemy();
            }
            else
            {
                target = path[waypointsPassed++].transform.position;
            }
        }
    }
}
