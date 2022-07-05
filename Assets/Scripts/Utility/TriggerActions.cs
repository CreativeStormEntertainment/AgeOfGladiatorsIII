using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class TriggerActions : DialogueSystemTrigger
{
    private void Start()
    {
        if (barker == null)
            barker = PlayerScene.instance.MainCharacter.transform;
    }



    protected override void DoQuestAction()
    {
        if (questName != "")
        {
            // base method tasks
            base.DoQuestAction();

            // grant xp
            if (setAnotherQuestEntryState)
                PlayerScene.instance.MainCharacter.GainExperience(GameData.smallMissionXP);

            // open quest box
            if (QuestLog.GetQuestState(questName) == QuestState.Unassigned)
                UI.instance.OpenQuestBox(questName, 1);
            else
                UI.instance.OpenQuestBox(questName, questEntryNumber);  
        }
    }

    public override void PauseMovement()
    {
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().conversationTriggered = true;
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().restrictMovement = true;
    }



    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (Combat.instance.combatActivated || Dredd.instance == null)
            return;

        if (other.tag == "Player") // called here for now until can figure out proper use with dropdown
            OnUse();
    }
}
