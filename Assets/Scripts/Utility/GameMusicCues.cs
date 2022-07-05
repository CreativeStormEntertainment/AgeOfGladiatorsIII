using System.Collections;
using UnityEngine;

public class GameMusicCues : MonoBehaviour
{
    public static GameMusicCues instance;

    public static AudioSource Audio;

    [Header("Cues")]
    public AudioClip[] LevelMusic;
    public AudioClip[] GameShowMusic;
    public AudioClip[] MissionAdvanceMusic;
    public AudioClip[] CombatInMusic;
    public AudioClip[] CombatOutMusic;



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



    // play music cue (enum)
    public void PlayMusicCue(MusicCues _Cue)
    {
        if (_Cue == MusicCues.None)
            return;

        switch (_Cue)
        {
            case MusicCues.GameShow:
                PlayMusicCue(GameShowMusic, 0);
                break;
            case MusicCues.Event:
                GameMusic.instance.TransitionToEventMusic(1f);
                break;
            case MusicCues.LevelUp:
                PlayMusicCue(LevelMusic, 0);
                break;
            case MusicCues.MissionAdvance:
                PlayMusicCue(MissionAdvanceMusic, 0);
                break;
            case MusicCues.CombatIn:
                PlayCombatIntro();
                break;
            case MusicCues.CombatOut:
                PlayMusicCue(CombatOutMusic, 0);
                break;
        }
    }

    // play music cue
    public void PlayMusicCue(AudioClip[] _Array, int _index)
    {
        // fade out ambient music
        GameMusic.instance.FadeOutMusic(1f);

        // play cue
        Audio.PlayOneShot(_Array[_index]);

        // after length of music cue fade back in ambient
        StartCoroutine(FadeBackInAmbientMusic((_Array[_index].length - 1)));
    }

    // play combat intro (needs to be here)
    public void PlayCombatIntro()
    {
        Audio.PlayOneShot(CombatInMusic[0]);
    }

    // fade back in ambient music
    public IEnumerator FadeBackInAmbientMusic(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        GameMusic.instance.FadeInMusic(2f);
    }
}
