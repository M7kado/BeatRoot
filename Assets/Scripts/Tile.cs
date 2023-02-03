using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Clockable
{
    enum State { Empty, Seed, Growing, Grown, Rotten}
    State state = new();
    int birthTick;
    int nextStateTick;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Action()
    {
        Clock.Instance.
        switch (state)
        {
            case State.Empty:
                break;
            case State.Rotten:
                break;
            default:
                state += 1;
                break;
        }
        Debug.Log(state);
    }
}
