using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // back to menu
    public void BackToMenu()
    {
        GameMusic.instance.FadeOutMusic(1f);

        //SceneManager.LoadScene("Screen-Start");
        DestroyScenes();

        SceneManager.LoadScene("Dredd");
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }



    // destroy open scenes
    void DestroyScenes()
    {
        Dredd.instance.ResetDredd();
        UI.instance.ResetUI();
        PlayerScene.instance.ResetPlayer();
    }
}
