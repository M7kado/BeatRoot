using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    // Beat Slider
    [SerializeField] private Slider[] tempoSliders;
    [SerializeField] private Sprite beatSpritesOFF;
    [SerializeField] private Sprite beatSpritesON;
    [SerializeField] private GameObject beat;
    private Image beatImage;
    private float bpm;
    private float realTime = 0f;

    // Progression
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressTxt;
    public TextMeshProUGUI remainingTime;
    public TextMeshProUGUI beetrootTruckCounter;
    public TextMeshProUGUI beetrootBagCounter;

    public static UIManager Instance { get; private set; }

    void Awake()
    {
        bpm = Clock.Instance.bpm;
        beatImage = beat.GetComponent<Image>();
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

    private void Update()
    {
        // Beat
        realTime += Time.deltaTime;

        foreach (Slider slider in tempoSliders)
        {
            slider.value = realTime * bpm / 60;
        }
        if (realTime * bpm / 60 > 0.8)
            beatImage.sprite = beatSpritesON;
        else
            beatImage.sprite = beatSpritesOFF;
        if (realTime >= 60 / bpm)
            realTime = 0;

        // display
        remainingTime.SetText(String.Format("{0}", GameManager.Instance.timeRemaining));
        beetrootTruckCounter.SetText(String.Format("Collected : {0} / {1}", GameManager.Instance.BeetrootCollected, GameManager.Instance.BeetrootNeeded));
        beetrootBagCounter.SetText(String.Format("Holding : {0} / {1}", PlayerManager.Instance.StoredBeets, PlayerManager.Instance.bagSize));
    }
}
