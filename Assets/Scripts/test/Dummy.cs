using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dummy : Clockable
{
    int counter = 0;
    public TextMeshProUGUI text;

    bool color = true;

    public override void Action()
    {
        counter++;
        //Debug.Log(counter);
        text.SetText(String.Format("{0}", counter));
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        color = !color;
        if (color) {
            sr.color = Color.white;
        } else {
            sr.color = Color.red;
        }
    }
}
