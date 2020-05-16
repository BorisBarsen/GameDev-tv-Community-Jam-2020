using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    // Use this for initialization
    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            if (!grid.ContainsKey(waypoint.GetGridPos()))
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
                SetTopColor(waypoint);
            }
            else
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
        }

        print("Loaded " + grid.Count + " blocks");        
    }

    private void SetTopColor(Waypoint waypoint)
    {
        if (waypoint.Equals(startWaypoint))
        {
            waypoint.SetTopColor(Color.green);
        }
        else if (waypoint.Equals(endWaypoint))
        {
            waypoint.SetTopColor(Color.red);
        }
        else
        {
            waypoint.SetTopColor(Color.gray);
        }
    }
}
