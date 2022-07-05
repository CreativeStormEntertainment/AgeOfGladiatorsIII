using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void BackToMenu()
    {
        GameMusic.instance.FadeOutMusic(1f);

        //SceneManager.LoadScene("Screen-Start");
        DestroyScenes();

        SceneManager.LoadScene("Master");
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    void DestroyScenes()
    {
        Master.instance.ResetMaster();
        UI.instance.ResetUI();
        PlayerScene.instance.ResetPlayer();
    }
}
