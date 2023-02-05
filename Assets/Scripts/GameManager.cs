using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLvl = 0;
    public int TimeInSeconds = 300;
    public int BeetrootNeeded = 50;
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
        // Dev Tool
        if (Input.GetKeyDown(KeyCode.B))
        {
            BeetrootCollected += 10;
        }
        timePassed += Time.deltaTime;
        if (BeetrootCollected >= BeetrootNeeded)
        {
            currentLvl += 1;
            SceneManager.LoadScene("LoadingScreen");
        }
        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
