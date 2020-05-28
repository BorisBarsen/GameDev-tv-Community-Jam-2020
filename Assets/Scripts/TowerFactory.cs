using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int fireTowerLimit = 5;
    [SerializeField] int frostTowerLimit = 5;
    [SerializeField] Tower fireTowerPrefab;
    [SerializeField] Tower frostTowerPrefab;
    [SerializeField] Transform towerParent;

    [SerializeField]Queue<Tower> fireTowers = new Queue<Tower>();
    [SerializeField]Queue<Tower> frostTowers = new Queue<Tower>();

    public void AddFireTower(Waypoint baseWaypoint) //Maybe create a scriptable object for elements?
    {
        if (fireTowers.Count < fireTowerLimit)
        {
            var fireTowerClone = InstantiateNewTower(baseWaypoint, fireTowerPrefab);
            fireTowers.Enqueue(fireTowerClone);
        }
        else
        {
            MoveExistingTower(baseWaypoint, fireTowers);
        }
    }

    public void AddFrostTower(Waypoint baseWaypoint)
    {
        if (frostTowers.Count < frostTowerLimit)
        {
            var frostTowerClone = InstantiateNewTower(baseWaypoint, frostTowerPrefab);
            fireTowers.Enqueue(frostTowerClone);
        }
        else
        {
            MoveExistingTower(baseWaypoint, fireTowers);
        }
    }


    private Tower InstantiateNewTower(Waypoint baseWaypoint, Tower towerPrefab)
    {
        var towerClone = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        towerClone.transform.parent = towerParent;


        towerClone.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        return towerClone;

        //towers.Enqueue(towerClone);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint, Queue<Tower> towers)
    {
        Tower tower = towers.Dequeue();

        Waypoint oldBaseWaypoint = tower.baseWaypoint;
        tower.baseWaypoint = newBaseWaypoint;

        oldBaseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        tower.transform.position = newBaseWaypoint.transform.position;

        towers.Enqueue(tower);
    }
}
