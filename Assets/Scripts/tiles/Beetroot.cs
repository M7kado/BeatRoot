using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface Iinteractable
{
    void Interact();
}

public class Beetroot : Clockable, Iinteractable
{
    System.Random rand = new System.Random();
    // RNG
    int growRNGThreshold = 10;
    int waterRNGThreshold = 10;

    public enum State 
    { 
        EMPTY = 0,
        GROWING = 1, 
        GROWN = 2,
        ROTTEN =3
    }  
    int birthTick = -1; // time planted
    int waterTick = 0; // time watered

    // game variables
    [SerializeField] int growthTime;
    [SerializeField] int rotTime;

    private float percentageGrowing; 

    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite[] drySprites;
    [SerializeField] Sprite[] growingSprites;
    [SerializeField] Sprite[] dryGrowingSprites;
    [SerializeField] Sprite[] grownSprites;
    private int numberStatesGrowing;
    private int numberStatesGrown;


    private SpriteRenderer sr;

    State lastState = State.EMPTY;
    State currentState = State.EMPTY;

    new private void Start()
    {
        base.Start();
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
        numberStatesGrowing = growingSprites.Length;
        numberStatesGrown = grownSprites.Length;
    }

    // DEBUG
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { 
            birthTick = Clock.Instance.Timer; 
            waterTick = Clock.Instance.Timer;
            Debug.Log("KeyCode.P pressed"); }
    }

    public override void Action()
    {
        // some RNG
        // I think this one is creating obscure 
        if (rand.Next(101) < growRNGThreshold 
            && birthTick > -1 &&Clock.Instance.Timer - birthTick > 5) birthTick++;
        if (rand.Next(101) < waterRNGThreshold) waterTick++;

        currentState = GetState(Clock.Instance.Timer);
        RenderSprite();

        lastState = currentState;
    }

    bool isDry()
    {
        return GameManager.Instance.TimeToDry > 0 
            && Clock.Instance.Timer - waterTick > GameManager.Instance.TimeToDry;
    }
    public State GetState(int currentTick)
    {
        // if field is dry stall the growth cycle
        if (isDry() && birthTick >= 0)
            birthTick++;

        if (birthTick == -1) { return State.EMPTY; }
        if (currentTick - birthTick < growthTime) { return State.GROWING; }
        if (currentTick - birthTick <= rotTime + growthTime) { return State.GROWN; }
        return State.ROTTEN;
    }

    public void Interact()
    {
        AnimationPlayer();
        if (PlayerManager.Instance.Tool == Tools.ARROSOIR) {
            waterTick = Clock.Instance.Timer;
            return;
        }

        if (PlayerManager.Instance.Tool != Tools.SAC) return;
        if (birthTick == -1)
            birthTick = Clock.Instance.Timer;// -1; // -1 =obscure buxfix with RNG creating negative difference
        if (currentState == State.GROWN)
        {
            PlayerManager.Instance.StoredBeets++;
            PlayerManager.Instance.StoredBeets = 
                PlayerManager.Instance.StoredBeets >= PlayerManager.Instance.bagSize
                     ? PlayerManager.Instance.bagSize
                     : PlayerManager.Instance.StoredBeets;
        }
        if (currentState == State.GROWN || currentState == State.ROTTEN)
            birthTick = -1;
    }

    void AnimationPlayer()
    {
        PlayerManager.playerAnimator.SetTrigger("trigger move");
    }

    void RenderSprite()
    {
        if (currentState != lastState && currentState != State.GROWING && currentState != State.GROWN)
        {
            sr.sprite = isDry() ? drySprites[(int)currentState] : sprites[(int)currentState];
        }
        if (currentState == State.GROWING)
        {
            percentageGrowing = (Clock.Instance.Timer - birthTick) / (float) growthTime;
            sr.sprite = isDry() 
                        ? dryGrowingSprites[Mathf.FloorToInt(percentageGrowing * numberStatesGrowing)]
                        : growingSprites[Mathf.FloorToInt(percentageGrowing * numberStatesGrowing)];
        }
        if (currentState == State.GROWN)
        {
            sr.sprite = grownSprites[UnityEngine.Random.Range(0,numberStatesGrown)];
        }
    }

}
