using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    int x, y;

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

    }
  
    bool Inputs()
    {
        if (Clock.Instance.playerCanMove && Clock.Instance.playerAction == KeyCode.Escape)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Clock.Instance.playerAction = KeyCode.Z;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Clock.Instance.playerAction = KeyCode.Q;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Clock.Instance.playerAction = KeyCode.S;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Clock.Instance.playerAction = KeyCode.D;
            }
            return true;
        }
        return false;
    }

    void Action()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(MapManager.Instance.map[x,y-1]==Tile.GROUND)
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }
}