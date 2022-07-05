using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public static StartScreen instance;

    [Header("Fade Screen")]
    public FadeBlack FadeScreen;

    [Header("New Game Screen")]
    public NewGameWindow NewGameScreen;

    [Header("Buttons")]
    public Button NewGameButton;
    public Button ContinueButton;
    public Button QuitGameButton;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        GameMusic.instance.FadeInMusic(2f);
        GameMusic.instance.PlayMenuMusic();

        FadeScreen.gameObject.SetActive(true);
        FadeScreen.StartCoroutine(FadeScreen.ActivateFade());

        NewGameScreen.gameObject.SetActive(false);

        NewGameButton.interactable = true;
        ContinueButton.interactable = true;
        QuitGameButton.interactable = true;
    }



    // character creation
    public void CharacterCreation()
    {
        NewGameScreen.Populate();
        NewGameScreen.gameObject.SetActive(true);
    }



    // start new game (from character creation)
    public void StartNewGame()
    {
        GameMusic.instance.FadeOutMusic(1f); // change to 2f for final

        NewGameButton.interactable = false;
        ContinueButton.interactable = false;
        QuitGameButton.interactable = false;

        StartCoroutine(StartAfterFade());
    }

    // start after audio fade
    public IEnumerator StartAfterFade()
    {
        yield return new WaitForSeconds(1f); // change to 2f for final

        LoadScenes();
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }



    // continue game
    public void ContinueGame()
    {
        GameMusic.instance.FadeOutMusic(1f);

        NewGameButton.interactable = false;
        ContinueButton.interactable = false;
        QuitGameButton.interactable = false;

        StartCoroutine(ContinueAfterFade());
    }

    // continue after audio fade
    public IEnumerator ContinueAfterFade()
    {
        yield return new WaitForSeconds(1f);

        LoadScenes();

        Dredd.instance.NewCharacterTemporary.PremadeCharacter(); // TEMPORARY(!!!!)
    }



    // load scenes
    void LoadScenes()
    {
        SceneSwitch.SceneSwitcherStartGameAsync("Map-Street", 0); // loads player party first then first location

        GameMusic.instance.FadeInMusic(1f); // using custom here for now due to quicker load time of scene for testing
        GameMusic.instance.PlayAmbientMusic(); // using custom here for now due to quicker load time of scene for testing
    }
}
