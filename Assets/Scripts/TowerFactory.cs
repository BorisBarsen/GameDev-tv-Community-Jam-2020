using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int fireTowerLimit = 5;
    [SerializeField] int frostTowerLimit = 5;
    [SerializeField] Tower fireTowerPrefab;
    [SerializeField] Tower frostTowerPrefab;
    [SerializeField] Transform towerParent;

    [SerializeField] Text towerCounterText;
    //[SerializeField] Text frostTowerCounterText;

    Queue<Tower> fireTowerQueue = new Queue<Tower>();
    Queue<Tower> frostTowerQueue = new Queue<Tower>();

    //int fireTowersAmount;
    //int frostTowersAmount;

    int towerChosen;

    private void Start()
    {
        towerChosen = 1;
    }

    private void Update()
    {
        string towersInfo =
                "Fire Towers left: " + (fireTowerLimit - fireTowerQueue.Count) +
                "\nFrost Towers left: " + (frostTowerLimit - frostTowerQueue.Count) +
                "\n\nCurrently placing: ";

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            towerChosen = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            towerChosen = 2;
        }

        switch (towerChosen)
        {
            case 1:
                towersInfo += "Fire tower";
                break;
            case 2:
                towersInfo += "Frost tower";
                break;
        }

        towerCounterText.text = towersInfo;
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        switch(towerChosen)
        {
            case 1:
                AddFireTower(baseWaypoint);
                break;
            case 2:
                AddFrostTower(baseWaypoint);
                break;
        }
    }

    public void AddFireTower(Waypoint baseWaypoint) //Maybe create a scriptable object for elements?
    {
        if (fireTowerQueue.Count < fireTowerLimit)
        {
            var fireTowerClone = InstantiateNewTower(baseWaypoint, fireTowerPrefab);
            fireTowerClone.SetElement("Fire");
            fireTowerQueue.Enqueue(fireTowerClone);
        }
        else
        {
            MoveExistingTower(baseWaypoint, fireTowerQueue);
        }
    }

    public void AddFrostTower(Waypoint baseWaypoint)
    {
        if (frostTowerQueue.Count < frostTowerLimit)
        {
            var frostTowerClone = InstantiateNewTower(baseWaypoint, frostTowerPrefab);
            frostTowerClone.SetElement("Frost");
            frostTowerQueue.Enqueue(frostTowerClone);
        }
        else
        {
            MoveExistingTower(baseWaypoint, frostTowerQueue);
        }
    }


    private Tower InstantiateNewTower(Waypoint baseWaypoint, Tower towerPrefab)
    {
        var towerClone = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        towerClone.transform.parent = towerParent;


        towerClone.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;
        
        return towerClone;
    }

    //public void RemoveTower(string element)
    //{
    //    switch(element)
    //    {
    //        case "Fire":
    //            fireTowersAmount--;
    //            break;

    //        case "Frost":
    //            frostTowersAmount--;
    //            break;
    //    }

    //}

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

    public void RemoveTower(string towerElement)
    {
        switch(towerElement)
        {
            case "Fire":
                var tower = fireTowerQueue.Dequeue();
                Destroy(tower.gameObject);
                break;

            case "Frost":
                break;
        }
    }
}
