﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyMovement : MonoBehaviour {

    [Tooltip("In s")]
    [SerializeField] float travelTimePerBlock = 3f;

    bool stop = false; //TODO remove

    float timeTraveledToDestination;

    Vector3 from;
    Vector3 to;

    Vector3 travelPath;

    // Use this for initialization
    void Start () {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(UpdateDestination(path));
    }

    IEnumerator UpdateDestination(List<Waypoint> path)
    {
        print("Start patrol");

        for (int i=0; i < path.Count - 1; i++)
        {
            from = path[i].transform.position;
            to = path[i+1].transform.position;
            travelPath = to - from;

            timeTraveledToDestination = 0f;
            yield return new WaitForSeconds(travelTimePerBlock);
        }

        stop = true;

        GetComponent<EnemyHealth>().KillEnemy();
        print("End patrol");
    }

    private void Update()
    {
        if (!stop)
        {
            float distanceThisFrame = timeTraveledToDestination / travelTimePerBlock;
            transform.position = from + (travelPath * distanceThisFrame);

            timeTraveledToDestination += Time.deltaTime;
        }
    }
}
