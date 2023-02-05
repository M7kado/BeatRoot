using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // game variables
    public int TimeInSeconds = 300;
    public int BeetrootNeeded = 50;
    public int TimeToDry = -1; // -1 / 35 / 70 / -1
    
    public int BeetrootCollected = 0;
    float timePassed = 0;
    public int timeRemaining { get => (int)(TimeInSeconds - timePassed); }
    public static GameManager Instance;

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
    void Start()
    {
        timePassed = 0;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        // TODO condition de victoire
        // TODO condition de défaite
    }
}
