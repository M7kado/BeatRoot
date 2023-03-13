using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DisplayTime : MonoBehaviour
{

    public TextMeshProUGUI text;
    float time = 0;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        text.SetText(String.Format("{0}", time));
    }
}
