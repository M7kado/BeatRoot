using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Clockable
{
    public enum State { Empty, Growing, Grown, Rotten}
    State state = new();
    int birthTick = -1;
    [SerializeField] int growTime;
    [SerializeField] int rotTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { birthTick = Clock.Instance.Timer; }
    }

    public override void Action()
    {
        Debug.Log(GetState(Clock.Instance.Timer));
        //animator.setfloat(GetState(Clock.Instance.Timer))
    }

    public State GetState(int currentTick)
    {
        if (birthTick == -1) { return State.Empty; }
        if (currentTick - birthTick <= growTime) { return State.Growing; }
        if (currentTick - birthTick <= rotTime + growTime) { return State.Grown; }
        return State.Rotten;
    }
}
