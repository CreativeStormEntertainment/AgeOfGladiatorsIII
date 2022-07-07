using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerSkillRoll : MonoBehaviour
{
    [Header("Skill")]
    public Skill SkillRequired;
    public int skillLevel;

    [Header("Other")]
    public bool openConversation;
    public bool itemLocked;

    [HideInInspector]
    public bool cannotClick;



    private void Start()
    {
        // needs to be here?
        if (SkillRequired == Skill.None)
            GetComponent<TriggerSkillRoll>().itemLocked = false;
    }



    public void AttemptItemSkillRoll()
    {
        // audio
        if (GetComponent<AudioCue>() != null)
            GameSounds.instance.PlayAudioCue(SkillRequired);


        PlayerScene.instance.MainCharacter.GetComponent<ActionTextActivator>().ActivateActionText("ATTEMPTING...");
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().restrictMovement = true;
        cannotClick = true;

        StartCoroutine(OpenConclusionTimer());
    }

    public IEnumerator OpenConclusionTimer()
    {
        yield return new WaitForSeconds(2f);

        CompleteAttemptSkillLevel();

        cannotClick = false;
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().restrictMovement = false;
    }

    void CompleteAttemptSkillLevel()
    {
        // stat
        string _stat = SkillRequired.ToString();
        _stat = _stat.ToUpper();

        // attempt roll
        if (PlayerScene.instance.MainCharacter.SkillCheck(SkillRequired, skillLevel))
        {
            itemLocked = false;
            UnlockConditions();
            AlertText(_stat);
            PlayerScene.instance.MainCharacter.GainExperience(GameData.unlockXP);
        }
        else
        {
            PlayerScene.instance.MainCharacter.GetComponent<ActionTextActivator>().ActivateActionText(_stat + " TOO LOW (" + PlayerScene.instance.MainCharacter.GetSkill(SkillRequired) + "/" + skillLevel + ")");
        }
    }





    void UnlockConditions()
    {
    }

    void AlertText(string _incoming)
    {
        // stat
        string _stat = _incoming + " SUCCESS!";
        _stat = _stat.ToUpper();

        // activate alert
        if (!String.IsNullOrEmpty(GetComponent<MapItem>().mapItemName))
            PlayerScene.instance.MainCharacter.GetComponent<ActionTextActivator>().ActivateActionText(_stat + "\n" + GetComponent<MapItem>().mapItemName.ToUpper() + " UNLOCKED!");
    }
}
