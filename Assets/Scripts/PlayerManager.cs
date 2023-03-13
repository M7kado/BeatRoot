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
    BECHE,
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
    
    public Vector2 initialPos;
    float wantedTimeMove=0.2f;
    float currentTimeMove = 0f;

    public int stunedFrame = -999;
    int stunDuration = 4;

    public static Animator playerAnimator { get; set; }

    private bool inputAccepted;

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
        playerAnimator = this.GetComponentInChildren<Animator>();
        //initialPos = transform.position + (Vector3)MapManager.Instance.renderOffset; // FIXME
        initialPos = pos + MapManager.Instance.renderOffset; // FIXME
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
            playerAnimator.SetInteger("int direction", (int)Clock.Instance.playerAction);
        }
        else
        {
            playerAnimator.SetTrigger("trigger inputNotAccepted");
        }

        // render position in scene
        currentTimeMove += Time.deltaTime;
        transform.position = Vector2.Lerp(initialPos, new Vector2(
            MapManager.Instance.renderOffset.x + pos.x,
            MapManager.Instance.renderOffset.y + pos.y), 
            currentTimeMove/wantedTimeMove); //FINISH IT

        playerAnimator.SetInteger("int tool", (int)Tool);
    }

    bool Inputs()
    {
        if (Clock.Instance.playerCanMove
            && Clock.Instance.playerAction == PlayerActions.NONE
            && stunedFrame + stunDuration < Clock.Instance.Timer)
        {
            currentTimeMove = 0;
            initialPos = pos + MapManager.Instance.renderOffset;
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
        
        // water propagation
        if (Tool == Tools.ARROSOIR 
            && MapManager.Instance.mapObjects[(int)(pos.x + dir.x), (int)(pos.y + dir.y)] is Beetroot)
            PropagateWater(pos+dir);

        // we do water propagation before ground otherwise we get out of bound on borders            
        MapManager.Instance.mapObjects[(int)(pos.x + dir.x), (int)(pos.y + dir.y)].Interact();

    }

    void AnimationTool()
    {
        Debug.Log("player action : " + Clock.Instance.playerAction);
    }

    void PropagateWater(Vector2 pos)
    {
        foreach (var dir in dirs)
        {
            if (MapManager.Instance.checkBorders(pos + dir)
                && MapManager.Instance.mapObjects[(int)(pos.x + dir.x), (int)(pos.y + dir.y)] is Beetroot)
            MapManager.Instance.mapObjects[(int)(pos.x + dir.x), (int)(pos.y + dir.y)].Interact();
        }
    }
}
