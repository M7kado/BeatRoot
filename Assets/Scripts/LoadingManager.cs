using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    bool loaded;
    private Slider loadBar;
    private TMP_Text txt;
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject[] loadingScreens;
    // Start is called before the first frame update
    void Start()
    {
        loadBar = loading.GetComponent<Slider>();
        txt = loading.GetComponent<TMP_Text>();
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
        loading.GetComponent<TMP_Text>().text = "Loading";
        loaded = false;
        yield return new WaitForSeconds(time);
        loading.GetComponent<TMP_Text>().text = "Press any key to continue";
        loaded = true;
    }
}
