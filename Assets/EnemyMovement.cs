using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Start patrol");
        foreach (Waypoint waypoint in path)
        {
            gameObject.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
        print("End patrol");
    }
}
