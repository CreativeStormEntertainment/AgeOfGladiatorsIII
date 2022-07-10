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



    public void PlayAmbient(int _index)
    {
        Audio.clip = CitySounds[_index];
        Audio.Play();
    }

    public void StopAmbient()
    {
        Audio.Stop();
    }



    public void FadeOut(float _speed)
    {
        StartCoroutine(AudioFade.StartFade(Audio, _speed, 0));
    }

    public void FadeIn(float _speed)
    {
        StartCoroutine(AudioFade.StartFade(Audio, _speed, PlayerPrefs.GetFloat("musicVolume")));
    }



    public void TransitionToAmbient(float _fadeTime, int _index)
    {
        FadeOut(_fadeTime);

        if (_index != 5000)
            StartCoroutine(WaitToStart(_fadeTime, _index));
        else
            StopAmbient();
    }

    public IEnumerator WaitToStart(float _fadeTime, int _index)
    {
        yield return new WaitForSeconds(_fadeTime);
        PlayAmbient(_index);
        FadeIn(_fadeTime);
    }
}
