using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;


public class TriggerMissionClick : MonoBehaviour
{
    [Header("Quest To Check")]
    [QuestPopup(showReferenceDatabase = true)]
    public string questName;
    [QuestEntryPopup]
    public int questEntry;

    [Header("Variable To Increment")]
    [VariablePopup]
    public string variable;
    public int amountRequired;

    public bool useOnce;
    [HideInInspector]
    public bool used;



    // update quest
    public void UpdateQuest()
    {
        if ((QuestLog.GetQuestState(questName) == QuestState.Active) && (QuestLog.GetQuestEntryState(questName, questEntry) == QuestState.Active))
        {
            if (useOnce && used)
                return;

            used = true;

            int oldValue = DialogueLua.GetVariable(variable).asInt;
            int newValue = Mathf.Clamp(oldValue + 1, 0, amountRequired);
            DialogueLua.SetVariable(variable, newValue);
            DialogueManager.SendUpdateTracker();

            if (DialogueLua.GetVariable(variable).asInt >= amountRequired)
                GameActions.instance.AdvanceQuest(questName, questEntry);
            else
                GameActions.instance.OpenQuestBox(questName, questEntry);
        }
    }



    private void OnEnable()
    {
        Lua.RegisterFunction("Increment", this, SymbolExtensions.GetMethodInfo(() => UpdateQuest()));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("EventScreenTrigger");
    }
}
