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
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject loading in loadingScreens)
        {
            loading.SetActive(false);
        }
        loadingScreens[GameManager.Instance.currentLvl].SetActive(true);
        StartCoroutine(Loading(3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded && Input.anyKeyDown)
        {
            SceneManager.LoadScene(GameManager.Instance.currentLvl);
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
}
