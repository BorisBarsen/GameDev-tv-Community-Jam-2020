using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    private List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        BreathsFirstSearch();
        CreatePath();
        return path;
    }

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public void CreatePath()
    {
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);

        path.Reverse();
    }

    private void BreathsFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HoldIfEndFound();
            ExploreNeighbors();
            searchCenter.discovered = true;
        }

        // todo work out path
        print("Finished pathfinding?");
    }

    private void HoldIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        if (!isRunning) { return; }

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = searchCenter.GetGridPos() + direction;

            if (grid.ContainsKey(neighborCoordinates))            
            {
                QueueNewNeighbors(neighborCoordinates);
            }

        }
    }

    private void QueueNewNeighbors(Vector2Int explorationCoordinates)
    {
        Waypoint neighbor = grid[explorationCoordinates];

        if (!neighbor.discovered && !queue.Contains(neighbor))
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }
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
