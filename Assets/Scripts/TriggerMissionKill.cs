using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class TriggerMissionKill : MonoBehaviour
{
    [Header("Type")]
    public QuestTriggers Type;

    [Header("Quest")]
    [QuestPopup(showReferenceDatabase = true)]
    public string questName;
    [QuestEntryPopup]
    public int questEntry;



    public void TriggerResolve()
    {
        switch (Type)
        {
            case QuestTriggers.QuestEntryAdvance:
                Advance();
                break;
            case QuestTriggers.QuestEntrySuccess:
                Succeed();
                break;
            case QuestTriggers.QuestActivate:
                break;
        }
    }



    void Advance()
    {
        GameActions.instance.AdvanceQuest(questName, questEntry);
    }

    void Succeed()
    {
        QuestLog.SetQuestEntryState(questName, questEntry, QuestState.Success);

        UI.instance.OpenQuestBox(questName, questEntry);
    }
}
