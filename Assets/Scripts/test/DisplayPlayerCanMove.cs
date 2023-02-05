using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerCanMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (Clock.Instance.playerCanMove) {
            sr.color = Color.green;
        } else {
            sr.color = Color.black;
        }
    }
}
