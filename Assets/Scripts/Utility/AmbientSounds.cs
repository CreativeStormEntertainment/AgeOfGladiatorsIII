using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    public static AmbientSounds instance;

    static AudioSource Audio;

    [Header("Sound")]
    public AudioClip[] CitySounds;



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



    // ambient - play
    public void PlayAmbient(int _index)
    {
        Audio.clip = CitySounds[_index];
        Audio.Play();
    }

    // ambient - stop
    public void StopAmbient()
    {
        Audio.Stop();
    }



    // music - fade out
    public void FadeOut(float _speed)
    {
        StartCoroutine(AudioFade.StartFade(Audio, _speed, 0));
    }

    // music - fade in
    public void FadeIn(float _speed)
    {
        StartCoroutine(AudioFade.StartFade(Audio, _speed, PlayerPrefs.GetFloat("musicVolume")));
    }



    // transition to combat music
    public void TransitionToAmbient(float _fadeTime, int _index)
    {
        FadeOut(_fadeTime);

        if (_index != 5000)
            StartCoroutine(WaitToStart(_fadeTime, _index));
        else
            StopAmbient();
    }

    // wait to start combat music
    public IEnumerator WaitToStart(float _fadeTime, int _index)
    {
        yield return new WaitForSeconds(_fadeTime);
        PlayAmbient(_index);
        FadeIn(_fadeTime);
    }
}
