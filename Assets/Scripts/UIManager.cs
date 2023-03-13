using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Menus
    [SerializeField] private GameObject pauseContainer;
    [SerializeField] private GameObject settingsContainer;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource music;
    private bool paused;

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
    [SerializeField] private Sprite[] icons;

    [SerializeField] private Image[] toolImages;

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
        /*pauseContainer.SetActive(false);
        settingsContainer.SetActive(false);*/
        bpm = Clock.Instance.bpm;
        beatImage = beat.GetComponent<Image>();
        for (int i = toolImages.Length - 1; i>= PlayerManager.Instance.toolBeltSize; i--)
        {
            Debug.Log("Startui : " + i);
            toolImages[i].gameObject.SetActive(false);
        }

        Debug.Log("Music : " + music.isPlaying);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Unpause();
            else
                Pause();
        }
        // Beat
        beatTime += Time.deltaTime;

        foreach (Slider slider in tempoSliders)
        {
            slider.value = beatTime * bpm / 60;
        }
        if (Clock.Instance.playerCanMove)
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
        for (int i = 0; i < toolImages.Length; i++)
        {
            toolImages[i].sprite = icons[i];
            toolImages[i].transform.localScale = Vector3.one;
        }
        int j = (int)PlayerManager.Instance.Tool;
        toolImages[j].sprite = icons[j + 4];
        toolImages[j].transform.localScale *= 1.5f;
    }

    // Menus

    public void Pause()
    {
        Time.timeScale = 0;
        music.Pause();
        pauseContainer.SetActive(true);
        paused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        music.UnPause();
        pauseContainer.SetActive(false);
        settingsContainer.SetActive(false);
        paused = false;
    }

    public void Settings()
    {
        settingsContainer.SetActive(!settingsContainer.activeSelf);
        pauseContainer.SetActive(!pauseContainer.activeSelf);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        audioMixer.SetFloat("EffectsVolume", effectsVolume);
    }
}
