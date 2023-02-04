using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TimeInSeconds = 300;
    public int BeetrootNeeded = 50;
    float timePassed = 0;
    public int timeRemaining { get => (int)(TimeInSeconds - timePassed); }
    public static GameManager Instance;


    // Start is called before the first frame update
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
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
    }
}
