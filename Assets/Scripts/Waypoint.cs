using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public bool discovered = false; //ok public as is a data class
    public bool isPlaceable = true;
    public bool isPathBlock = false;

    public Waypoint exploredFrom;

    [SerializeField] Renderer renderer;
 
    [SerializeField] Transform towers;

    public Tower towerClone;

    Vector2Int gridPos;

    const int gridSize = 10;


    public void OffsetMaterial()
    {
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0.45f, 0f));
    }

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
        print("there");

        if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("Can't place here");
            }
        }
        //else if(Input.GetMouseButtonDown(1)) //right click
        //{
        //    if (isPlaceable)
        //    {
        //        FindObjectOfType<TowerFactory>().RemoveTower(this);
        //    }
        //    else
        //    {
        //        print("Can't place here");
        //    }
        //}
    }
}
