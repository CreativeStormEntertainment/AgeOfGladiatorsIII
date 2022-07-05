using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dredd : MonoBehaviour
{
    public static Dredd instance;

    // new character stats stored here from creation screen
    public PlayerCharacter NewCharacterTemporary;

    public bool isLoading;
    public GameDifficulty difficulty = GameDifficulty.Easy;



    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        SceneManager.LoadScene("Screen-Start", LoadSceneMode.Additive); // loads once here and remains
        SceneManager.LoadScene("Screen-UI", LoadSceneMode.Additive); // load the disabled UI as DDOL for use later
    }



    public void ResetDredd()
    {
        instance = null;
        Destroy(gameObject);
    }
}
