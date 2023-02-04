using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Tile
{
    FIELD,
    GROUND
}
public class MapManager : MonoBehaviour
{

    public Tile[,] map= 
    {
    {Tile.FIELD,Tile.FIELD,Tile.FIELD},
    {Tile.FIELD,Tile.GROUND,Tile.FIELD},
    {Tile.FIELD,Tile.FIELD,Tile.FIELD}
    };

    public static MapManager Instance { get; private set; }


    // Start is called before the first frame update
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            Debug.Log("instance set" + Instance);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
