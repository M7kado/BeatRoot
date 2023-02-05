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
    TRUELLE
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int bagSize;
    public int StoredBeets { get; set; }
    public Tools Tool { get; private set; }
    public Position pos = new Position(0, 0);

    public Vector2 initialPos;
    float wantedTimeMove=0.2f;
    float currentTimeMove = 0f;
    public static Animator playerAnimator { get; set; }

    private bool inputAccepted;

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
        playerAnimator = this.GetComponentInChildren<Animator>();
        Tool = Tools.SAC;
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch Tool
        if (Input.GetKeyDown(KeyCode.J))
        {
            Tool = Tools.SAC;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Tool = Tools.ARROSOIR;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Tool = Tools.RATEAU;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Tool = Tools.TRUELLE;
        }

        if (Inputs())
        {
            Action();
            playerAnimator.SetInteger("int direction", (int)Clock.Instance.playerAction);
        }
        else
        {
            playerAnimator.SetTrigger("trigger inputNotAccepted");
        }

        // render position in scene
        currentTimeMove += Time.deltaTime;
        transform.position = Vector2.Lerp(initialPos, new Vector2(pos.x,pos.y), currentTimeMove/wantedTimeMove); //FINISH IT

        //transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        playerAnimator.SetInteger("int tool", (int)Tool);
    }

    bool Inputs()
    {
        if (Clock.Instance.playerCanMove && Clock.Instance.playerAction == PlayerActions.NONE)
        {
            currentTimeMove = 0;
            initialPos = new Vector2(pos.x, pos.y);
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

    void AnimationTool()
    {
        Debug.Log("player action : " + Clock.Instance.playerAction);
    }

}