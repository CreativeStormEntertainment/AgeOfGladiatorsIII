using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindow : MonoBehaviour
{
    public void Populate()
    {
        GameSounds.instance.PlayAudioCue(AudioCues.Craft);
    }
}
