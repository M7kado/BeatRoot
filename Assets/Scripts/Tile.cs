using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Clockable
{
    public enum State 
    { 
        EMPTY = 0,
        GROWING = 1, 
        GROWN = 2,
        ROTTEN =3
    }  
    State state = new();
    int birthTick = -1;
    [SerializeField] int growthTime;
    [SerializeField] int rotTime;

    private float percentageGrowing; 

    [SerializeField] Sprite[] sprites; 
    [SerializeField] Sprite[] growingSprites;
    private int numberStatesGrowing;

    private SpriteRenderer sr;

    State lastState = State.EMPTY;
    State currentState = State.EMPTY;

    new private void Start()
    {
        base.Start();
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
        numberStatesGrowing = growingSprites.Length;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { birthTick = Clock.Instance.Timer; Debug.Log("KeyCode.P pressed"); }
    }

    public override void Action()
    {
        //Debug.Log(GetState(Clock.Instance.Timer));
        //animator.setfloat(GetState(Clock.Instance.Timer))
        currentState = GetState(Clock.Instance.Timer);
        RenderSprite();
    
        lastState = currentState;
    }

    public State GetState(int currentTick)
    {
        if (birthTick == -1) { return State.EMPTY; }
        if (currentTick - birthTick < growthTime) { return State.GROWING; }
        if (currentTick - birthTick <= rotTime + growthTime) { return State.GROWN; }
        return State.ROTTEN;
    }

    void RenderSprite()
    {
        if (currentState != lastState && currentState != State.GROWING)
        {
            sr.sprite = sprites[(int)currentState];
        }
        if (currentState == State.GROWING)
        {
            percentageGrowing = (Clock.Instance.Timer - birthTick) / (float) growthTime;
            Debug.Log("percentageGrowing = "+percentageGrowing);
            sr.sprite = growingSprites[Mathf.FloorToInt(percentageGrowing * numberStatesGrowing)];
        }

    }

}
