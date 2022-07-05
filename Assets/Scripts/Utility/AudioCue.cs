using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    [Header("Music Cue")]
    public MusicCues MusicCueOnTrigger;
    [Header("Audio Cue")]
    public AudioCues AudioCueOnTrigger;



    // play audio cue
    public void PlayCue()
    {
        if (MusicCueOnTrigger != MusicCues.None)
            GameMusicCues.instance.PlayMusicCue(MusicCueOnTrigger);

        if (AudioCueOnTrigger != AudioCues.None)
            GameSounds.instance.PlayAudioCue(AudioCueOnTrigger);
    }
}
