using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitch
{
    public static int entryNode;
    public static int travelMapNode;



    public static void SceneSwitcher(string _name, int _nodeNumber)
    {
        // determine which node to spawn character on
        entryNode = _nodeNumber;

        // load the map scene
        SceneManager.LoadScene(_name);

        // load UI (additive)
        SceneManager.LoadScene("Screen-UI", LoadSceneMode.Additive);
    }



    public static void SceneSwitcherAsync(string _name, int _nodeNumber)
    {
        Master.instance.isLoading = true;

        entryNode = _nodeNumber;

        AsyncLoading(_name);
    }

    public static void SceneSwitcherAsync(string _name)
    {
        Master.instance.isLoading = true;

        AsyncLoading(_name);
    }

    public static void AsyncLoading(string _name)
    {
        AsyncOperation _async = SceneManager.LoadSceneAsync(_name);
        _async.allowSceneActivation = false;
        LoadingScreen.instance.InitiateLoadingBar(_async);
    }

    public static void LoadUIAdditive()
    {
        SceneManager.LoadScene("Screen-UI", LoadSceneMode.Additive);
    }

    public static void SceneSwitcherStartGameAsync(string _name, int _nodeNumber)
    {
        Master.instance.isLoading = true;

        entryNode = _nodeNumber;

        AsyncOperation _async = SceneManager.LoadSceneAsync("Player");
        _async.allowSceneActivation = false;
        LoadingScreen.instance.InitiateStartGameLoadingBar(_async, _name);
    }
}
