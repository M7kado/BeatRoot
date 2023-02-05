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
    SAC,
    ARROSOIR,
    RATEAU,
    BECHE
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public int bagSize;
    [SerializeField] public int toolBeltSize;
    public int StoredBeets { get; set; }
    public Tools Tool { get; private set; }
    public Vector2 pos = new Vector2(0, 0);

    public static Vector2[] dirs = {
        new Vector2(0, 1),
        new Vector2(0, -1),
        new Vector2(-1, 0),
        new Vector2(1, 0)
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

    void Start()
    {
        Tool = Tools.SAC;
        UIManager.Instance.UpdateSprites();
    }

    void Update()
    {
        // Switch Tool
        if (Input.GetKeyDown(KeyCode.K))
        {
            Tool = (Tools)(((int)Tool - 1 + toolBeltSize) % toolBeltSize) ;
            UIManager.Instance.UpdateSprites();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Tool = (Tools)(((int)Tool + 1) % toolBeltSize);
            UIManager.Instance.UpdateSprites();
        }

        if (Inputs())
        {
            Action();
        }
        
        // render position in scene
        transform.position = new Vector3(MapManager.Instance.renderOffset.x + pos.x,
                                    MapManager.Instance.renderOffset.y + pos.y, transform.position.z);
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
        Vector2 dir = dirs[(int)Clock.Instance.playerAction];
        
        if(!MapManager.Instance.checkBorders(pos + dir))
            return;
        
        MapManager.Instance.mapObjects[(int)(pos.x + dir.x), (int)(pos.y + dir.y)].Interact();
    }
}