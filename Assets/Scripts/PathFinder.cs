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
        if (path.Count == 0)
        {
            CalculatePath();
        }

        return path;

    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreathsFirstSearch();
        CreatePath();
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
        SetAsPath(endWaypoint);       

        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(startWaypoint);

        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
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
            }
            else
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
        }

        print("Loaded " + grid.Count + " blocks");        
    }

}
