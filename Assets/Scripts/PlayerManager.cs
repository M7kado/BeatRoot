using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerActions {
    UP = 0,
    DOWN = 1,
    LEFT = 2,
    RIGHT = 3,
    NONE
}

public class PlayerManager : MonoBehaviour
{

    public Position pos = new Position(0, 0);

    int[,] dirs = {
        {0, 1},
        {0, -1},
        {-1, 0},
        {1, 0},
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Inputs())
        {
            Action();
        }
        
        // render position in scene
        transform.position = new Vector3(pos.x, pos.y, 0);
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
        Position dir = new Position(
                        dirs[(int)Clock.Instance.playerAction, 0],
                        dirs[(int)Clock.Instance.playerAction, 1]);
        
        if(!pos.checkBorders(dir))
            return;
        
        if (true)// (pos.getTileType(dir) == Tile.GROUND)
        {
            pos += dir;
            // change player on screen
        } else { // tile is field
            // interact
        }

    }
}