using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    public static VolumeChanger instance;

    public AudioSource GameAudio;
    public AudioSource MusicAudio;
    public AudioSource MusicCuesAudio;
    public AudioSource MapsAudio;
    public AudioSource CombatAudio;
    public AudioSource UIAudio;

    public AudioListener Listener;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Listener.enabled = true;
    }

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("masterVolume", 1f));

        SetGameVolume(PlayerPrefs.GetFloat("gameSoundVolume", 1f));

        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 1f));
        SetMusicCueVolume(PlayerPrefs.GetFloat("musicCuesVolume", 0.7f));

        SetUIVolume(PlayerPrefs.GetFloat("uiVolume", 1f));
    }

    private void Update()
    {
        CheckMultipleListeners();
    }



    // set master volume
    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("masterVolume", vol);
    }



    // set game volume
    public void SetGameVolume(float vol)
    {
        PlayerPrefs.SetFloat("gameSoundVolume", vol);

        GameAudio.volume = PlayerPrefs.GetFloat("gameSoundVolume");
        MapsAudio.volume = PlayerPrefs.GetFloat("gameSoundVolume");
        CombatAudio.volume = PlayerPrefs.GetFloat("gameSoundVolume");
    }

    // set music volume
    public void SetMusicVolume(float vol)
    {
        PlayerPrefs.SetFloat("musicVolume", vol);
        MusicAudio.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    // set music cues volume
    public void SetMusicCueVolume(float vol)
    {
        PlayerPrefs.SetFloat("musicCuesVolume", vol);

        MusicCuesAudio.volume = PlayerPrefs.GetFloat("musicCuesVolume");
    }

    // set ui volume
    public void SetUIVolume(float vol)
    {
        PlayerPrefs.SetFloat("uiVolume", vol);

       UIAudio.volume = PlayerPrefs.GetFloat("uiVolume");
    }

    // check if multiple listeners
    public void CheckMultipleListeners()
    {
        if (Listener == null)
            return;

        AudioListener[] aL = FindObjectsOfType<AudioListener>();

        for (int i = 0; i < aL.Length; i++)
        {
            if (i > 1)
                Listener.enabled = false;
        }
    }
}