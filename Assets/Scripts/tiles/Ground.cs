using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Clockable, Iinteractable
{
    System.Random rand = new System.Random();
    // RNG
    int leavesRNGThreshold;
    
    private SpriteRenderer sr;
    [SerializeField] Sprite[] leaveSprites;
    int spriteCycle = 0;

    new private void Start()
    {
        base.Start();
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = leaveSprites[0];
        leavesRNGThreshold = GameManager.Instance.LeavesRNG;
    }
    
    public override void Action()
    {
        // some RNG
        if (rand.Next(301) < leavesRNGThreshold && spriteCycle == 0) 
        {
            Debug.Log("leave activaed");
            sr.enabled = true;
            spriteCycle = 1;
        }

        sr.sprite = leaveSprites[spriteCycle];
        if (spriteCycle > 0 && spriteCycle + 1 < leaveSprites.Length) spriteCycle++;
    }

    bool isWalkable()
    {
        return spriteCycle + 1 < leaveSprites.Length;
    }

    public void Interact()
    {
        AnimationPlayer();
        
        if (!isWalkable())
        {
            if (PlayerManager.Instance.Tool == Tools.RATEAU)
            {
                spriteCycle = 0;
                sr.enabled = false;
            }
        } else {
            PlayerManager.Instance.pos += PlayerManager.dirs[(int)Clock.Instance.playerAction];
        }
    }

    void AnimationPlayer()
    {
        PlayerManager.playerAnimator.SetTrigger("trigger move");
    }
}
