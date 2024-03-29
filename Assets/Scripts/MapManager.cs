using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapManager : MonoBehaviour
{
    public int mapWidth = 3;
    public int mapHeight = 3;

    public Vector2 playerSpawnPoint;
    public Vector2 renderOffset;

    // MUST MATCH PREFAB NAME
    public String mapName;
    public String[,] map;

    public String[,] map1= 
    {
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Tile", "Ground","Tile","Tile","Tile","Tile","Tile","Ground"},
    {"Tile", "Ground","Tile","Tile","Tile","Tile","Tile","Ground"},
    {"Tile","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Tile", "Ground","Tile","Tile","Tile","Tile","Tile","Ground"},
    {"Tile", "Ground","Tile","Tile","Tile","Tile","Tile","Ground"},
    {"Tile","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Tile", "Ground","Tile","Tile","Tile","Tile","Tile","Ground"},
    {"Tile", "Ground","Tile","Tile","Tile","Tile","Tile","Ground"},
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Wall","Wall","Wall","Ground","Ground","Wall","Wall","Wall",},
    {"Wall","Wall","Wall","Truck","Truck","Wall","Wall","Wall",},
    };

    public String[,] map2= 
    {
    {"Tile","Tile","Tile","Tile","Tile","Tile","Tile","Tile",},
    {"Tile", "Ground","Ground","Ground","Ground","Ground","Ground","Tile"},
    {"Tile", "Ground","Tile","Tile","Ground","Tile","Ground","Tile"},
    {"Tile", "Ground","Tile","Tile","Ground","Tile","Ground","Tile"},
    {"Tile", "Ground","Tile","Tile","Ground","Tile","Ground","Ground"},
    {"Tile", "Ground","Ground","Ground","Ground","Tile","Ground","Ground"},
    {"Tile", "Ground","Tile","Tile","Ground","Tile","Ground","Tile"},
    {"Tile", "Ground","Tile","Tile","Ground","Tile","Ground","Tile"},
    {"Tile", "Ground","Tile","Tile","Ground","Tile","Ground","Tile"},
    {"Tile", "Ground","Ground","Ground","Ground","Ground","Ground","Tile"},
    {"Wall","Wall","Wall","Ground","Ground","Wall","Wall","Wall",},
    {"Wall","Wall","Wall","Truck","Truck","Wall","Wall","Wall",},
    };

    public String[,] map3= 
    {
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Ground","Tile","Tile","Ground"},
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Wall","Wall","Wall","Ground","Ground","Wall","Wall","Wall",},
    {"Wall","Wall","Wall","Truck","Truck","Wall","Wall","Wall",},
    };

    public String[,] map4= 
    {
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Ground", "Tile","Tile","Ground","Tile","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Tile","Tile","Tile","Ground"},
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Ground", "Tile","Tile","Tile","Ground","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Tile","Ground","Tile","Tile","Ground"},
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Ground", "Tile","Tile","Ground","Tile","Tile","Tile","Ground"},
    {"Ground", "Tile","Tile","Ground","Tile","Tile","Tile","Ground"},
    {"Ground","Ground","Ground","Ground","Ground","Ground","Ground","Ground",},
    {"Wall","Wall","Wall","Ground","Ground","Wall","Wall","Wall",},
    {"Wall","Wall","Wall","Truck","Truck","Wall","Wall","Wall",},
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
        // SelectMap
        if (mapName.Equals("Level1")) map = map1;
        if (mapName.Equals("Level2")) map = map2;
        if (mapName.Equals("Level3")) map = map3;
        if (mapName.Equals("Level4")) map = map4;
        mapObjects = new Iinteractable[mapWidth, mapHeight];
        for(int i = 0; i< mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                GameObject tileObj = Instantiate(Resources.Load<GameObject>("Prefabs/" + map[i,j]));
                tileObj.transform.position = new Vector2(i, j) + renderOffset;
                tileObj.gameObject.SetActive(true);
                mapObjects[i,j] = tileObj.GetComponent<Iinteractable>();
            }
        }

    }

    void Start()
    {
        PlayerManager.Instance.pos = playerSpawnPoint;
    }

    public bool checkBorders(Vector2 pos)
    {
        return pos.x >= 0 &&
                pos.x < mapWidth &&
                pos.y >= 0 &&
                pos.y < mapHeight;
    }
}
