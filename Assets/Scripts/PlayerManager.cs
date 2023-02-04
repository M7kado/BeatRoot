using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerActions {
    UP = 0,
    DOWN = 1,
    LEFT = 2,
    RIGHT = 3,
    NONE
}

public enum Tools
{
    BECHE,
    SAC,
    ARROSOIR
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public int bagSize;
    public int StoredBeets { get; set; }
    public Tools Tool { get; private set; }
    public Position pos = new Position(0, 0);

    public static Position[] dirs = {
        new Position(0, 1),
        new Position(0, -1),
        new Position(-1, 0),
        new Position(1, 0)
    };

    public static PlayerManager Instance { get; private set; }

    void Awake() 
    {
        if (Instance != null && Instance != this) 
        {
            Debug.Log("Destroying");
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            Debug.Log("instance set" + Instance);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        Tool = Tools.BECHE;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch Tool
        if (Input.GetKeyDown(KeyCode.J))
        {
            Tool = Tools.BECHE;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Tool = Tools.SAC;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Tool = Tools.ARROSOIR;
        }

        if (Inputs())
        {
            Action();
        }
        
        // render position in scene
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
  
    bool Inputs()
    {
        if (Clock.Instance.playerCanMove && Clock.Instance.playerAction == PlayerActions.NONE)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Clock.Instance.playerAction = PlayerActions.UP;
                return true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Clock.Instance.playerAction = PlayerActions.LEFT;
                return true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Clock.Instance.playerAction = PlayerActions.DOWN;
                return true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Clock.Instance.playerAction = PlayerActions.RIGHT;
                return true;
            }
        }
        return false;
    }

    void Action()
    {
        Position dir = dirs[(int)Clock.Instance.playerAction];
        
        if(!pos.checkBorders(dir))
            return;
        
        // if (pos.getTileType(dir) == TileType.GROUND)
        // {
        //     pos += dir;
        //     // change player on screen
        // } else { // tile is field
        //     // interact
        //     MapManager.Instance.mapObjects[pos.x + dir.x, pos.y + dir.y].Interact();
        // }

        MapManager.Instance.mapObjects[pos.x + dir.x, pos.y + dir.y].Interact();
    }
}