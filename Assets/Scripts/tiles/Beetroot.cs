using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    void Interact();
}

public class Beetroot : Clockable, Iinteractable
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
        //Debug.Log(GetState(Clock.Instance.Timer));
        //animator.setfloat(GetState(Clock.Instance.Timer))
    }

    public State GetState(int currentTick)
    {
        if (birthTick == -1) { return State.Empty; }
        if (currentTick - birthTick <= growTime) { return State.Growing; }
        if (currentTick - birthTick <= rotTime + growTime) { return State.Grown; }
        return State.Rotten;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        // if (birthTick == -1)
        //     birthTick = Clock.Instance.Timer;
        // if (state == State.Grown)
        //     player.StoredBeets++;
        // if (state == State.Grown || state == State.Rotten)
        //     birthTick = -1;
    }
}
