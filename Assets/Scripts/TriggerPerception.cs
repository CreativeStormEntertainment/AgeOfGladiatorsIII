using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerPerception : MonoBehaviour
{
    [Header("Perception Level")]
    public int perceptionLevel;

    [Header("Discovered")]
    public bool alreadyDiscovered;



    void Update()
    {
        if (Master.instance == null)
            return;

        PerceptionCheck();
    }



    void PerceptionCheck()
    {
        if (Vector3.Distance(PlayerScene.instance.MainCharacter.transform.position, transform.position) <= 5)
        {
            // roll for perception
            if (PlayerScene.instance.MainCharacter.SkillCheck(Skill.Perception, perceptionLevel))
            {
                if (!alreadyDiscovered)
                {
                    PlayerScene.instance.MainCharacter.GainExperience(GameData.unlockXP);
                    PlayerScene.instance.MainCharacter.GetComponent<ActionTextActivator>().ActivateActionText("PERCEPTION SUCCESS!");

                    GameSounds.instance.PlayAudioCue(AudioCues.Perception);
                }
                    
                alreadyDiscovered = true;
                GetComponent<MouseOver>().Outliner.enabled = true;
            }
        }
        else
        {
            if (!GetComponent<MouseOver>().mouseOver)
                GetComponent<MouseOver>().Outliner.enabled = false;
        }
    }
}
