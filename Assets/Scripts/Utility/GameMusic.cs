using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public static GameMusic instance;

    public static AudioSource Audio;

    [Header("Menu Music")]
    public List<AudioClip> MenuMusic = new List<AudioClip>();

    [Header("Game Music")]
    public List<AudioClip> AmbientMusic = new List<AudioClip>();

    [Header("Event Music")]
    public List<AudioClip> EventMusic = new List<AudioClip>();

    [Header("Combat Music")]
    public List<AudioClip> CombatMusic = new List<AudioClip>();


    bool playAmbientMusic;
    int lastTrack = -1;
    int randomTrack;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!playAmbientMusic)
            return;

        if (!Audio.isPlaying)
            ChooseAndPlayAmbientTrack();
    }



    public void PlayMenuMusic()
    {
        Audio.loop = false;
        Audio.Stop();
        playAmbientMusic = false;
        Audio.clip = MenuMusic[0];
        Audio.Play();
    }

    public void PlayAmbientMusic()
    {
        Audio.loop = false;
        Audio.Stop();
        playAmbientMusic = true;
    }

    public void PlayEventMusic()
    {
        Audio.loop = true;
        Audio.Stop();

        Audio.clip = EventMusic[Random.Range(0, EventMusic.Count)];
        Audio.Play();
    }

    public void PlayCombatMusic()
    {
        Audio.loop = true;
        Audio.Stop();

        //if (PlayerPrefs.GetInt("combatMusic") == 1)
        //{
        //    Audio.clip = CombatMusic[Random.Range(0, CombatMusic.Count)];
        //    Audio.Play();
        //}

        Audio.clip = CombatMusic[Random.Range(0, CombatMusic.Count)];
        Audio.Play();
    }



    public void ChooseAndPlayAmbientTrack()
    {
        while (randomTrack == lastTrack)
            randomTrack = Random.Range(0, AmbientMusic.Count);

        lastTrack = randomTrack;
        Audio.clip = AmbientMusic[randomTrack];
        Audio.Play();
    }



    public void StopMusic()
    {
        Audio.Stop();
    }



    public void FadeOutMusic(float _speed)
    {
        StartCoroutine(AudioFade.StartFade(Audio, _speed, 0));
    }

    public void FadeInMusic(float _speed)
    {
        StartCoroutine(AudioFade.StartFade(Audio, _speed, PlayerPrefs.GetFloat("musicVolume")));
    }



    public void TransitionToMenuMusic(float _fadeTime)
    {
        //StartCoroutine(AudioFade.StartFade(Audio, fadeTime, 0));
        StartCoroutine(WaitToGoToStartMenuMusic(_fadeTime));
    }

    public IEnumerator WaitToGoToStartMenuMusic(float _fadeTime)
    {
        yield return new WaitForSeconds(_fadeTime);
        PlayMenuMusic();
        FadeInMusic(_fadeTime);
    }



    public void TransitionToCombatMusic(float _fadeTime)
    {
        FadeOutMusic(_fadeTime);
        StartCoroutine(WaitToGoToStartCombatMusic(_fadeTime));
    }

    public IEnumerator WaitToGoToStartCombatMusic(float _fadeTime)
    {
        yield return new WaitForSeconds(_fadeTime);
        PlayCombatMusic();
        FadeInMusic(_fadeTime);
    }



    public void TransitionToAmbient(float _fadeTime)
    {
        FadeOutMusic(_fadeTime);
        StartCoroutine(WaitToGoToStartAmbientMusic(_fadeTime));
    }

    public IEnumerator WaitToGoToStartAmbientMusic(float _fadeTime)
    {
        yield return new WaitForSeconds(_fadeTime);
        PlayAmbientMusic();
        FadeInMusic(_fadeTime);
    }



    public void TransitionToEventMusic(float _fadeTime)
    {
        FadeOutMusic(_fadeTime);
        StartCoroutine(WaitToGoToStartEventMusic(_fadeTime));
    }

    public IEnumerator WaitToGoToStartEventMusic(float _fadeTime)
    {
        yield return new WaitForSeconds(_fadeTime);
        PlayEventMusic();
        FadeInMusic(_fadeTime);
    }
}
