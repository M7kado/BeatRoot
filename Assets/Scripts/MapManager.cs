using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Position
{
    public int x, y;


    public Position(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public Position(int[] xy)
    {
        this.x = xy[0];
        this.y = xy[1];
    }

    public static Position operator +(Position a, Position b)
        => new Position(a.x + b.x, a.y + b.y);

    public bool checkBorders(Position dir)
    {
        return x + dir.x >= 0 &&
                x + dir.x < MapManager.Instance.mapWidth &&
                y + dir.y >= 0 &&
                y + dir.y < MapManager.Instance.mapHeight;
    }
}

public class MapManager : MonoBehaviour
{
    public int mapWidth = 3;
    public int mapHeight = 3;

    // MUST MATCH PREFAB NAME
    public String[,] map= 
    {
    {"Ground","Tile", "Ground","Tile", "Ground"},
    {"Ground","Tile", "Ground","Tile", "Ground"},
    {"Ground","Tile", "Ground","Tile", "Ground"},
    {"Ground","Tile", "Ground","Tile", "Ground"},
    {"Ground","Ground", "Ground","Ground", "Ground"},
    {"Ground","Ground", "Ground","Ground", "Truck"},
    {"Ground","Tile", "Tile","Ground", "Truck"},
    {"Ground","Tile", "Tile","Ground", "Ground"},
    };

    public Iinteractable[,] mapObjects;

    public static MapManager Instance { get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            Debug.Log("instance set" + Instance);
        }
        // Creating the mapObjects
        mapObjects = new Iinteractable[mapWidth, mapHeight];
        for(int i = 0; i< mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                GameObject tileObj = Instantiate(Resources.Load<GameObject>("Prefabs/" + map[i,j]));
                tileObj.transform.position = new Vector3(i, j, 0);
                tileObj.gameObject.SetActive(true);
                mapObjects[i,j] = tileObj.GetComponent<Iinteractable>();
            }
        }
    }
}
