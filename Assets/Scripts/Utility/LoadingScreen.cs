using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    public GameObject loadingCanvas;
    public Slider loadingBar;
    public float progress;
    public float fillRate;
    private float current;

    public int loadingIndex;
    public Image LoadingImage;
    public TextMeshProUGUI LoadingText;



    private void OnEnable()
    {
        if (instance == null)
            instance = this;

        Map.OnLoad += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (instance == this)
            instance = null;

        Map.OnLoad -= OnSceneLoaded;
    }

    public void ChooseLoadingScreen()
    {
        LoadingImage.sprite = UISprites.instance.LoadingImages[loadingIndex];

        LoadingImage.gameObject.SetActive(true);

        // assign tooltip
        switch (loadingIndex)
        {
            case 0:
                LoadingText.text = "\"Otto Sump's Gunge is taking Mega-City One by storm! Kilo for kilo, Gunge is twice as nutritious as Prime Munce - and tastier too! Does this mean Munce is soon to be dead meat?\"";
                break;
            case 1:
                LoadingText.text = "\"Uncle Ump's Umpty Candy is the confectionery hit in Mega-City One, quickly becoming known as \"the sweet that's too good to eat\". Although not a narcotic drug per se, it tastes so good that some say it's impossible to stop eating it once it's been tasted...\"";
                break;
            case 2:
                LoadingText.text = "\"I want to say one word to you. Just one word. Plasteen. There's a great future in plasteen. Think about it.\"";
                break;
        }

        loadingIndex++;

        if (loadingIndex >= UISprites.instance.LoadingImages.Count)
            loadingIndex = 0;
    }

    public void InitiateLoadingBar(AsyncOperation _async)
    {
        loadingCanvas.SetActive(true);

        ChooseLoadingScreen();

        StartCoroutine(Loading(_async));
    }

    IEnumerator Loading(AsyncOperation _async)
    {
        while (current != 1)
        {
            if (_async.progress < 0.9f)
                progress = _async.progress;
            else
                progress = 1;

            current = Mathf.MoveTowards(current, progress, fillRate);

            loadingBar.value = current;

            yield return null;
        }

        _async.allowSceneActivation = true;

        while (!_async.isDone)
        {
            yield return null;
        }
    }



    public void InitiateStartGameLoadingBar(AsyncOperation _async, string _name)
    {
        loadingCanvas.SetActive(true);

        ChooseLoadingScreen();

        StartCoroutine(StartGameLoading(_async, _name));
    }

    IEnumerator StartGameLoading(AsyncOperation _async, string _name)
    {
        while (current != 0.5)
        {
            if (_async.progress < 0.9f)
                progress = _async.progress / 2f;
            else if (progress != 0.5f)
            {
                progress = 0.5f;
            }


            current = Mathf.MoveTowards(current, progress, fillRate / 5);

            loadingBar.value = current;

            yield return null;
        }

        _async.allowSceneActivation = true;

        AsyncOperation _async2 = SceneManager.LoadSceneAsync(_name);
        _async2.allowSceneActivation = false;

        while (current != 1)
        {
            if (_async2.progress < 0.9f)
                progress = 0.5f + _async2.progress / 2f;
            else
                progress = 1f;

            current = Mathf.MoveTowards(current, progress, fillRate);

            loadingBar.value = current;

            yield return null;
        }

        _async2.allowSceneActivation = true;

        while (!_async.isDone && !_async2.isDone)
        {
            yield return null;
        }

        UI.instance.ActivateUI(true);
        UI.instance.ResetCharacter();

    }

    private void ResetLoadingScreen()
    {
        Dredd.instance.isLoading = false;

        LoadingImage.gameObject.SetActive(false);

        UI.instance.FadeScreen.gameObject.SetActive(true);
        UI.instance.FadeScreen.StartCoroutine(UI.instance.FadeScreen.ActivateFade());

        loadingCanvas.SetActive(false);
        progress = 0;
        current = 0;
        loadingBar.value = 0;
    }

    private void OnSceneLoaded(bool loaded)
    {
        StartCoroutine(FinishLoading());
    }

    IEnumerator FinishLoading()
    {
        yield return new WaitForSeconds(1.0f);

        ResetLoadingScreen();
    }
}
