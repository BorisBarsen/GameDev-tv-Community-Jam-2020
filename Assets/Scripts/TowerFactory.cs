using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    [SerializeField]List<Tower> towers = new List<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towers.Count < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower();
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        print("Setting tower at: " + baseWaypoint.name);
        var towerClone = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        towers.Add(towerClone);
        baseWaypoint.isPlaceable = false;
    }

    private static void MoveExistingTower()
    {
        print("Tower limit reached, can not place more towers");
    }
}
