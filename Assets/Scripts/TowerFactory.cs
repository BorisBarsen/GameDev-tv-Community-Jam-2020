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

    List<Tower> fireTowers = new List<Tower>();
    List<Tower> frostTowers = new List<Tower>();

    //int fireTowersAmount;
    //int frostTowersAmount;

    int towerChosen;
    string towersInfo;

    private void Start()
    {
        towerChosen = 1;
        StartCoroutine(UpdateTowersInfo());
    }

    IEnumerator UpdateTowersInfo()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            towerCounterText.text = towersInfo;
        }
    }

    private void Update()
    {

        towersInfo =
            "Fire Towers left: " + (fireTowerLimit - fireTowers.Count) +
            "\nFrost Towers left: " + (frostTowerLimit - frostTowers.Count) +
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
        if (fireTowers.Count < fireTowerLimit)
        {
            var fireTowerClone = InstantiateNewTower(baseWaypoint, fireTowerPrefab);
            fireTowerClone.SetElement("Fire");
            fireTowers.Add(fireTowerClone);
        }
        else
        {
            Tower tower = fireTowers[0];
            fireTowers.RemoveAt(0);
            fireTowers.Add(MoveExistingTower(baseWaypoint, tower));
        }
    }

    public void AddFrostTower(Waypoint baseWaypoint)
    {
        if (frostTowers.Count < frostTowerLimit)
        {
            var frostTowerClone = InstantiateNewTower(baseWaypoint, frostTowerPrefab);
            frostTowerClone.SetElement("Frost");
            frostTowers.Insert(0, frostTowerClone);
        }
        else
        {
            Tower tower = frostTowers[0];
            frostTowers.RemoveAt(0);
            frostTowers.Add(MoveExistingTower(baseWaypoint, tower));
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

    private Tower MoveExistingTower(Waypoint newBaseWaypoint, Tower tower)
    {   
        Waypoint oldBaseWaypoint = tower.baseWaypoint;
        tower.baseWaypoint = newBaseWaypoint;

        oldBaseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        tower.transform.position = newBaseWaypoint.transform.position;

        return tower;
    }

    public void RemoveTower(Tower tower)
    {
        switch(tower.GetElement())
        {
            case "Fire":
                fireTowers.Remove(tower);
                break;

            case "Frost":
                frostTowers.Remove(tower);
                break;
        }

        Destroy(tower.gameObject);
    }
}
