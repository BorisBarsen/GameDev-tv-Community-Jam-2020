using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] float travelTimePerBlock = 3f;

    bool stop = false;

    float timeTraveledToDestination;

    Vector3 from;
    Vector3 to;

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

            timeTraveledToDestination = 0f;
            yield return new WaitForSeconds(travelTimePerBlock);
        }

        stop = true;
        print("End patrol");
    }

    private void Update()
    {
        if (!stop)
        {
            float distanceThisFrame = (timeTraveledToDestination / travelTimePerBlock);
            transform.position = from + ((from - to) * -1 * distanceThisFrame); // TODO fix moving off screen

            timeTraveledToDestination += Time.deltaTime;
        }
    }
}
