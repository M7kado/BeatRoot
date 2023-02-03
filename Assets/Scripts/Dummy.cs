using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Clockable
{
    int counter = 0;

    public override void Action()
    {
        counter++;
        Debug.Log(counter);
    }
}
