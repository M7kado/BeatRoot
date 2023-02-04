using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Clockable
{
    int counter = 0;
    bool color = true;
    public override void Action()
    {
        counter++;
        Debug.Log(counter);
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        color = !color;
        if (color) {
            sr.color = Color.white;
        } else {
            sr.color = Color.red;
        }
    }
}
