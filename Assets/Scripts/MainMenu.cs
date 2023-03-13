using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuContainer;

    [SerializeField]
    GameObject settingsContainer;

    [SerializeField]
    AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuContainer.SetActive(true);
        settingsContainer.SetActive(false);
    }

    public void Settings()
    {
        mainMenuContainer.SetActive(false);
        settingsContainer.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenuContainer.SetActive(true);
        settingsContainer.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    // Setting functions
    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        audioMixer.SetFloat("EffectsVolume", effectsVolume);
    }
}
