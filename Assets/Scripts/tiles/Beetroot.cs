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
    public enum State 
    { 
        EMPTY = 0,
        GROWING = 1, 
        GROWN = 2,
        ROTTEN =3
    }  
    int birthTick = -1;
    [SerializeField] int growthTime;
    [SerializeField] int rotTime;

    [SerializeField] GameObject iconOverField;
    private SpriteRenderer srIcon;
    [SerializeField] Sprite[] IconsTools;


    private float percentageGrowing; 

    [SerializeField] Sprite[] sprites; 
    [SerializeField] Sprite[] growingSprites;
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
        iconOverField = Instantiate(Resources.Load<GameObject>("Prefabs/IconOverField"));
        srIcon = iconOverField.GetComponent<SpriteRenderer>();
    }

    // DEBUG
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { birthTick = Clock.Instance.Timer; Debug.Log("KeyCode.P pressed"); }
    }

    public override void Action()
    {
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

    public void Interact()
    {
        AnimationPlayer();
        StartCoroutine(ControlIconOverField());
        if (birthTick == -1)
            birthTick = Clock.Instance.Timer;
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
            sr.sprite = sprites[(int)currentState];
        }
        if (currentState == State.GROWING)
        {
            percentageGrowing = (Clock.Instance.Timer - birthTick) / (float) growthTime;
            sr.sprite = growingSprites[Mathf.FloorToInt(percentageGrowing * numberStatesGrowing)];
        }
        if (currentState == State.GROWN)
        {
            sr.sprite = grownSprites[UnityEngine.Random.Range(0,numberStatesGrown)];
        }
    }

    IEnumerator ControlIconOverField()
    {
        iconOverField.transform.position = (Vector2)this.transform.position + Vector2.up *0.75f;
        srIcon.sprite = IconsTools[(int)PlayerManager.Instance.Tool];
        srIcon.enabled = true;
        yield return new WaitForSeconds(0.25f);
        srIcon.enabled = false;

    }
}
