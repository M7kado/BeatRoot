using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Beat Slider
    [SerializeField] private Slider[] tempoSliders;
    [SerializeField] private Sprite beatSpritesOFF;
    [SerializeField] private Sprite beatSpritesON;
    [SerializeField] private GameObject beat;
    private Image beatImage;
    private float bpm;
    private float beatTime = 0f;

    // Progression
    [SerializeField] private TextMeshProUGUI remainingTime;
    [SerializeField] private TextMeshProUGUI beetrootTruckCounter;
    [SerializeField] private TextMeshProUGUI beetrootBagCounter;

    // Tools
    private PlayerManager player;

    [SerializeField] private Sprite[] icons;

    [SerializeField] private GameObject[] tools;

    private Image[] toolImages;

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

    private void Start()
    {
        bpm = Clock.Instance.bpm;
        player = PlayerManager.Instance;
        toolImages = new Image[tools.Length];
        beatImage = beat.GetComponent<Image>();
        for (int i= 0; i < tools.Length; i++)
        {
            toolImages[i] = tools[i].GetComponent<Image>();
        }
        for (int i = tools.Length - 1; i>= player.toolBeltSize; i--)
        {
            tools[i].SetActive(false);
        }
    }

    private void Update()
    {
        // Beat
        beatTime += Time.deltaTime;

        foreach (Slider slider in tempoSliders)
        {
            slider.value = beatTime * bpm / 60;
        }
        if (beatTime * bpm / 60 > 0.8)
            beatImage.sprite = beatSpritesON;
        else
            beatImage.sprite = beatSpritesOFF;
        if (beatTime >= 60 / bpm)
            beatTime = 0;

        // Display
        remainingTime.SetText(String.Format("{0}", GameManager.Instance.timeRemaining));
        beetrootTruckCounter.SetText(String.Format("Collected : {0} / {1}", GameManager.Instance.BeetrootCollected, GameManager.Instance.BeetrootNeeded));
        beetrootBagCounter.SetText(String.Format("Holding : {0} / {1}", PlayerManager.Instance.StoredBeets, PlayerManager.Instance.bagSize));

        // Tools (cf UpdateSprites)
        
    }

    public void UpdateSprites()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            toolImages[i].sprite = icons[i];
            toolImages[i].transform.localScale = Vector3.one;
        }
        int j = (int)player.Tool;
        toolImages[j].sprite = icons[j + 4];
        toolImages[j].transform.localScale *= 1.5f;
    }
}
