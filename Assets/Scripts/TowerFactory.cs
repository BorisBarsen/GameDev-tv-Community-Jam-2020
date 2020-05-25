using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject towerParent;

    [SerializeField]Queue<Tower> towers = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towers.Count < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        print("Setting tower at: " + baseWaypoint.name);

        var towerClone = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        towerClone.transform.parent = towerParent.transform;


        towerClone.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        towers.Enqueue(towerClone);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        Tower tower = towers.Dequeue();

        Waypoint oldBaseWaypoint = tower.baseWaypoint;
        tower.baseWaypoint = newBaseWaypoint;

        oldBaseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        tower.transform.position = newBaseWaypoint.transform.position;

        towers.Enqueue(tower);

        print("Tower limit reached, moving tower from " + oldBaseWaypoint + " to " + newBaseWaypoint);
    }
}
