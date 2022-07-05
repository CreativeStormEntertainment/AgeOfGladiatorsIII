using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindow : MonoBehaviour
{
    // populate
    public void Populate()
    {
        // audio
        GameSounds.instance.PlayAudioCue(AudioCues.Craft);
    }
}
