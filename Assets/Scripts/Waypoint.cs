using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public bool discovered = false; //ok public as is a data class
    public Waypoint exploredFrom;
    public bool isPlaceable = true;

        [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towers;

    Vector2Int gridPos;

    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (isPlaceable)
            {
                print("Setting tower at: " + gameObject.name);
                var towerClone = Instantiate(towerPrefab, transform.position, Quaternion.identity);
                towerClone.transform.parent = towers;
                isPlaceable = false;
            }
            else
            {
                print("Can't place here");
            }
        }

    }

}
