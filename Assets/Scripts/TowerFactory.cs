using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour {

    [SerializeField] FriendlyBase friendlyBase;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] int fireTowerLimit = 5;
    [SerializeField] int frostTowerLimit = 5;
    [SerializeField] int waterTowerLimit = 5;
    [SerializeField] int thunderTowerLimit = 5;

    [SerializeField] Tower fireTowerPrefab;
    [SerializeField] Tower frostTowerPrefab;
    [SerializeField] Tower waterTowerPrefab;
    [SerializeField] Tower thunderTowerPrefab;
    [SerializeField] Transform towerParent;

    [SerializeField] Text towerCounterText;
    //[SerializeField] Text frostTowerCounterText;

    List<Tower> fireTowers = new List<Tower>();
    List<Tower> frostTowers = new List<Tower>();
    List<Tower> waterTowers = new List<Tower>();
    List<Tower> thunderTowers = new List<Tower>();

    //int fireTowersAmount;
    //int frostTowersAmount;

    int towerChosen;
    string towersInfo;

    private void Start()
    {
        towerChosen = 1;
        StartCoroutine(UpdateTowersInfo());
    }

    public void SetTowerLimits(Vector4 towerFactory)
    {
        fireTowerLimit = (int)towerFactory.x;
        frostTowerLimit = (int)towerFactory.y;
        waterTowerLimit = (int)towerFactory.z;
        thunderTowerLimit = (int)towerFactory.w;
    }

    IEnumerator UpdateTowersInfo()
    {
        while (true)
        {
            towerCounterText.text = towersInfo;
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        towersInfo = "Max Fire Towers: " + (fireTowerLimit);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            towerChosen = 1;
        }


        if (frostTowerLimit != 0)
        {
            towersInfo += "\nMax Frost Towers: " + (frostTowerLimit);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                towerChosen = 2;
            }
        }

        if (waterTowerLimit != 0)
        {
            towersInfo += "\nMax Water Towers: " + (waterTowerLimit);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                towerChosen = 3;
            }
        }

        if (thunderTowerLimit != 0)
        {
            towersInfo += "\nMax Thunder Towers: " + (thunderTowerLimit);
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                towerChosen = 4;
            }
        }

        towersInfo += "\n\nCurrently placing: ";

        switch (towerChosen)
        {
            case 1:
                towersInfo += "Fire tower";
                break;
            case 2:
                towersInfo += "Frost tower";
                break;
            case 3:
                towersInfo += "Water tower";
                break;
            case 4:
                towersInfo += "Thunder tower";
                break;
        }
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        if (!friendlyBase.gameOver && enemySpawner.allowPlacingTowers)
        {
            switch (towerChosen)
            {
                case 1:
                    AddFireTower(baseWaypoint);
                    break;
                case 2:
                    AddFrostTower(baseWaypoint);
                    break;
                case 3:
                    AddWaterTower(baseWaypoint);
                    break;
                case 4:
                    AddThunderTower(baseWaypoint);
                    break;
            }
        }
    }

    public void AddFireTower(Waypoint baseWaypoint) //Maybe create a scriptable object for elements?
    {
        if (fireTowers.Count < fireTowerLimit)
        {
            var fireTowerClone = InstantiateNewTower(baseWaypoint, fireTowerPrefab);
            fireTowerClone.type = Tower.Type.Fire;
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
            frostTowerClone.type = Tower.Type.Frost;
            frostTowers.Insert(0, frostTowerClone);
        }
        else
        {
            Tower tower = frostTowers[0];
            frostTowers.RemoveAt(0);
            frostTowers.Add(MoveExistingTower(baseWaypoint, tower));
        }
    }

    public void AddWaterTower(Waypoint baseWaypoint)
    {
        if (waterTowers.Count < waterTowerLimit)
        {
            var waterTowerClone = InstantiateNewTower(baseWaypoint, waterTowerPrefab);
            waterTowerClone.type = Tower.Type.Water;
            waterTowers.Insert(0, waterTowerClone);
        }
        else
        {
            Tower tower = waterTowers[0];
            waterTowers.RemoveAt(0);
            waterTowers.Add(MoveExistingTower(baseWaypoint, tower));
        }
    }

    public void AddThunderTower(Waypoint baseWaypoint)
    {
        if (thunderTowers.Count < thunderTowerLimit)
        {
            var thunderTowerClone = InstantiateNewTower(baseWaypoint, thunderTowerPrefab);
            thunderTowerClone.type = Tower.Type.Thunder;
            thunderTowers.Insert(0, thunderTowerClone);
        }
        else
        {
            Tower tower = thunderTowers[0];
            thunderTowers.RemoveAt(0);
            thunderTowers.Add(MoveExistingTower(baseWaypoint, tower));
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
        switch(tower.type)
        {
            case Tower.Type.Fire:
                fireTowers.Remove(tower);
                break;

            case Tower.Type.Frost:
                frostTowers.Remove(tower);
                break;

            case Tower.Type.Water:
                waterTowers.Remove(tower);
                break;

            case Tower.Type.Thunder:
                thunderTowers.Remove(tower);
                break;
        }

        Destroy(tower.gameObject);
    }
}
