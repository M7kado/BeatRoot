using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    bool loaded;
    [SerializeField] private Slider loadBar;
    [SerializeField] private TMP_Text txt;
    [SerializeField] private GameObject[] loadingScreens;
    private int nextLvl;
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (GameObject loading in loadingScreens)
        {
            loading.SetActive(false);
        }
        Debug.Log("Loading game manager : " + GameManager.Instance.currentLvl);
        if (GameManager.Instance.currentLvl == 0)
        {
            nextLvl = 1;
            loadingScreens[0].SetActive(true);
        }
        else
        {
            nextLvl = GameManager.Instance.currentLvl + 1;
            loadingScreens[GameManager.Instance.currentLvl].SetActive(true);
        }
        if (nextLvl <= 4)
            StartCoroutine(Loading(1f));
        else
            loaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded && Input.anyKeyDown)
        {
            SceneManager.LoadScene("Lvl"+(nextLvl));
        }
    }

    private IEnumerator Loading(float time)
    {
        txt.text = "Loading";
        loaded = false;
        for (float i = 0; i <= time; i += Time.deltaTime)
        {
            loadBar.value = i / time;
            yield return null;
        }
        txt.text = "Press any key to continue";
        loaded = true;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
