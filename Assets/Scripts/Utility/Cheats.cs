using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using PixelCrushers.DialogueSystem;

public class Cheats : MonoBehaviour
{
    public bool enableCheats;



    void Update()
    {
        if (enableCheats)
        {
            if (Input.GetKeyDown(KeyCode.K))
                KillCharacter();

            if (Input.GetKeyDown(KeyCode.O))
                OpenMap();

            if (Input.GetKeyDown(KeyCode.X))
                AddXP();

            if (Input.GetKeyDown(KeyCode.P))
                Misc();
        }
    }

    void OpenMap()
    {
        WorldMap.instance.AreasUnlocked.Add(1);
    }

    void KillCharacter()
    {
        foreach (Character _C in Combat.instance.CombatList)
        {
            if (!_C.playerControlledCombat)
            {
                Combat.instance.Defending = _C;
                Combat.instance.KillTarget();
            }
        }
    }

    void AddXP()
    {
        foreach (var quest in UI.instance.QuestLogWindow.quests)
        {
            Debug.Log(quest.Heading.text);
        }
    }

    void Misc()
    {
        //DialogueManager.ShowAlert("This is an alert.");
        //PlayerScene.instance.MainCharacter.DeductCredits(500);

        //Debug.Log(QuestLog.GetQuestEntryCount("Dude, Where's My Wife?"));
        //Debug.Log(QuestLog.GetQuestEntry("Dude, Where's My Wife?", 1));
        //Debug.Log(QuestLog.GetQuestEntryState("Dude, Where's My Wife?", 1));

        // quest
        QuestLog.SetQuestEntryState("You Bet Your Life", 4, "success");
        QuestLog.SetQuestEntryState("You Bet Your Life", 5, "active");
        WorldMap.instance.AreasUnlocked.Add(1);
    }
}