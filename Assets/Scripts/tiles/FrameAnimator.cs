using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic script that shifts a sprite every frame
public class FrameAnimator : Clockable
{
    private SpriteRenderer sr;
    [SerializeField] Sprite[] sprites; 
    int count = 0;

    new private void Start()
    {
        base.Start();
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }

    public override void Action()
    {
        count = (++count) % sprites.Length;
        sr.sprite = sprites[count];
    }
}
