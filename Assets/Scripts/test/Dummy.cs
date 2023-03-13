using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dummy : Clockable
{
    public TextMeshProUGUI text;

    bool color = true;

    public override void Action()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        color = !color;
        if (color) {
            sr.color = Color.white;
        } else {
            sr.color = Color.red;
        }
    }
}
