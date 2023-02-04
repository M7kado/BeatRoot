using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI remainingTime;
    public TextMeshProUGUI beetrootCounter;

    public static UIManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            Debug.Log("instance set" + Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime.SetText(String.Format("{0}", GameManager.Instance.timeRemaining));
        beetrootCounter.SetText(String.Format("{0} / {1}", PlayerManager.Instance.StoredBeets, GameManager.Instance.BeetrootNeeded));
        
    }
}
