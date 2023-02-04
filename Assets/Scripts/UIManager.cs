using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    // Start is called before the first frame update
    void Start()
    {
        bpm = Clock.Instance.bpm;
        beatImage = beat.GetComponent<Image>();
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

        // Progression
        
    }
}
